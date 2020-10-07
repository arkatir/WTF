using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Unicorn : SlotItem
{

    public GameObject player;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();

    }

    //surcharger cette méthode de la classe SlotItem
  /*  public void OnTriggerEnter(Collider other)
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
    */

    public override void OnInsert()
    {
        rb.isKinematic = true;
        transform.parent.rotation = player.gameObject.transform.rotation;
        transform.parent.position = player.gameObject.transform.position - (Vector3.up * 0.5f);
        transform.parent.parent = player.gameObject.transform;
        Camera cam = player.gameObject.transform.GetComponentInChildren<Camera>();
        player.gameObject.transform.GetComponentInChildren<HeadBob>().enabled = false;
        cam.transform.position += Vector3.up * 0.5f;

       
    }
   
    public override void OnRemove()
    {
        //on jette la licorne
        rb.transform.position = rb.transform.position + rb.transform.forward * 1;
        rb.isKinematic = false;
        player.transform.parent = null;
        player.SetActive(true);
        player.gameObject.transform.GetComponentInChildren<HeadBob>().enabled = true;
        Camera.main.transform.parent = player.transform;
        Camera.main.transform.position -= Vector3.up * 0.5f;


    }

}


