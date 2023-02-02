using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    // An array of all of our power up type prefabs
    [SerializeField] GameObject[] powerUpPrefabs;


    // This method is called each time a destructible block is destroyed
    // Will check to see if a random power up should be spawned
    public void BlockDestroyed(Vector3 pos)
    {
        if (Random.value < 0.25f)            
        {
            Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], pos, Quaternion.identity);
        }
    }
}
