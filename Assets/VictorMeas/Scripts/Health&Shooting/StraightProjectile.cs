using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : MonoBehaviour
{
    #region Private attributes
    [SerializeField]
    private float projectileSpeed; //Not susceptible to change during runtime           
    private float currentSpeed;
    #endregion

    #region Public attributes
    public GameObject hitPrefab; //Instantiate hit particle effect object on hitting something
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = projectileSpeed;
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

    }
}
