using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public GameObject target;
    private float explosionRadius = 5.0f;
    private float explosionForce = 20000.0f;
    private float propulsion = 3000.0f;
    private bool moving = true;
    private Rigidbody rb;
    public float speedCap;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.transform.position - transform.position;
        if(direction.magnitude < 1.5f)
        {
            StartCoroutine(WaitExplode());
            moving = false;
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            //PlayerStats playerStats = target.GetComponent<PlayerStats>();
            //playerStats.RemoveHealth(1);
            //Destroy(gameObject);
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
    }

    IEnumerator WaitExplode()
    {
        yield return new WaitForSeconds(Random.Range(2.5f, 3.5f));
        Explode();
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

    //private Vector2 Symetric(Vector2 v, Vector2 axis)
    //{
    //    float x1 = v.x;
    //    float y1 = v.y;
    //    float x2 = axis.x;
    //    float y2 = axis.y;
    //    Vector2 u = new Vector2(x1 * x2 * x2 + 2 * y1 * x2 * y2 - x1 * y2 * y2, y1 * y2 * y2 + 2 * x1 * x2 * y2 - y1 * x2 * x2);
    //    return  u / Mathf.Pow(axis.magnitude, 2)
    //}

}
