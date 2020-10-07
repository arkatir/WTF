using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTimer;
    public float maxSpawnRadius;

    private float currentTimer;

    public GameObject mobToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        currentTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForSpawn();
    }

    public void CheckForSpawn()
    {
        if (mobToSpawn)
        {
            if(currentTimer >= spawnTimer)
            {
                ObjectPoolManager.managerInstance.CreateObject(mobToSpawn.name, transform.position, transform.rotation);
                currentTimer = 0f;
                return;
            }
            else
            {
                currentTimer += Time.deltaTime;
            }
        }
    }
}
