using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    PlayerController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
    }
}