using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSchool : MonoBehaviour
{
    private static FishSchool instance = null;
    public static FishSchool sharedInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<FishSchool>();
            }
            return instance;
        }
    }
    public float initRangeX = 20;
    public float initRangeY = 30;
    public float initRangeZ = 80;

    public float goalRangeX = 20;
    public float goalRangeY = 30;
    public float goalRangeZ = 80;

    public float initRange = 10;
    public float goalRange = 40;

    public float PlayerDetectionDistance = 15;
    public GameObject fishPrefab;
    public int fishNumber = 10;
    public GameObject[] fishSchool;

    public static Vector3 goal = Vector3.zero;

    void Start()
    {
        fishSchool = new GameObject[fishNumber];
        InstantiateSchool();
    }

    void Update()
    {
        ChangeGoal();
    }

    void InstantiateSchool()
    {
        for (int i = 0; i < fishNumber; i++)
        {
            //Vector3 startPosition = transform.position + Random.insideUnitSphere * Random.Range(-initRange, initRange);
            Vector3 startPosition = transform.position + new Vector3(Random.Range(0, initRangeX), Random.Range(0, initRangeY), Random.Range(0, initRangeZ));
            fishSchool[i] = (GameObject)Instantiate(fishPrefab, startPosition, Quaternion.identity);
        }
    }

    void ChangeGoal()
    {
        if (Random.Range(0, 5000) < 50)
        {
            //goal = transform.position + Random.insideUnitSphere * Random.Range(-goalRange, goalRange);
            goal = transform.position + new Vector3(Random.Range(0, goalRangeX), Random.Range(0, goalRangeY), Random.Range(0, goalRangeZ));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Mesh goalZone = new Mesh();
        Vector3[] vertices = {
            new Vector3 (0, 0, 0),
            new Vector3 (goalRangeX, 0, 0),
            new Vector3 (goalRangeX, goalRangeY, 0),
            new Vector3 (0, goalRangeY, 0),
            new Vector3 (0, goalRangeY, goalRangeZ),
            new Vector3 (goalRangeX, goalRangeY, goalRangeZ),
            new Vector3 (goalRangeX, 0, goalRangeZ),
            new Vector3 (0, 0, goalRangeZ),
};

        int[] triangles = {
            0, 2, 1, //face front
			0, 3, 2,
            2, 3, 4, //face top
			2, 4, 5,
            1, 2, 5, //face right
			1, 5, 6,
            0, 7, 4, //face left
			0, 4, 3,
            5, 4, 7, //face back
			5, 7, 6,
            0, 6, 7, //face bottom
			0, 1, 6
        };

        goalZone.vertices = vertices;
        goalZone.triangles = triangles;
        goalZone.Optimize();
        goalZone.RecalculateNormals();

        Gizmos.DrawWireMesh(goalZone, transform.position);
       // Gizmos.DrawWireSphere(transform.position, goalRange);
    }
}
