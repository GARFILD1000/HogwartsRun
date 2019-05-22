using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;
    Vector3 startDistance, moveVector;
    // Start is called before the first frame update
    public bool targeting = true;
    void Start()
    {
        startDistance = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (targeting)
        {
            moveVector = target.position + startDistance;
            if (moveVector.y < 0.5f)
            {
                moveVector.y = 0.5f;
            }
            //moveVector.y = target.position.y + startDistance.y;//startDistance.y;
            //zmoveVector.z = 0;
            transform.position = moveVector;
        }
    }
}
