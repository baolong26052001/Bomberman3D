using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTransform;

    [SerializeField] private Vector3 offset;

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + offset;
        }        
    }


    public void SetPlayer(GameObject player)
    {
        playerTransform = player.transform;
    }
}
