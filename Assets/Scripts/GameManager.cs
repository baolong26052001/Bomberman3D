using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int lives = 3;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerParentTransform;

    private PlayerController currentPlayer;

    [SerializeField] private float delayToSpawnPlayer = 1f;

    void Start()
    {
        SpawnPlayer();
    }

    public void PlayerDied()
    {
        if (lives > 1)
        {
            Debug.Log("GameManager: Player has died");
            lives--;

            //float delayTime = GameObject.FindObjectOfType<PlayerController>().GetDestroyDelayTime();

            Invoke("SpawnPlayer", currentPlayer.GetDestroyDelayTime() + delayToSpawnPlayer);
        }
        else
        {
            Debug.Log("GameManager: No more lives - game over");
        }
    }

    private void SpawnPlayer()
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(0,-1,0), Quaternion.identity, playerParentTransform);
        currentPlayer = player.GetComponent<PlayerController>();
    }
}
