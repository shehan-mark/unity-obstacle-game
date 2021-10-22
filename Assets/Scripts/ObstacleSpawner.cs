using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Plane Player;
    public GameObject SpawnItem;
    public GameObject RowDetectionRow;
    public GameObject LeftWall, RightWall;
    public UIController UIControllerRef;

    public float SpawnSpeed = 0.5f; // This will take care of the spawn Z distance between obstacles
    public float ForwardDistanceToPlayer = 10.0f;
    public float XSpawnAmount = 5.0f;
    public int SpacesToGoThrough = 2;

    private float LeftWallWithPadding;
    private float RightWallWithPadding;
    private List<float> SpawnXAxisLocations;
    private List<GameObject> ObjectPool;
    private List<GameObject> RowDetectionPool;
    private int SpawnedRows = 0;

    // Start is called before the first frame update
    void Start()
    {

        initObstacleSpawner();
        StartCoroutine(SpawnDelay());

        ObjectPool = new List<GameObject>();
        RowDetectionPool = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIControllerRef.GetGameState() == GameState.Running)
        {
            if (Player)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, Player.transform.position.z + ForwardDistanceToPlayer);
            }
            else
            {
                Player = GameObject.FindObjectOfType<Plane>();
            }
        }
    }

    void initObstacleSpawner()
    {
        LeftWallWithPadding = LeftWall.transform.position.x;
        RightWallWithPadding = RightWall.transform.position.x - 2;
        SpawnXAxisLocations = new List<float>();

        float MiddleGroundDifference = RightWallWithPadding - LeftWallWithPadding;
        //print($"Middle Ground Difference {MiddleGroundDifference}");
        float ObstacleSpace = MiddleGroundDifference / XSpawnAmount;
        float value = LeftWallWithPadding;
        for (int i = 0; i < XSpawnAmount; i++)
        {
            value += ObstacleSpace;
            //print($"Adding value to obstacle {value}");
            SpawnXAxisLocations.Add(value);
        }
    }

    void SpawnAhead()
    {
        if (UIControllerRef.GetGameState() != GameState.Running)
        {
            return;
        }

        print($"LOG: Spawning Row - {RowDetectionPool.Count}");
        Quaternion SpawnRotation = Quaternion.Euler(0, 0, 0); // I do not want to rotate anything for now
        float Spawned = 0;
        List<int> SpawnedIndex = new List<int>();
        while (Spawned < XSpawnAmount - SpacesToGoThrough)
        {
            int index = (int)Random.Range(0, SpawnXAxisLocations.Count);
            if (!SpawnedIndex.Contains(index))
            {
                float XPoint = SpawnXAxisLocations[index];
                Vector3 SpawnLocation = new Vector3(XPoint, transform.position.y, transform.position.z);

                Vector3 RowDetectionRowSpawnLoc = new Vector3(transform.position.x, transform.position.y, transform.position.z);

                GameObject ObstacleObj = null;
                GameObject RowDetectionObj = null;
                if (SpawnedRows < 4)
                {
                    ObstacleObj = Instantiate(SpawnItem, SpawnLocation, SpawnRotation);
                    if (Spawned == 0)
                    {
                        RowDetectionObj = Instantiate(RowDetectionRow, RowDetectionRowSpawnLoc, SpawnRotation);
                    }
                }
                else
                {
                    ObstacleObj = ObjectPool[0];
                    ObjectPool.RemoveAt(0);
                    ObstacleObj.transform.position = SpawnLocation;

                    if (Spawned == 0)
                    {
                        RowDetectionObj = RowDetectionPool[0];
                        RowDetectionPool.RemoveAt(0);
                        RowDetectionObj.transform.position = RowDetectionRowSpawnLoc;
                    }
                }

                ObjectPool.Add(ObstacleObj);
                if (RowDetectionObj)
                {
                    RowDetectionPool.Add(RowDetectionObj);
                }

                SpawnedIndex.Add(index);
                Spawned++;
                    
            }
        }
        SpawnedRows++;
    }

    IEnumerator SpawnDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(SpawnSpeed);
            SpawnAhead();
        }
    }
}
