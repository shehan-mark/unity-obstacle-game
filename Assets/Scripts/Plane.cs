using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{

    public float MoveForwardSpeed, DodgeSpeed;
    public GameObject WallRight, WallLeft;

    // Start is called before the first frame update
    void Start()
    {
        MoveForwardSpeed = 0.01f;
        DodgeSpeed = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();

        // listen to input
        if (Input.GetKey("a"))
        {
            DodgeLeft();
        }

        if (Input.GetKey("d"))
        {
            DodgeRight();
        }
    }

    void MoveForward()
    {
        transform.position = transform.position + new Vector3(0.0f, 0.0f, MoveForwardSpeed);
    }

    void DodgeLeft()
    {
        Transform LeftWallPosition = WallLeft.transform;
        if (transform.position.x > LeftWallPosition.position.x + 1)
        {
            transform.position = transform.position + new Vector3(-DodgeSpeed, 0.0f, 0.0f);
        }
    }

    void DodgeRight()
    {
        Transform RightWallPosition = WallRight.transform;
        if (transform.position.x < RightWallPosition.position.x - 1)
        {
            transform.position = transform.position + new Vector3(DodgeSpeed, 0.0f, 0.0f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        print($"LOG:: Collision| {collision.collider.name}");
    }

    void OnTriggerEnter(Collider other)
    {
        //print($"LOG:: Trigger - {other.tag}");
        if (other.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
