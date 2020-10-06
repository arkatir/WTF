using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public GameObject target;
    private float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.transform.position - transform.position;
        if(direction.magnitude < 1.0f)
        {
            Rigidbody targetHealth = target.GetComponent<Rigidbody>();
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(direction.normalized * Time.deltaTime * speed);
        }
    }


}
