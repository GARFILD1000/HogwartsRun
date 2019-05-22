using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordsMovement : MonoBehaviour
{
    public float speed = 0.05f;
    public float range = 5;
    float y; 
    // Start is called before the first frame update
    void Start()
    {
        y = Random.Range(0,25);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        y += speed;
        newPosition.y += Mathf.Sin(y) * range;
        transform.position = newPosition;
    }
}
