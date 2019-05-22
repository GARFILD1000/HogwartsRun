using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBeatController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            print("PLayer beat knight!");
            StartCoroutine(Beat());
        }

    }

    IEnumerator Beat()
    {
        animator.SetTrigger("beat");
        yield return new WaitForSeconds(0.5f);
        animator.ResetTrigger("beat");
    }

}
