using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsProjectile : MonoBehaviour
{
    #region Private attributes
    public enum directedTowards { Player, Enemy, Environment };
    [SerializeField]
    private float startingForce; //Not susceptible to change during runtime     
    private Rigidbody rb;
    #endregion

    #region Public attributes
    public int damage;
    public directedTowards targetToHit; //To know if we have to call player health script or enemy health script
    public GameObject hitPrefab; //Instantiate hit particle effect object on hitting something
    public int timeToDeactivate;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        //rb.isKinematic = true;
    }

    private void OnEnable()
    {
        if (rb || (rb = this.GetComponent<Rigidbody>()))
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
            Vector3 directionalForce = Vector3.zero + transform.forward*startingForce;
            rb.AddForce(directionalForce, ForceMode.Impulse);
        }
        StartCoroutine(CountdownToDeactivate());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        ContactPoint contact = other.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, pos, rot);
            var psHit = hitVFX.GetComponent<ParticleSystem>();
            if (psHit != null)
            {
                Destroy(hitVFX, psHit.main.duration);
            }
            else
            {
                var psChild = hitVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitVFX, psChild.main.duration);
            }
        }
        switch (targetToHit)
        {
            case directedTowards.Player:
                if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    PlayerStats healthManager = other.gameObject.GetComponent<PlayerStats>();
                    if (healthManager)
                    {
                        healthManager.RemoveHealth(damage);
                    }
                }
                break;
            case directedTowards.Enemy:
                if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyStats enemyManager = other.gameObject.GetComponent<EnemyStats>();
                    if (enemyManager)
                    {
                        enemyManager.RemoveHealth(damage);
                    }
                }
                break;
            case directedTowards.Environment:
                if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
                {
                    //If we hit a gameobject with tag environment
                }
                break;
                default:
                break;
        }
        //rb.isKinematic = true;
        ObjectPoolManager.managerInstance.RemoveObject(this.gameObject);
        StopAllCoroutines();
    }
    /// <summary>
    /// Ensures that projectile is deactivated after travelling a set amount of time after hitting nothing.
    /// </summary>
    /// <returns></returns>
    /// 
    private IEnumerator CountdownToDeactivate()
    {
        yield return new WaitForSeconds(timeToDeactivate);
        //rb.isKinematic = true;
        ObjectPoolManager.managerInstance.RemoveObject(this.gameObject);
        yield return null;
    }
}
