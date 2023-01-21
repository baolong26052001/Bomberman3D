using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] target;

    

    [SerializeField] private float moveSpeed = 1f;

    Rigidbody myRigidBody;

    private bool isMoving = true;

    private void Start() 
    {
        myRigidBody = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            
            myRigidBody.MovePosition(Vector3.MoveTowards(transform.position, target[0].position, Time.deltaTime * moveSpeed));
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            isMoving = false;
        }
        if (collision.gameObject.tag == "Bomb")
        {
            isMoving = false;
        }
    }
}