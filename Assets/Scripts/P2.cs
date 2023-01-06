using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2 : MonoBehaviour
{
    [SerializeField] float xMoveSpeed;
    [SerializeField] float yMoveSpeed;
    [SerializeField] float zMoveSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0f, 0f, zMoveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0f, 0f, -zMoveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(xMoveSpeed * Time.deltaTime,0f,0f);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-xMoveSpeed * Time.deltaTime,0f,0f);
        }
    }
}