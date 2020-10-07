using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunController : SlotItem
{

    private bool selected = false;
    public float throwWeaponForce = 10f;
    private Quaternion rotationToPlayer;
    public float xRotationToPlayer = -1.196f;
    public float yRotationToPlayer = -84.201f;
    public Vector3 positionToPlayer = new Vector3(0.001344124f, -0.6869054f, 0.51004f);


    //Gun info
    public GameObject projectileToShoot;
    public Transform shootingOriginPoint;
    public Transform farAwayDirection;
    public int maxBullets = 30;
    public int currentBullets = 30;
    public float rateOfFire = 0.5f;
    private float currentTime;
    private string projectileName;
    // Start is called before the first frame update
    void Start()
    {
        rotationToPlayer = Quaternion.Euler(xRotationToPlayer, yRotationToPlayer, 0);
        projectileName = projectileToShoot.name;
        farAwayDirection = Camera.main.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            CheckShoot();
            CheckReload();
        }
        
    }

    public void CheckShoot()
    {
        if (Input.GetMouseButton(0) && (currentTime > rateOfFire) && (currentBullets>0))
        {
            RaycastHit HitInfo;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out HitInfo, 1000.0f))
            {
                shootingOriginPoint.LookAt(HitInfo.point);
                ObjectPoolManager.managerInstance.CreateObject(projectileName, shootingOriginPoint.position, shootingOriginPoint.rotation);
            }
            else
            {
                shootingOriginPoint.LookAt(farAwayDirection.position);
                ObjectPoolManager.managerInstance.CreateObject(projectileName, shootingOriginPoint.position, shootingOriginPoint.rotation);
            }
                
            currentTime = 0f;
            currentBullets -= 1;
            return;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
        
    }

    public void CheckReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentBullets = maxBullets;
        }
    }

    public override void OnInsert()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponentInChildren<MeshCollider>().enabled = false;
        transform.SetParent(Camera.main.transform);

        transform.localPosition = positionToPlayer;
        transform.localRotation = rotationToPlayer;
        selected = true;
    }

    public override void OnRemove()
    {
        StopAllCoroutines();
        selected = false;
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponentInChildren<MeshCollider>().enabled = true;
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwWeaponForce, ForceMode.Impulse);
    }
}
