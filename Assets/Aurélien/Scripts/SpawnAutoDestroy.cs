using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAutoDestroy : MonoBehaviour
{
    public GameObject autoDestroyPrefab;
    private AutoDestroy autoDestroy;
    private Vector3 spawnOffset = new Vector3(0, 0.8f, 0);
    private int spawnNumber = 1;
    public GameObject spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            float spawnRadius = (spawnNumber - 1) / 2;
            float spawnAngle = (360) / spawnNumber;
            spawnPos.transform.position = new Vector3(spawnRadius, 0, 0);
            for (int i = 0; i < spawnNumber; i++)
            {
                GameObject spawn = Instantiate(autoDestroyPrefab, spawnPos.transform.position + spawnOffset, autoDestroyPrefab.transform.rotation);
                autoDestroy = spawn.GetComponent<AutoDestroy>();
                autoDestroy.target = other.gameObject;
                autoDestroy.speedCap = Mathf.Sqrt(spawnNumber);
                spawnPos.transform.RotateAround(Vector3.zero, Vector3.up, spawnAngle);
            }
            spawnNumber++;
            transform.localScale *= 1.2f;
        }
    }
}
