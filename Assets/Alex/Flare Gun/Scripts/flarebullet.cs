using UnityEngine;
using System.Collections;

public class flarebullet : MonoBehaviour
{


    private Light flarelight;
    private AudioSource flaresound;
    private ParticleSystemRenderer smokepParSystem;
    private bool myCoroutine;
    private float smooth = 2.4f;
    public float flareTimer = 9;
    public AudioClip flareBurningSound;
    public Transform Player;
    public float space;
    private bool teleport = true;
    public Object marker;

    public Transform debugtr;


    // Use this for initialization
    void Start()
    {

        //StartCoroutine("flareLightoff");

        GetComponent<AudioSource>().PlayOneShot(flareBurningSound);
        flarelight = GetComponent<Light>();
        flaresound = GetComponent<AudioSource>();
        smokepParSystem = GetComponent<ParticleSystemRenderer>();


        Destroy(gameObject, flareTimer + 1f);
        debugtr = GameObject.Find("Marker").transform;

    }

    // Update is called once per frame
    void Update()
    {


        if (myCoroutine == true)

        {
            flarelight.intensity = Random.Range(2f, 6.0f);

        }
        else

        {
            flarelight.intensity = Mathf.Lerp(flarelight.intensity, 0f, Time.deltaTime * smooth);
            flarelight.range = Mathf.Lerp(flarelight.range, 0f, Time.deltaTime * smooth);
            flaresound.volume = Mathf.Lerp(flaresound.volume, 0f, Time.deltaTime * smooth);
            smokepParSystem.maxParticleSize = Mathf.Lerp(smokepParSystem.maxParticleSize, 0f, Time.deltaTime * 5);


        }



        /*IEnumerator flareLightoff()
        {
            myCoroutine = true;
            yield return new WaitForSeconds(flareTimer);
            myCoroutine = false;

        }*/
    }
    public void OnCollisionEnter(Collision collision)
    {








        /*float maxY = float.NegativeInfinity;
        
        for(int i=0;i< collision.contactCount; i++)
        {
            float val = Vector3.Dot(collision.contacts[i].normal, collision.contacts[i].point - );
            if (maxY < val)
            {
                iBestpoint = i;
                maxY = val;

                Debug.Log("test " + collision.contacts[i].normal + " " + val);
            }
                
        }

        Debug.Log("best  " + iBestpoint);*/


        int iBestpoint = -1;
        bool enoughspace = true;
        for (int i = 0; i < collision.contactCount; i++)
        {
            enoughspace = true;
            RaycastHit[] hits = Physics.SphereCastAll(collision.contacts[i].point + (collision.contacts[i].normal * 1.0f), 0.5f, Vector3.up, 0.01f);

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform != this.transform)
                {
                    enoughspace = false;
                    Debug.Log("NAAAAAAAAAN " + collision.contactCount + " " + hit.transform.gameObject.name);
                    debugtr.position = collision.contacts[i].point;
                }

            }
            if (enoughspace)
            {
                iBestpoint = i;
            }
        }


        if (teleport && enoughspace)
        {

           
            Player.position = collision.contacts[iBestpoint].point + (collision.contacts[iBestpoint].normal * .6f);

        }

        Destroy(this.gameObject);
       
       teleport = false;
    }

}
