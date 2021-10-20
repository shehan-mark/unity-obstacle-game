using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{
    public Plane PlaneObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
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
            // player not found so restarting the game
            StartCoroutine(RestartGame());
        }
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2);
        print("LOG:: You are dead. Restarting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
