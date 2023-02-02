using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    PlayerController player;

    [SerializeField] private float explodeDelay = 2f;
    private float explosionTimer = 0;

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float explodeSpeed = 200f;
    private int explodeRange = 1;

    [SerializeField] private AudioClip bombExplodeSound;

    private bool hasExploded = false;
    [SerializeField] private GameObject bombModel;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        explodeRange = FindObjectOfType<GameManager>().GetExplodeRange();        
    }

    // Update is called once per frame
    void Update()
    {
        explosionTimer += Time.deltaTime;
        if (explosionTimer >= explodeDelay && !hasExploded)
        {
            Explode();            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<SphereCollider>().isTrigger = false;
        }
    }

    public void Explode()
    {
        GameObject explosionRight = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionRight.GetComponent<explosion>().SetExplosion(Vector3.right, explodeSpeed, explodeRange);

        GameObject explosionLeft = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionLeft.GetComponent<explosion>().SetExplosion(Vector3.left, explodeSpeed, explodeRange);

        GameObject explosionUp = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionUp.GetComponent<explosion>().SetExplosion(Vector3.forward, explodeSpeed, explodeRange);

        GameObject explosionDown = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionDown.GetComponent<explosion>().SetExplosion(Vector3.back, explodeSpeed, explodeRange);


        // Tell the player that the bomb exploded so it decreases its current bomb placed counter
        player.BombExploded();

        // Play bomb explode sound
        GetComponent<AudioSource>().PlayOneShot(bombExplodeSound);

        // Destroy the collider on the bomb and turn off the bomb model
        Destroy(GetComponent<Collider>());
        bombModel.SetActive(false);

        Destroy(gameObject, 2f);

        hasExploded = true;
    }
}

