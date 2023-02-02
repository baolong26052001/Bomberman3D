using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveSpeed = 0;    

    Rigidbody myRigidBody;

    [SerializeField] GameObject bombPrefab;

    private GameManager myGameManager;

    private int maxBombs = 1;
    private int currentBombsPlaced = 0;

    private bool hasControl = true;
    [SerializeField] private float destroyTime = 2f;

    private bool isPaused = false;
    private bool isDead = false;

    [SerializeField] private LayerMask whatAreBombLayers;

    private Animator myAnimator;
    private AudioSource myAudioSource;

    [SerializeField] private AudioClip playerDeathSound;
    [SerializeField] private AudioClip powerUpPickupSound;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        myGameManager = FindObjectOfType<GameManager>();
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasControl && !isPaused)
        {
            Movement();
            Rotation();
            UpdateAnimator();
            PlaceBomb();
        }
    }

    
    private void Rotation()
    {
        if (myRigidBody.velocity != Vector3.zero)
        {
            transform.forward = myRigidBody.velocity;
        }        
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        
        Vector3 newVelocity = new Vector3(x * moveSpeed, 0f, z * moveSpeed);
        myRigidBody.velocity = newVelocity;        
    }


    private void PlaceBomb()
    {
        if (Input.GetButtonDown("Fire1") && (currentBombsPlaced < maxBombs))
        {
            Vector3 center = new Vector3(Mathf.Round(transform.position.x), 0f, Mathf.Round(transform.position.z));
            
            // Create an overlap sphere where the new bomb will be placed to check if there is already a bomb there
            Collider[] hitColliders = Physics.OverlapSphere(center, 0.5f, whatAreBombLayers);
            if (hitColliders.Length > 0)
            {
                return;
            }

            GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
            bomb.transform.position = center;
            currentBombsPlaced++;
        }
    }

    public void Die()
    {
        if (!isDead)
        {
            isDead = true;

            // Play death sound
            myAudioSource.PlayOneShot(playerDeathSound);

            // Player has died so take away control
            hasControl = false;

            // Removes all of players velocity since we now have no control of the player
            myRigidBody.velocity = Vector3.zero;

            myRigidBody.isKinematic = true;

            // Destroy the player after a set delay time
            Destroy(gameObject, destroyTime);

            // Tell the GameManager that the player has died
            myGameManager.PlayerDied();

            // Play a player death animation
            myAnimator.SetBool("isDead", true);            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            // Player hit the Enemy and died
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

    // Called when the player is spawned and sets its starting values
    public void InitializePlayer(int bombs, float speed)
    {
        maxBombs = bombs;
        moveSpeed = speed;
    }

    public void SetPaused(bool state)
    {
        isPaused = state;
    }

    private void UpdateAnimator()
    {
        // If player has no velocity play the idle animation
        if (myRigidBody.velocity == Vector3.zero)
        {
            myAnimator.SetBool("isWalking", false);
        }
        // Player has velocity so play the walk animation
        else
        {
            myAnimator.SetBool("isWalking", true);
        }
    }

    public void PlayVictory()
    {
        hasControl = false;
        myAnimator.SetBool("isVictory", true);
    }

    public void PlayPowerupPickupSound()
    {        
        myAudioSource.PlayOneShot(powerUpPickupSound);
    }
}
