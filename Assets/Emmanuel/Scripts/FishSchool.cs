using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSchool : MonoBehaviour
{
    public float range = 10;
    public GameObject fishPrefab;
    public int fishNumber = 10;
    public static GameObject[] fishSchool { get; set; }

    public static Vector3 goal = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        fishSchool = new GameObject[fishNumber];
        InstantiateSchool();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeGoal();
    }

    void InstantiateSchool()
    {
        for (int i = 0; i < fishNumber; i++)
        {
            Vector3 startPosition = new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
            fishSchool[i] = (GameObject)Instantiate(fishPrefab, startPosition, Quaternion.identity);
        }
    }

    void ChangeGoal()
    {
        if(Random.Range(0,1000) < 50)
        {
            goal = new Vector3(Random.Range(-range, range), Random.Range(-range, range), Random.Range(-range, range));
        }
    }
    void Separation()
    {

    }

    void Alignment()
    {

    }

    void Collision()
    {

    }
}
