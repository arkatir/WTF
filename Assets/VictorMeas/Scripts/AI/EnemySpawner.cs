using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
                ObjectPoolManager.managerInstance.CreateObject(mobToSpawn.name, GetRandomLocation(), transform.rotation);
                currentTimer = 0f;
                return;
            }
            else
            {
                currentTimer += Time.deltaTime;
            }
        }
    }

    Vector3 GetRandomLocation()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        // Pick the first indice of a random triangle in the nav mesh
        int t = Random.Range(0, navMeshData.indices.Length - 3);

        // Select a random point on it
        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
        Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);

        return point;
    }
}
