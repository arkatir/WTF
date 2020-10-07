using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeProjectile : MonoBehaviour
{
    #region Private attributes
    public enum directedTowards { Player, Enemy, Environment };
    #endregion

    #region Public attributes
    public int damage;
    public directedTowards targetToHit; //To know if we have to call player health script or enemy health script
    public GameObject hitPrefab; //Instantiate hit particle effect object on hitting something
    public float timeToDeactivate;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnEnable()
    {
        StartCoroutine(CountdownToDeactivate());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (targetToHit)
        {
            case directedTowards.Player:
                if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
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
        ObjectPoolManager.managerInstance.RemoveObject(this.gameObject);
        yield return null;
    }
}
