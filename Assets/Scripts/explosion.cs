using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    private Rigidbody myRigidBody;
    private Vector3 explodeDirection = Vector3.zero;
    private float explodeSpeed = 200f;
    private float explodeRange = 2f;

    private Vector3 startPosition;

    // Start is called before the first frame update
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
        switch (other.gameObject.tag)
        {
            case "Block":
                {
                    Destroy(gameObject);
                    break;
                }
            case "Bomb":
                {
                    other.gameObject.GetComponent<Bomb>().Explode();
                    Destroy(gameObject);
                    break;
                }
            case "Player":
                {
                    other.gameObject.GetComponent<PlayerController>().Die();
                    break;
                }
            case "Enemy":
                {
                    other.gameObject.GetComponent<EnemyController>().Die();
                    break;
                }
            case "Destructible":
                {
                    FindObjectOfType<PowerUpSpawner>().BlockDestroyed(other.transform.position);

                    // Play destroy block animation
                    other.gameObject.GetComponent<Animator>().SetTrigger("isDestroyed");
                    Destroy(other.gameObject, .5f);
                    Destroy(gameObject);

                    break;
                }
            case "PowerUp":
                {
                    Destroy(other.gameObject);
                    Destroy(gameObject);

                    break;
                }
        }
    }

}
