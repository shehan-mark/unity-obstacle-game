using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public GameObject Player;
    public float DestroyDistance = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.z - transform.position.z > DestroyDistance)
        {
            Destroy(this.gameObject);
        }
    }
}
