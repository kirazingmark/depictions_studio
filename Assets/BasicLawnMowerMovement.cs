using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLawnMowerMovement : MonoBehaviour {

    public float moveSpeed;
    public float sideMoveSpeed;
    public float speed; // Rotation Speed.

    public PickUpObject po;

    // Use this for initialization
    void Start()
    {
        moveSpeed = 2.5f;
        sideMoveSpeed = 0.0f; // Disabled.
        speed = 50.0f;
        po = GameObject.FindGameObjectWithTag("Player").GetComponent<PickUpObject>();
        // Debug.Log(po.isMowerActive);
    }

    // Update is called once per frame
    void Update()
    {
        if(po.isMowerActive == false)
        {
            moveSpeed = 0.0f;
            sideMoveSpeed = 0.0f;
            speed = 0.0f;
        }
        else if (po.isMowerActive == true)
        {
            moveSpeed = 2.5f;
            sideMoveSpeed = 0.0f; // Disabled.
            speed = 25.0f;
            transform.Translate(sideMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
            transform.Rotate(0, Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0);
        }

    }
}