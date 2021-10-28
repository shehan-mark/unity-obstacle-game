using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DodgeDirection
{
    Right,
    Left
}


public class Plane : MonoBehaviour
{

    public float MoveForwardSpeed = 0.01f;
    public float DodgeSpeed = 0.01f;
    public float DashSpeed = 0.1f;
    private GameObject WallRight, WallLeft;
    private UIController UIControllerRef;
    private bool IsColliding;

    private float PressedTime = 0.0f;
    private bool DashNow = false;

    // Start is called before the first frame update
    void Start()
    {
        UIControllerRef = GameObject.FindObjectOfType<UIController>();
        WallRight = GameObject.Find("Right-Wall");
        WallLeft = GameObject.Find("Left-Wall");
        IsColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();

        ListenDashMovements();

        ListenDodgeMovements();
    }

    void ListenDodgeMovements()
    {
        // listen to input
        if (Input.GetKey("a"))
        {
            if (!DashNow)
            {
                DodgeLeft(DodgeSpeed);
            }
        }

        if (Input.GetKey("d"))
        {
            DodgeRight();
        }
    }    

    void ListenDashMovements()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            float Difference = Time.time - PressedTime;
            if (Difference < 0.2f)
            {
                DashNow = true;
                DashToTheSide(DodgeDirection.Left);
            }
            PressedTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            float Difference = Time.time - PressedTime;
            if (Difference < 0.2f)
            {
                DashNow = true;
                DashToTheSide(DodgeDirection.Right);
            }
            PressedTime = Time.time;
        }
    }

    void MoveForward()
    {
        if (UIControllerRef.GetGameState() == GameState.Running)
        {
            transform.position = transform.position + new Vector3(0.0f, 0.0f, MoveForwardSpeed);
        }
    }

    void DodgeLeft(float Speed)
    {
        Transform LeftWallPosition = WallLeft.transform;
        if (transform.position.x > LeftWallPosition.position.x + 1)
        {
            transform.position = transform.position + new Vector3(-Speed, 0.0f, 0.0f);
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

    void DashToTheSide(DodgeDirection DodgeSide)
    {
        if (DashNow)
        {
            if (DodgeSide == DodgeDirection.Left)
            {
                Transform LeftWallPosition = WallLeft.transform;
                if (transform.position.x > LeftWallPosition.position.x + 1)
                {
                    transform.position = transform.position + new Vector3(-10 * DashSpeed, 0.0f, 0.0f);
                }
            }
            else
            {
                Transform RightWallPosition = WallRight.transform;
                if (transform.position.x < RightWallPosition.position.x - 1)
                {
                    transform.position = transform.position + new Vector3(10 * DashSpeed, 0.0f, 0.0f);
                }
            }
            DashNow = false;
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
