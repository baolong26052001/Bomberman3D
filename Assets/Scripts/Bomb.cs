using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    PlayerController player;
    EnemyController enemy;

    [SerializeField] private float explodeDelay = 2f;
    private float explosionTimer = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
    }

    void Update()
    {
        explosionTimer += Time.deltaTime;
        if (explosionTimer >= explodeDelay)
        {
            Debug.Log("Bomb has exploded");
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.tag == "Player")
        {
            GetComponent<SphereCollider>().isTrigger = false;
        }
        if (other.tag == "Enemy")
        {
            GetComponent<SphereCollider>().isTrigger = false;
        }
    }

    private void Explode()
    {
        
    }
}