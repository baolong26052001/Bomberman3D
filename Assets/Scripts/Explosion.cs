using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Rigidbody myRigidBody;
    private Vector3 explodeDirection = Vector3.zero;
    private float explodeSpeed = 200f;
    private float explodeRange = 2f;

    private Vector3 startPosition;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        startPosition = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, startPosition) >= explodeRange)
        {
            Destroy(gameObject);
        }
    }

    
    private void FixedUpdate() 
    {
        myRigidBody.velocity = explodeDirection * explodeSpeed * Time.deltaTime;
    }

    public void SetExplosion(Vector3 direction, float speed, float range)
    {
        explodeDirection = direction;
        explodeSpeed = speed;
        explodeRange = range;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Block")
        {
            Destroy(gameObject);
        }    
    }
}