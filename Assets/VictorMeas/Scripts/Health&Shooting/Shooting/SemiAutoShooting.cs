using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutoShooting : MonoBehaviour
{

    public Transform shootingOriginPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ObjectPoolManager.managerInstance.CreateObject(shootingOriginPoint.position, shootingOriginPoint.rotation);
        }
    }

    
}
