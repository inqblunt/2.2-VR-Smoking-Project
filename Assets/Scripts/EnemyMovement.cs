using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int moveSpeed = 10;  //per second 
    Vector3 computerDirection = Vector3.right;
    Vector3 moveDirection = Vector3.zero;
    Vector3 newPosition = Vector3.zero;
    void Start()
    {

    }

    void Update()
    {
        Vector3 newPosition = computerDirection * (moveSpeed * Time.deltaTime);
        newPosition = transform.position + newPosition;
        newPosition.x = Mathf.Clamp(newPosition.x, 14, 15);
        transform.position = newPosition;
        if (newPosition.x > 15)
        {
            newPosition.x = 15;
            computerDirection.x *= -1;
        }
        else if (newPosition.x < 14)
        {
            newPosition.x = 14;
            computerDirection.x *= -1;
        }
    }
}