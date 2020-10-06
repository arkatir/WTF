using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutoShooting : MonoBehaviour
{

    public Transform shootingOriginPoint;
    public GameObject projectileToShoot;

    private string projectileName;
    // Start is called before the first frame update
    void Start()
    {
        projectileName = projectileToShoot.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ObjectPoolManager.managerInstance.CreateObject(projectileName, shootingOriginPoint.position, shootingOriginPoint.rotation);
        }
    }

    
}
