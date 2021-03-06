﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class GravityGunController : SlotItem
{
    public float floatingForce = 23f;
    public Rigidbody objSelRigidB;
    public LineRenderer particles;
    public float pushForce = 15f;
    public float throwWeaponForce = 10f;
    public float deltaDistance = 1f;
    public float xRotationToPlayer = -14.127f;
    public float yRotationToPlayer = -20.579f;
    public Vector3 positionToPlayer = new Vector3(0.9f, -1.1f, 1.3f);
    public float objectHoldInitialDistance = 5f;
    public float objectHoldMinDistance = 2f;
    public float objectHoldDragSpeed = 1f;
    public AudioSource gravityGunHold;

    private bool selected = false;
    private RaycastHit shootingRaycast;
    private Vector3 rayOrigin;
    private Vector3 rayDirection;
    private bool objectHold = false;
    private Vector3 objSelCoordToTendTo;
    private Quaternion rotationToPlayer;
    private float objectHoldDistance;

    private void Start()
    {
        rotationToPlayer = Quaternion.Euler(xRotationToPlayer, yRotationToPlayer, 0);
        objectHoldDistance = objectHoldInitialDistance;
    }
    void FixedUpdate()
    {
        if (objectHold)
            HandleObject();
    }

    // Update is called once per frame
    void Update() {

        rayOrigin = Camera.main.transform.position;
        rayDirection = Camera.main.transform.forward;

        //Debug.DrawLine(rayOrigin, rayOrigin + rayDirection * 100, Color.red);

        CheckIfObjectHoldStillExists();

        if (objectHold)
             GenerateBeamShape();

        if (selected && Input.GetMouseButtonDown(0))
        {
            if (!objectHold)
                PickUpObject();
            else
                ThrowObject();
        }

        if (selected && Input.GetMouseButton(1) && objectHoldDistance > objectHoldMinDistance)
            objectHoldDistance -= Time.deltaTime * objectHoldDragSpeed;
    }

    private void GenerateBeamShape()
    {
        Vector3 end = particles.transform.InverseTransformPoint(objSelRigidB.transform.position);
        for (int i = 0; i < particles.positionCount; i++)
        {
            float t = (float)i / (float)(particles.positionCount - 1);
            float t2 = t;
            if (t > 0.5)
                t2 = 1 - t;
            Vector3 p = Vector3.Lerp(Vector3.zero, end, t);
            p.y += Mathf.Sin((Time.time + t) * 3) / 300 * t2 * 2;
            particles.SetPosition(i, p);
        }
    }

    private void CheckIfObjectHoldStillExists()
    {
        if (objectHold && objSelRigidB == null)
            DropObject();
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
                particles.gameObject.SetActive(true);
                objectHoldDistance = objectHoldInitialDistance;
                EventManager.TriggerEvent("gravityGun");
                gravityGunHold.Play();
                gravityGunHold.loop = true;
            }
        }
    }

    private void ThrowObject()
    {
        objSelRigidB.AddForce(Camera.main.transform.forward * pushForce, ForceMode.Impulse);
        DropObject();
    }

    private void DropObject()
    {
        objSelRigidB = null;
        objectHold = false;
        particles.gameObject.SetActive(false);
        gravityGunHold.loop = false;
        gravityGunHold.Stop();
    }

    private void HandleObject()
    {
        SelectedObjectTargetComputation();
        Vector3 vdist = objSelCoordToTendTo - objSelRigidB.transform.position;
        objSelRigidB.AddForce(vdist * 20 * objSelRigidB.mass);
        objSelRigidB.velocity = vdist.normalized * Mathf.Min(20, objSelRigidB.velocity.magnitude);
        if (vdist.magnitude < deltaDistance)
        {
            objSelRigidB.velocity /= 2;
        }
    }

    private void SelectedObjectTargetComputation()
    {
        objSelCoordToTendTo = Camera.main.transform.position + Camera.main.transform.forward * objectHoldDistance;
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
        DropObject();
        selected = false;
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<MeshCollider>().enabled = true;
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwWeaponForce, ForceMode.Impulse);
    }
}