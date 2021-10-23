using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum RotateAxis
{ 
    XAxis,
    YAxis,
    ZAxis
};

public class Obstacle : MonoBehaviour
{
    private RotateAxis RotationAxis;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RotateObstacle());
        DecideRotationAxis();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // https://stackoverflow.com/a/37588536
    IEnumerator RotateObstacle()
    {
        //while (true)
        //{
        //    yield return new WaitForSeconds(0.5f);
        //    Vector3 CurrentRotation = transform.eulerAngles;
        //    Vector3 NewRotation = CurrentRotation + new Vector3(CurrentRotation.x, CurrentRotation.y, CurrentRotation.z + 2);
        //    transform.eulerAngles = NewRotation;
        //}

        while (true)
        {
            if (RotationAxis == RotateAxis.XAxis)
            {
                transform.Rotate(10.0f * Time.deltaTime, 0, 0);
            }

            if (RotationAxis == RotateAxis.YAxis)
            {
                transform.Rotate(0, 10.0f * Time.deltaTime, 0);
            }

            if (RotationAxis == RotateAxis.ZAxis)
            {
                transform.Rotate(0, 0, 10.0f * Time.deltaTime);
            }
            yield return null;
        }
    }

    void DecideRotationAxis()
    {
        int RandomAxis = Random.Range(1, 4);
        if (RandomAxis == 1)
        {
            RotationAxis = RotateAxis.XAxis;
            transform.Rotate(10.0f * Time.deltaTime, 0, 0);
        }

        if (RandomAxis == 2)
        {
            RotationAxis = RotateAxis.YAxis;
            transform.Rotate(0, 10.0f * Time.deltaTime, 0);
        }

        if (RandomAxis == 3)
        {
            RotationAxis = RotateAxis.ZAxis;
            transform.Rotate(0, 0, 10.0f * Time.deltaTime);
        }
    }
}
