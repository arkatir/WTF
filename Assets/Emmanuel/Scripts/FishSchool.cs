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
            if (instance==null)
            {
                instance = GameObject.FindObjectOfType<FishSchool>();
            }
            return instance;
        } 
    }

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
            Vector3 startPosition = transform.position + Random.insideUnitSphere * Random.Range(-initRange, initRange);
            fishSchool[i] = (GameObject)Instantiate(fishPrefab, startPosition, Quaternion.identity);
        }
    }

    void ChangeGoal()
    {
        if(Random.Range(0,5000) < 50)
        {
            goal = transform.position + Random.insideUnitSphere * Random.Range(-goalRange, goalRange);
            //goal = new Vector3(Random.Range(-goalRange, goalRange), Random.Range(0, goalRange), Random.Range(-goalRange, goalRange));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, initRange);
    }
}
