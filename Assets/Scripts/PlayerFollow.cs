using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public UIController UIControllerRef;
    private Plane PlaneObj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (UIControllerRef.GetGameState() == GameState.Running)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        if (PlaneObj)
        {
            Transform PlaneObjectTransform = PlaneObj.GetComponent<Transform>();
            float PlaneZDifference = PlaneObjectTransform.position.z - transform.position.z;
            transform.position = transform.position + new Vector3(0.0f, 0.0f, PlaneZDifference);
        }
        else
        {
            PlaneObj = GameObject.FindObjectOfType<Plane>();
        }
    }

}
