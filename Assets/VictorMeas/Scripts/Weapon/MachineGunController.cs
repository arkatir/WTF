using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunController : SlotItem
{

    private bool selected = false;
    public float throwWeaponForce = 10f;
    private Quaternion rotationToPlayer;
    public float xRotationToPlayer = 5.359f;
    public float yRotationToPlayer = -88.231f;
    public float zRotationToPlayer = -4.898f;
    public Vector3 positionToPlayer = new Vector3(0.525f, -1.166f, 0.708f);

    private Rigidbody playerRb;

    //Gun info
    public GameObject projectileToShoot;
    public Transform shootingOriginPoint;
    public Transform farAwayDirection;
    public int maxBullets = 30;
    public int currentBullets = 30;
    public float rateOfFire = 0.5f;
    private float currentTime;
    private string projectileName;
    private int layerMask;
    //Animator
    public Animator gunAnimator;
    public ParticleSystem psMuzzle;
    private bool isReloading = false;

    void Start()
    {
        layerMask = 1 << 9;
        rotationToPlayer = Quaternion.Euler(xRotationToPlayer, yRotationToPlayer, zRotationToPlayer);
        projectileName = projectileToShoot.name;
        farAwayDirection = Camera.main.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            TransmitSpeed();
            CheckShoot();
            CheckReload();
        }
        
    }

    public void TransmitSpeed()
    {
        if (gunAnimator)
        {
            float speedX = Input.GetAxis("Horizontal");
            float speedZ = Input.GetAxis("Vertical");
            gunAnimator.SetFloat("SpeedX", speedX);
            gunAnimator.SetFloat("SpeedZ", speedZ);
        }
    }

    public void CheckShoot()
    {
        if (isReloading == false)
        {
            if (Input.GetMouseButton(0) && (currentTime > rateOfFire) && (currentBullets > 0))
            {
                RaycastHit HitInfo;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out HitInfo, Mathf.Infinity, layerMask))
                {
                    shootingOriginPoint.LookAt(HitInfo.point);
                    ObjectPoolManager.managerInstance.CreateObject(projectileName, shootingOriginPoint.position, shootingOriginPoint.rotation);
                }
                else
                {
                    shootingOriginPoint.LookAt(farAwayDirection.position);
                    ObjectPoolManager.managerInstance.CreateObject(projectileName, shootingOriginPoint.position, shootingOriginPoint.rotation);
                }
                if (gunAnimator)
                {
                    gunAnimator.SetTrigger("Shoot");
                    psMuzzle.Play();
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
    }

    public void CheckReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentBullets<maxBullets && !isReloading)
        {
            isReloading = true;
            StartCoroutine(ReloadGun());
        }
    }

    private IEnumerator ReloadGun()
    {
        
        gunAnimator.SetTrigger("Reload"); //launch  anim
        //Fetch the current Animation clip information for the base layer
        var m_CurrentClipInfo = gunAnimator.GetCurrentAnimatorClipInfo(0);
        //Access the current length of the clip
        var m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
        yield return new WaitForSeconds(m_CurrentClipLength + 1f); //Wait for end of clip before you can shoot
        currentBullets = maxBullets;
        isReloading = false;
        yield return null;
    }

    public override void OnInsert()
    {
        isReloading = false;
        playerRb = Camera.main.transform.parent.GetComponent<Rigidbody>();
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
        currentBullets = maxBullets;
        selected = false;
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponentInChildren<MeshCollider>().enabled = true;
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwWeaponForce, ForceMode.Impulse);
    }
}
