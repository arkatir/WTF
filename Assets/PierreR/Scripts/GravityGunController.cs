using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunController : SlotItem
{
    public float floatingForce = 1f;
    public Rigidbody objSelRigidB;
    public float floatingPushInterval = 1;
    public float floatingMass = 0.1f;
    public float pushForce = 100f;
    public float deltaDistance = 1f;
    public float xRotationToPlayer = -14.127f;
    public float yRotationToPlayer = -20.579f;
    public Vector3 positionToPlayer = new Vector3(0.9f, -1.1f, 1.3f);

    private bool selected = false;
    private RaycastHit shootingRaycast;
    private Vector3 rayOrigin;
    private Vector3 rayDirection;
    private bool objectHold = false;
    private Vector3 objSelCoordToTendTo;
    private Quaternion rotationToPlayer;

    private void Start()
    {
        rotationToPlayer = Quaternion.Euler(xRotationToPlayer, yRotationToPlayer, 0);
    }
    void FixedUpdate()
    {
        if (objectHold)
        {
            HandleObject();
        }
    }

    // Update is called once per frame
    void Update() {

        rayOrigin = Camera.main.transform.position;
        rayDirection = Camera.main.transform.forward;

        Debug.DrawLine(rayOrigin, rayOrigin + rayDirection * 100, Color.red);

        if (selected && Input.GetMouseButtonDown(0)) {
            if (!objectHold)
            {
                PickUpObject();
            } else {
                ThrowObject();
            }
        }    
    }

    private void PickUpObject()
    {
        bool touchedSomething = Physics.Raycast(rayOrigin, rayDirection, out shootingRaycast);
        if (touchedSomething && !objectHold)
        {
            Rigidbody objectTouchedRB = shootingRaycast.collider.GetComponent<Rigidbody>();
            if (objectTouchedRB != null)
            {
                objectHold = true;
                objSelRigidB = objectTouchedRB;
            }
        }
    }

    private void ThrowObject()
    {
        objSelRigidB.AddForce(Camera.main.transform.forward * pushForce, ForceMode.Impulse);
        objSelRigidB = null;
        objectHold = false;
    }

    private void HandleObject()
    {
        objSelCoordToTendTo = Camera.main.transform.position + Camera.main.transform.forward * 5;
        Vector3 vdist = objSelCoordToTendTo - objSelRigidB.transform.position;
        objSelRigidB.AddForce(vdist * 20 * objSelRigidB.mass);
        objSelRigidB.velocity = vdist.normalized * Mathf.Min(20, objSelRigidB.velocity.magnitude);
        if (vdist.magnitude < deltaDistance)
        {
            objSelRigidB.velocity /= 2;
        }
    }

    public override void OnInsert()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MeshCollider>().enabled = false;
        transform.SetParent(Camera.main.transform);
        
        transform.localPosition = positionToPlayer;
        transform.localRotation = rotationToPlayer;
        selected = true;
    }

    public override void OnRemove()
    {
        selected = false;
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<MeshCollider>().enabled = true;
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward, ForceMode.Impulse);
    }
}