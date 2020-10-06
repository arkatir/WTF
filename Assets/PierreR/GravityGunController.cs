using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunController : MonoBehaviour
{
    public Vector3 offset;

    private Transform cameraTr;
    private bool selected = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraTr = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            selected = !selected;
        }

        if (selected && Input.GetMouseButtonDown(0))
        {

        }
    }
}
