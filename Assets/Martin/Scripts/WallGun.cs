﻿using UnityEngine;

public class WallGun : SlotItem
{
    public GameObject wallsPrefab;
    public float groundRotationSpeed = 60f;
    public float minIntervalBetweenShots = 0.2f;
    public float distance = 5f;

    private bool _held;
    private float _lastShot;
    private string projectileName;

    private void Start()
    {
        projectileName = wallsPrefab.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (_held)
        {
            if (Input.GetMouseButton(0))
            {
                if (Time.time >= _lastShot + minIntervalBetweenShots)
                {
                    if (Physics.Raycast(transform.parent.position, transform.parent.forward, out RaycastHit hitInfo))
                    {
                        ObjectPoolManager.managerInstance.CreateObject(projectileName, hitInfo.point + Vector3.up * 2, Quaternion.identity);
                        _lastShot = Time.time;
                    }
                }
            }
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y + Time.deltaTime * groundRotationSpeed, 0);
        }
    }

    public override void OnInsert()
    {
        _held = true;
        transform.SetParent(Player.Get().GetComponentInChildren<Camera>().transform);
        transform.localPosition = -0.5f * Vector3.up;
        transform.localRotation = Quaternion.AngleAxis(-10f, Vector3.right);
    }

    public override void OnRemove()
    {
        _held = false;
        Vector3 newPos = transform.parent.position + 10 * transform.parent.forward;
        transform.SetParent(null);
        transform.position = new Vector3(newPos.x, 1, newPos.z);
        transform.localRotation = Quaternion.identity;
    }
}
