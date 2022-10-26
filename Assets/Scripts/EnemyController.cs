using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed;
    public Vector3 targetPosition1;
    public Vector3 targetPosition2;
    private bool moveLeft;
    private bool moveRight;

    void Start()
    {
        moveLeft = false;
        moveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition1, speed * Time.deltaTime);
            if((transform.position - targetPosition1).magnitude < 0.1f)
            {
                moveRight = false;
                moveLeft = true;
            }
        }
        if(moveLeft)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition2, speed * Time.deltaTime);
            if((transform.position - targetPosition2).magnitude < 0.1f)
            {
                moveRight = true;
                moveLeft = false;
            }
        }
    }
}
