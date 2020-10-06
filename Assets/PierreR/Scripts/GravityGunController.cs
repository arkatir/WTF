using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunController : MonoBehaviour
{
    public GameObject player;
    public float floatingForce = 1f;
    public Rigidbody objSelRigidB;
    public float floatingPushInterval = 1;
    public float floatingMass = 0.1f;
    public float pushForce = 100f;
    public float deltaDistance = 1f;

    private bool selected = true;
    private RaycastHit shootingRaycast;
    private Vector3 rayOrigin;
    private Vector3 rayDirection;
    private bool objectHold = false;
    private Vector3 objSelCoordToTendTo;

    void FixedUpdate()
    {
        if (objectHold)
        {
            objSelCoordToTendTo = Camera.main.transform.position + Camera.main.transform.forward * 5;
            Vector3 vdist = objSelCoordToTendTo - objSelRigidB.transform.position;
            objSelRigidB.AddForce(vdist * 20 * objSelRigidB.mass);
            objSelRigidB.velocity = vdist.normalized * Mathf.Min(20,objSelRigidB.velocity.magnitude);   
            if (vdist.magnitude < deltaDistance)
            {
                objSelRigidB.velocity /= 2;
            }
        }
    }

    // Update is called once per frame
    void Update() {

        rayOrigin = Camera.main.transform.position;
        rayDirection = Camera.main.transform.forward;

        Debug.DrawLine(rayOrigin, rayOrigin + rayDirection * 100, Color.red);
        
        if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("TODO : Throw");
        }

        if (selected && Input.GetMouseButtonDown(0)) {
            
            if (!objectHold)
            {
                bool touchedSomething = Physics.Raycast(rayOrigin, rayDirection, out shootingRaycast);
                if (touchedSomething && !objectHold)
                {
                    Rigidbody objectTouchedRB = shootingRaycast.collider.GetComponent<Rigidbody>();
                    if (objectTouchedRB)
                    {
                        objectHold = true;
                        objSelRigidB = objectTouchedRB;
                    }
                }
            } else {
                Debug.Log("PUSH !");
                objSelRigidB.AddForce(Camera.main.transform.forward * pushForce, ForceMode.Impulse);
                objSelRigidB = null;
                objectHold = false;
            }
        }    
    }
}