using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSlide : MonoBehaviour
{
    public Animator[] Animators;
    // Start is called before the first frame update
    public int menuSelection = 1;
    public int menuPositions = 4;
    public float cameraStep = 1.82f;
    public float speed = 4;
    public Vector3 moveOrientation;
    public SwipeController swipeController;
    Vector3 cameraStartPosition;
    void Start()
    {
        cameraStartPosition = transform.position;
        swipeController.SwipeEvent += CheckSwipeInput;
    }


    void CheckSwipeInput(SwipeController.SwipeType type)
    {
        if (type == SwipeController.SwipeType.LEFT && menuSelection < menuPositions)
        {
            menuSelection++;
            Animate();
        }
        else if (type == SwipeController.SwipeType.RIGHT && menuSelection > 1)
        {
            menuSelection--;
            Animate();
        }
    }


    void Animate()
    {
        if (menuSelection <= Animators.Length)
        {
            StartCoroutine(SetTrigger(menuSelection - 1));
        }
    }

    IEnumerator SetTrigger(int number)
    {
        Animators[number].SetTrigger("animate");
        yield return null;
        Animators[number].ResetTrigger("animate");

    }


    bool PositionsEquals(Vector3 position1, Vector3 position2, float delta)
    {
        bool equals = false;
        if (Mathf.Abs(position1.x - position2.x) <= delta)
        {
            if (Mathf.Abs(position1.y - position2.y) <= delta)
            {
                if (Mathf.Abs(position1.z - position2.z) <= delta)
                {
                    equals = true;
                }
            }
        }
        return equals;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Lerp(newPosition.x, cameraStartPosition.x - (menuSelection - 1) * cameraStep * moveOrientation.x, Time.deltaTime * speed); 
        newPosition.y = Mathf.Lerp(newPosition.y, cameraStartPosition.y - (menuSelection - 1) * cameraStep * moveOrientation.y, Time.deltaTime * speed);
        newPosition.z = Mathf.Lerp(newPosition.z, cameraStartPosition.z - (menuSelection - 1) * cameraStep * moveOrientation.z, Time.deltaTime * speed);
        transform.position = newPosition;
    }
}
