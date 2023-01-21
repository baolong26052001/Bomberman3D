using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    PlayerController player;
    EnemyController enemy;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
    }

    void Update()
    {
        
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