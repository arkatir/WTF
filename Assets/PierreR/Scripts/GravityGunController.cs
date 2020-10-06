using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunController : MonoBehaviour
{
    public GameObject player;

    private bool selected = true;
    private Vector3 rayOrigin;
    private Vector3 rayDirection;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void ToggleWeaponSelection()
    {
        selected = !selected;
        
    }

    // Update is called once per frame
    void Update()
    {
        rayOrigin = Camera.main.transform.position;
        rayDirection = Camera.main.transform.forward;

        Debug.DrawLine(rayOrigin, rayOrigin + rayDirection * 100, Color.red);
        /*if (Input.GetKey(KeyCode.G))
        {
            ToggleWeaponSelection();
        }*/

        if (selected && Input.GetMouseButtonDown(0))
        {
            Debug.Log("shoot!");
            RaycastHit shootingRaycast = new RaycastHit();
            bool touchedSomething = Physics.Raycast(rayOrigin, rayDirection, out shootingRaycast);
            Debug.Log("touchedSomething ? " + touchedSomething);
            if (touchedSomething)
            {
                Rigidbody objectTouchedRB = shootingRaycast.collider.GetComponent<Rigidbody>();
                if (objectTouchedRB)
                {
                    objectTouchedRB.transform.position = player.transform.position;
                }
            }     
        }
    }
}
