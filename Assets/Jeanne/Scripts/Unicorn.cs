using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Unicorn : MonoBehaviour
{
    public bool isOnUnicorn;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        isOnUnicorn = false;
    }

    //surcharger cette méthode de la classe SlotItem
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Coucou");
        if( other.gameObject.tag == "Player")
        {
            transform.parent.rotation = other.gameObject.transform.rotation;
            transform.parent.position = other.gameObject.transform.position - (Vector3.up * 0.8f);
            transform.parent.parent = other.gameObject.transform;
            Camera cam = other.gameObject.transform.GetComponentInChildren<Camera>();
            other.gameObject.transform.GetComponentInChildren<HeadBob>().enabled = false;
            cam.transform.position += Vector3.up * 0.8f;
            
        }
    }
}
