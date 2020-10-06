﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : MonoBehaviour
{
    #region Private attributes
    public enum directedTowards { Player, Enemy, Environment };
    [SerializeField]
    private float projectileSpeed; //Not susceptible to change during runtime           
    private float currentSpeed;
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
        currentSpeed = projectileSpeed;
        
    }

    private void OnEnable()
    {
        currentSpeed = projectileSpeed;
        StartCoroutine(CountdownToDeactivate());
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSpeed != 0)
        {
            transform.position += transform.forward * (currentSpeed * Time.deltaTime);
        }
        else
        {
            Debug.LogWarning("Projectile has no ascribed speed in editor!");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        currentSpeed = 0; //So our object wont be moving on deactivation
        ContactPoint contact = other.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if(hitPrefab != null)
        {
            var hitVFX = Instantiate(hitPrefab, pos, rot);
            var psHit = hitVFX.GetComponent<ParticleSystem>();
            if(psHit != null)
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
                PlayerStats healthManager = other.gameObject.GetComponent<PlayerStats>();
                if (healthManager)
                {
                    healthManager.RemoveHealth(damage);
                }
                break;
            case directedTowards.Enemy:
                break;
            case directedTowards.Environment:
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
