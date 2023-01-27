using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float xMoveSpeed;
    [SerializeField] float yMoveSpeed;
    [SerializeField] float zMoveSpeed;

    Rigidbody myRigidBody;

    [SerializeField] GameObject bombPrefab;

    private GameManager myGameManager;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputRight = KeyCode.D;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode placeBomb = KeyCode.Space;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myGameManager = FindObjectOfType<GameManager>();
    }

    void Update() 
    {
        Movement();
        PlaceBomb(); 
    }

    private void Movement()
    {
        Vector3 newPosition = new Vector3();
        if (Input.GetKey(inputUp))
        {
            newPosition = new Vector3(0f,0f, zMoveSpeed);
        }
        else if (Input.GetKey(inputDown))
        {
            newPosition = new Vector3(0f,0f, -zMoveSpeed);
            
        }
        else if (Input.GetKey(inputRight))
        {
            newPosition = new Vector3(xMoveSpeed,0f,0f);
            
        }
        else if (Input.GetKey(inputLeft))
        {
            newPosition = new Vector3(-xMoveSpeed,0f,0f);
            
        }

        transform.position = transform.position + (newPosition * Time.deltaTime);
    }

    private void PlaceBomb()
    {
        if (Input.GetKeyDown(placeBomb))
        {
            GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        }
    }

    private void Die()
    {
        myGameManager.PlayerDied();
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Die();
        }
    }
}