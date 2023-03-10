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

    [SerializeField] private int maxBombs = 2;
    private int currentBombsPlaced = 0;

    private bool hasControl = true;
    [SerializeField] private float destroyTime = 2f;

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
        if (hasControl == true)
        {
            Movement();
            PlaceBomb(); 
        }
        
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
        if (Input.GetKeyDown(placeBomb) && (currentBombsPlaced < maxBombs))
        {
            GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            //bomb.transform.position = new Vector3(Mathf.Round(transform.position.x), 0.8f, Mathf.Round(transform.position.z));
            currentBombsPlaced++;
        }
    }

    public void Die()
    {
        hasControl = false;
        Destroy(gameObject, destroyTime);
        myGameManager.PlayerDied();
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Die();
        }
    }

    public void BombExploded()
    {
        currentBombsPlaced--;
    }

    public float GetDestroyDelayTime()
    {
        return destroyTime;
    }
}