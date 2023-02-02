using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    enum PowerUps { MaxBombs, Range, Speed };

    [SerializeField] PowerUps powerUpType;
        
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerController>().PlayPowerupPickupSound();
            switch (powerUpType)
            {
                case PowerUps.MaxBombs:
                    {
                        FindObjectOfType<GameManager>().IncreaseMaxBombs();
                        Destroy(gameObject);
                        break;
                    }
                case PowerUps.Range:
                    {
                        FindObjectOfType<GameManager>().IncreaseExplodeRange();
                        Destroy(gameObject);
                        break;
                    }
                case PowerUps.Speed:
                    {
                        FindObjectOfType<GameManager>().IncreaseSpeed();
                        Destroy(gameObject);
                        break;
                    }
            }
        }
    }
}
