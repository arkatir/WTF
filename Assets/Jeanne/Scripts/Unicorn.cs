using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Unicorn : SlotItem
{
    private float timerDisable;
    public GameObject player;
    public Rigidbody rb;
    public Transform handle;
    private float runMultBase;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        timerDisable -= Time.deltaTime;
    }

    public override void OnInsert()
    {
        if (timerDisable > 0) return;

        GetComponent<MeshCollider>().enabled = false;
        rb.isKinematic = true;
        transform.parent.rotation = player.transform.rotation;
        player.transform.position = handle.position;
        transform.parent.parent = player.transform;
        Camera cam = player.gameObject.transform.GetComponentInChildren<Camera>();
        player.gameObject.transform.GetComponentInChildren<HeadBob>().enabled = false;
        cam.transform.position += Vector3.up * 0.5f;
        runMultBase = player.GetComponent<RigidbodyFirstPersonController>().movementSettings.RunMultiplier;
        player.GetComponent<RigidbodyFirstPersonController>().movementSettings.RunMultiplier = 5;


    }
   
    public override void OnRemove()
    {
        timerDisable = 2;
        //on jette la licorn
        rb.transform.position = rb.transform.position + rb.transform.forward * 1;
        rb.isKinematic = false;
        player.SetActive(true);
        player.gameObject.transform.GetComponentInChildren<HeadBob>().enabled = true;
        Camera.main.transform.parent = player.transform;
        Camera.main.transform.position -= Vector3.up * 0.5f;
        GetComponent<MeshCollider>().enabled = true;
        transform.parent.parent = null;
        player.GetComponent<RigidbodyFirstPersonController>().movementSettings.RunMultiplier = runMultBase;

    }

}


