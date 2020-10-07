using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicDamageController : MonoBehaviour
{
    public int damageFactor = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyStats es = collision.rigidbody.GetComponent<EnemyStats>();
        if (es != null)
            es.RemoveHealth((int)transform.GetComponent<Rigidbody>().velocity.magnitude * damageFactor);
    }
}
