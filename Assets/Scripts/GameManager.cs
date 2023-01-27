using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int lives = 3;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform playerParentTransform;

    void Start()
    {
        Instantiate(playerPrefab, new Vector3(0,0,0), Quaternion.identity, playerParentTransform);
    }

    void Update()
    {

    }
}
