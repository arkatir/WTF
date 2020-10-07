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
    private bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && active)
        {
            float spawnRadius = (spawnNumber - 1) / 2;
            float spawnAngle = (360) / spawnNumber;
            spawnPos.transform.position = new Vector3(spawnRadius, 0, 0) + transform.position;
            for (int i = 0; i < Mathf.Min(spawnNumber, 10); i++)
            {
                GameObject spawn = Instantiate(autoDestroyPrefab, spawnPos.transform.position + spawnOffset, autoDestroyPrefab.transform.rotation);
                autoDestroy = spawn.GetComponent<AutoDestroy>();
                autoDestroy.target = other.gameObject;
                autoDestroy.speedCap = 2 * Mathf.Sqrt(spawnNumber);
                autoDestroy.timer = Random.Range(2.5f, 3.5f);
                spawnPos.transform.RotateAround(transform.position, Vector3.up, spawnAngle);
            }
            spawnNumber++;
            transform.localScale *= 1.2f;
            StartCoroutine(Deactivate());
        }
        
    }

    IEnumerator Deactivate()
    {
        active = false;
        yield return new WaitForSeconds(5);
        active = true;
    }
}
