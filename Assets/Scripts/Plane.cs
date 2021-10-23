using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{

    public float MoveForwardSpeed, DodgeSpeed;
    private GameObject WallRight, WallLeft;
    private UIController UIControllerRef;
    private bool IsColliding;

    // Start is called before the first frame update
    void Start()
    {
        UIControllerRef = GameObject.FindObjectOfType<UIController>();
        WallRight = GameObject.Find("Right-Wall");
        WallLeft = GameObject.Find("Left-Wall");
        MoveForwardSpeed = 0.01f;
        DodgeSpeed = 0.01f;
        IsColliding = false;
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
        if (UIControllerRef.GetGameState() == GameState.Running)
        {
            transform.position = transform.position + new Vector3(0.0f, 0.0f, MoveForwardSpeed);
        }
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
            UIControllerRef.SetGameState(GameState.GameOver);
            Destroy(this.gameObject);
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (IsColliding)
        {
            return;
        }

        //print($"LOG:: Trigger Exit - {other.tag}");
        if (other.tag == "Milestone")
        {
            UIControllerRef.SetScore(1);
            IsColliding = true;
            StartCoroutine(ResetColliding());
        }
    }

    private IEnumerator ResetColliding()
    {
        yield return new WaitForSeconds(0.5f);
        IsColliding = false;
    }

}
