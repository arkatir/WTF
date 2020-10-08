using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FlyingBoardController : SlotItem
{
    public GameObject player;
    public Vector3 positionToCamera = new Vector3(0f, 0f, 1.62f);
    public float xRotationToCamera = 25f;
    public float yRotationToCamera = 180f;
    public float throwVehiculeForce = 10f;

    private Quaternion rotationToCamera;

    private void Start()
    {
        rotationToCamera = Quaternion.Euler(xRotationToCamera, yRotationToCamera, 0);
    }
    public override void OnInsert()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MeshCollider>().enabled = false;
        transform.SetParent(Camera.main.transform);

        transform.localPosition = positionToCamera;
        transform.localRotation = rotationToCamera;

        RigidbodyFirstPersonController script = player.GetComponent<RigidbodyFirstPersonController>();
        if (script != null)
            script.jetpackMode = true;
    }

    public override void OnRemove()
    {
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<MeshCollider>().enabled = true;

        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwVehiculeForce, ForceMode.Impulse);

        RigidbodyFirstPersonController script = player.GetComponent<RigidbodyFirstPersonController>();
        if (script != null)
            script.jetpackMode = false;
    }
}
