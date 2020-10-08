using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

//on hérite de la classe SlotItem la licorne est considérer comme un slot de véhicule
public class Unicorn : SlotItem
{
    private float timerDisable;
    public GameObject player;
    public Rigidbody rb;
    public Transform handle;
    private float runMultBase;


    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
        GetComponent<LaserUnicorn>().enabled = false;

    }

    private void Update()
    {
        timerDisable -= Time.deltaTime;
    }

    //on surcharge la méthode Virtual OnInsert en mettant le Override pour pouvoir récupérer cette méthode de notre classe fille
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
        cam.transform.position += Vector3.up * 1.2f;
        runMultBase = player.GetComponent<RigidbodyFirstPersonController>().movementSettings.RunMultiplier;
        player.GetComponent<RigidbodyFirstPersonController>().movementSettings.RunMultiplier = 5;
        //on récupère notre laser et on l'active que lorsque l'on est sur la licorne
        GetComponent<LaserUnicorn>().enabled = true;


    }
   
    public override void OnRemove()
    {
        //on met un timer pour éviter de retourner directement sur la licorne
        timerDisable = 2;
        //on jette la licorne
        rb.transform.position = rb.transform.position + rb.transform.forward * 1;
        rb.isKinematic = false;
        player.SetActive(true);
        player.gameObject.transform.GetComponentInChildren<HeadBob>().enabled = true;
        Camera.main.transform.parent = player.transform;
        Camera.main.transform.position -= Vector3.up * 1.2f;
        GetComponent<MeshCollider>().enabled = true;
        transform.parent.parent = null;
        player.GetComponent<RigidbodyFirstPersonController>().movementSettings.RunMultiplier = runMultBase;
        //on désactive les effets du laser si on est plus sur la licorne
        GetComponent<LaserUnicorn>().enabled = false;
    }

}


