using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Very dumb script to check if enemy is near player
/// </summary>
public class EnemyDetection : MonoBehaviour
{
    public bool isDetected = false;
    public Vector3 lastSavedPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        isDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isDetected = true;
            lastSavedPosition = other.transform.position;
        }
    }

    //Lose sight of player
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isDetected = false;
        }
    }
}
