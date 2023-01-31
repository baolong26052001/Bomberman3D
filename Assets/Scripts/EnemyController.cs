using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] target;

    

    [SerializeField] private float moveSpeed = 1f;

    Rigidbody myRigidBody;

    private bool isMoving = true;
    private int waypointDestination = 0;

    private void Start() 
    {
        myRigidBody = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            myRigidBody.MovePosition(Vector3.MoveTowards(transform.position, target[waypointDestination].position, Time.deltaTime * moveSpeed));
            if (Vector3.Distance(transform.position, target[waypointDestination].position) < 0.1f)
            {
                if (waypointDestination >= target.Length - 1)
                {
                    waypointDestination = 0;
                }
                else
                {
                    waypointDestination++;
                }
            }
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

    public void Die()
    {
        Destroy(gameObject);
    }
}