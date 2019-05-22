using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CapsuleCollider characterCollider;
    Rigidbody rigidBody;
    float DELTA = 0.1f;
    public Animator skinAnimator;
    public float jumpSpeed = 15.0f;
    int lineNumber = 1;
    int lineCount = 2;
    public float firstLinePosition;
    public float lineDistance;
    public float sideSpeed;
    public GameManager gameManager;
    public SwipeController swipeController;

    bool startRolling = false;
    bool doRolling = false;
    bool startJumping = false;
    bool doJumping = false;

    Vector3 startPosition;
    Vector3 playerVelocity;

    Vector3 ccCenterRun = new Vector3(0, 1f, 0), 
        ccCenterRoll = new Vector3(0, 0.25f, 1f),
        ccCenterJump = new Vector3(0, 2f, 0f);
    float ccHeightRun = 2, ccHeightRoll = 0.25f;
    float ccRadiusRun = 0.5f, ccRadiusRoll = 0.25f;

    void Start()
    {

        startPosition = transform.position;
        characterCollider = GetComponent<CapsuleCollider>();
        rigidBody = GetComponent<Rigidbody>();
        addSwipeEvent();

    }

    public void addSwipeEvent()
    {
        swipeController.SwipeEvent += CheckSwipeInput;
    }

    public void removeSwipeEvent()
    {
        swipeController.SwipeEvent -= CheckSwipeInput;
    }

    bool isGrounded()
    {
        Vector3 rayStart = transform.position;
        rayStart.y += 0.025f;
        Debug.DrawRay(rayStart, Vector3.down * 0.05f, Color.red);
        bool grounded = Physics.Raycast(rayStart, Vector3.down, 0.05f);
        return grounded;
    }

    void FixedUpdate()
    {
        rigidBody.AddForce(new Vector3(0, Physics.gravity.y * 4, 0), ForceMode.Acceleration);
        if (startJumping && isGrounded() && !doRolling)
        {
            rigidBody.AddForce(new Vector3(0,jumpSpeed,0), ForceMode.Impulse);
            skinAnimator.SetTrigger("jumping");
            AudioManager.Instance.PlayEffect(AudioManager.Instance.jumpEffectSound);
            doJumping = true;
            startJumping = false;
        }
    }

    void Update()
    {
        if (gameManager.canPlay)
        {
            CheckKeyboardInput();
            TurnPlayer();
        }
    }

    void TurnPlayer()
    {
        if (isGrounded())
        {
            if (!doRolling)
            {
                if (startRolling && !doJumping)
                {
                    startRolling = false;
                    StartCoroutine(DoRoll());
                }
                if (doJumping)
                {
                    AudioManager.Instance.PlayEffect(AudioManager.Instance.landingEffectSound);
                    skinAnimator.ResetTrigger("jumping");
                    doJumping = false;
                }
            }
            else
            {
                startJumping = false;
                startRolling = false;
            }
        }
        else
        {
            startRolling = false;
            startJumping = false;
        }

        Vector3 newPos = transform.position;
        newPos.z = Mathf.Lerp(newPos.z, firstLinePosition + ((float)lineNumber * lineDistance), Time.deltaTime * sideSpeed);
        transform.position = newPos;
        Vector3 objRot = transform.eulerAngles;

        if (Mathf.Abs(firstLinePosition + ((float)lineNumber * lineDistance) - newPos.z) < DELTA)
        {
            objRot.y = 90;
        }
        else if (firstLinePosition + ((float)lineNumber * lineDistance) > newPos.z)
        {

            if (Mathf.Abs(firstLinePosition + ((float)lineNumber * lineDistance) - newPos.z) < Mathf.Abs(lineDistance / 2.0f))
            {
                objRot.y = Mathf.Lerp(objRot.y, 90, Time.deltaTime * sideSpeed);
            }
            else
            {
                objRot.y = 70;
            }
        }
        else if (firstLinePosition + ((float)lineNumber * lineDistance) < newPos.z)
        {
            if (Mathf.Abs(firstLinePosition + ((float)lineNumber * lineDistance) - newPos.z) < Mathf.Abs(lineDistance / 2.0f))
            {
                objRot.y = Mathf.Lerp(objRot.y, 90, Time.deltaTime * sideSpeed);
            }
            else
            {
                objRot.y = 110;
            }
        }
        transform.eulerAngles = objRot;
    }

    void CheckSwipeInput(SwipeController.SwipeType type)
    {
        if (type == SwipeController.SwipeType.LEFT)
        {
            lineNumber += -1;
            lineNumber = Mathf.Clamp(lineNumber, 0, lineCount);
        }
        else if (type == SwipeController.SwipeType.RIGHT)
        {
            lineNumber += 1;
            lineNumber = Mathf.Clamp(lineNumber, 0, lineCount);
        }
        else if (type == SwipeController.SwipeType.UP)
        {
            startJumping = true;
        }
        else if (type == SwipeController.SwipeType.DOWN)
        {
            startRolling = true;
        }
    }

    void CheckKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !doRolling)
        {
            lineNumber += -1;
            lineNumber = Mathf.Clamp(lineNumber, 0, 2);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !doRolling)
        {
            lineNumber += 1;
            lineNumber = Mathf.Clamp(lineNumber, 0, 2);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            startJumping = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            startRolling = true;
        }
    }

    IEnumerator DoRoll()
    {
        doRolling = true;
        skinAnimator.SetTrigger("rolling");
        AudioManager.Instance.PlayEffect(AudioManager.Instance.rollEffectSound);
        characterCollider.center = ccCenterRoll;
        characterCollider.height = ccHeightRoll;
        characterCollider.radius = ccRadiusRoll;
        yield return new WaitForSeconds(1.1f);
        characterCollider.center = ccCenterRun;
        characterCollider.height = ccHeightRun;
        characterCollider.radius = ccRadiusRun;
        skinAnimator.ResetTrigger("rolling");
        doRolling = false;
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("trap") || collision.gameObject.CompareTag("deathPlane")) && gameManager.canPlay)
        {
            StartCoroutine(Death());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            gameManager.AddCoins(1);
            Destroy(other.gameObject);
            AudioManager.Instance.PlayEffect(AudioManager.Instance.coinEffectSound);
        }
        
    }

    IEnumerator Death()
    {
        gameManager.canPlay = false;
        skinAnimator.ResetTrigger("respawn");
        skinAnimator.SetTrigger("death");
        AudioManager.Instance.PlayEffect(AudioManager.Instance.deathEffectSound);
        yield return new WaitForSeconds(2.0f);
        gameManager.ShowResult();
        skinAnimator.ResetTrigger("death");
    }

    public void Respawn()
    {
        skinAnimator.SetTrigger("respawn");
        if (characterCollider != null)
        {
            characterCollider.center = ccCenterRun;
            characterCollider.height = ccHeightRun;
            characterCollider.radius = ccRadiusRun;
        }
        ResetPosition();
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        lineNumber = 1;
    }

    public void StopMovement()
    {
        playerVelocity = rigidBody.velocity;
        rigidBody.isKinematic = true;
        skinAnimator.speed = 0;
    }

    public void StartMovement()
    {
        rigidBody.velocity = playerVelocity;
        rigidBody.isKinematic = false;
        skinAnimator.speed = 1;
    }
}
