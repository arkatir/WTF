﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public GameObject target;
    private float explosionRadius = 5.0f;
    private float explosionForce = 20000.0f;
    private float propulsion = 3000.0f;
    private bool moving = true;
    private bool exploding = false;
    private Rigidbody rb;
    public float speedCap;
    public float timer;
    public Color color1 = new Color(1, 0, 0);
    public Color color2 = new Color(0, 0, 0);
    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mat = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.transform.position - transform.position;
        if(direction.magnitude < 1.5f)
        {
            exploding = true;
            moving = false;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }
        else if (moving)
        {
            Vector3 directionCorrected = direction - 10 * rb.velocity ;
            rb.AddForce(propulsion * directionCorrected.normalized * Time.deltaTime, ForceMode.Force);
            if (rb.velocity.magnitude > speedCap)
            {
                Vector3.Normalize(rb.velocity * speedCap);
            }
        }
        if (exploding)
        {
            WaitExplode();
        }
    }

    private void WaitExplode()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Explode();
        }
        Debug.Log(timer % 1);
        if (timer % 1 < 0.5f)
        {
            mat.SetColor("_Color", color1);
        }
        else
        {
            mat.SetColor("_Color", color2);
        }
    }

    IEnumerator WaitResume()
    {
        moving = false;
        yield return new WaitForSeconds(2.5f);
        moving = true;
    }


    private void Explode()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, explosionRadius);
        for (int i = 0; i < col.Length; i++)
        {
            GameObject obj = col[i].gameObject;
            if (obj.GetComponent<PlayerStats>() != null)
            {
                obj.GetComponent<PlayerStats>().RemoveHealth(1);
            }
            if (obj.GetComponent<Rigidbody>() != null)
            {
                obj.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
            if (obj.GetComponent<AutoDestroy>() != null)
            {
                obj.GetComponent<AutoDestroy>().StartCoroutine(WaitResume());
            }
        }
        Destroy(gameObject);
    }

    

}
