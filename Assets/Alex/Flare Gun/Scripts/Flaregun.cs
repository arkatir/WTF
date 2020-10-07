using UnityEngine;
using System.Collections;

public class Flaregun : SlotItem
{

    public Transform Player;
    public Rigidbody flareBullet;
    public Transform barrelEnd;
    public GameObject muzzleParticles;
    public AudioClip flareShotSound;
    public AudioClip noAmmoSound;
    public AudioClip reloadSound;
    public int bulletSpeed = 2000;
    public int maxSpareRounds = 1000;
    public int spareRounds = 1000;
    public int currentRound = 1;
    public float xRotationToPlayer = -6f;
    public float yRotationToPlayer = -12f;
    public Vector3 positionToPlayer = new Vector3(0.9f, -1.1f, 1.3f);
    private Quaternion rotationToPlayer;
    private bool pickup = false;
    // Update is called once per frame

    private void Start()
    {
        rotationToPlayer = Quaternion.Euler(xRotationToPlayer, yRotationToPlayer, 0);
    }
    void Update()
    {

        if (Input.GetButtonDown("Fire1") && !GetComponent<Animation>().isPlaying && pickup)
        {
            if (currentRound > 0)
            {
                Shoot();
            }
            else
            {
                GetComponent<Animation>().Play("noAmmo");
                GetComponent<AudioSource>().PlayOneShot(noAmmoSound);
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && !GetComponent<Animation>().isPlaying)
        {
            Reload();

        }

    }

    void Shoot()
    {
        currentRound--;
        if (currentRound <= 0)
        {
            currentRound = 0;
        }

        GetComponent<Animation>().CrossFade("Shoot");
        GetComponent<AudioSource>().PlayOneShot(flareShotSound);


        Rigidbody bulletInstance;
        bulletInstance = Instantiate(flareBullet, barrelEnd.position, barrelEnd.rotation) as Rigidbody; //INSTANTIATING THE FLARE PROJECTILE
        bulletInstance.GetComponent<flarebullet>().Player = GameObject.Find("Player").transform;


        bulletInstance.AddForce(barrelEnd.forward * bulletSpeed); //ADDING FORWARD FORCE TO THE FLARE PROJECTILE

        Instantiate(muzzleParticles, barrelEnd.position, barrelEnd.rotation);    //INSTANTIATING THE GUN'S MUZZLE SPARKS	

    }

    void Reload()
    {
        if (spareRounds >= 1 && currentRound == 0)
        {
            GetComponent<AudioSource>().PlayOneShot(reloadSound);
            spareRounds--;
            currentRound++;
            GetComponent<Animation>().CrossFade("Reload");
        }

    }
    
    private void enableColliders(bool enable)
    {
        MeshCollider[] cols = GetComponentsInChildren<MeshCollider>();
        foreach (MeshCollider c in cols)
            c.enabled = enable;
    }

    public override void OnInsert()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        MeshCollider[] cols = GetComponentsInChildren<MeshCollider>();
        enableColliders(false);
        transform.SetParent(Camera.main.transform);

        transform.localPosition = positionToPlayer;
        transform.localRotation = rotationToPlayer;
        pickup = true;
    }

    public override void OnRemove()
    {
        pickup = false;
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
        enableColliders(true);
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward, ForceMode.Impulse);
    }
}
