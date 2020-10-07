using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAlone : MonoBehaviour
{
    float speed;
    float startSpeed;
    float rotationSpeed = 1;

    public float playerDistance;

    public GameObject[] flock;
    GameObject[] noticedNeighbour;
    public GameObject player;

    //Vector3 averageHeading;
    Vector3 averagePosition;
    //float neighbourDistance = 0.5f;
    public int noticedNeighbourNumber = 4;

    private int c;

    // Start is called before the first frame update
    void Start()
    {

        Material m_Material = GetComponent<Renderer>().material;
        m_Material.SetFloat("_RandVal", Random.Range(0f,6.28f));
        startSpeed = Random.Range(0.8f, 1.5f);
        speed = startSpeed;
        noticedNeighbour = new GameObject[noticedNeighbourNumber];
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) > playerDistance)
        {
            if (Random.Range(0, 4) < 1)
            {
                ApplyRules();
            }
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            //HuntPlayer();
            //transform.position += transform.forward * speed * Time.deltaTime;
            Vector3.Slerp(this.transform.position, player.transform.position, speed * Time.deltaTime * 3);
        }
    }

    void ApplyRules()
    {
        //Empty noticedNeighbour
        flock = FishSchool.fishSchool;
        
        //averageHeading = Vector3.zero;
        averagePosition = Vector3.zero;
        

        float groupSpeed = 0f;
        Vector3 goal = FishSchool.goal;

        c = 0;
        
        //Choix des neigbours aléatoires
        GameObject neigbour;
        while(c<noticedNeighbourNumber)
        {
            neigbour = flock[Random.Range(0, flock.Length)]; // ne vérifie pas si les neighbours choisis sont différents
            if(neigbour!=this.gameObject)
            {
                noticedNeighbour[c] = neigbour;
                c += 1;
            }
        }

        foreach (GameObject fish in noticedNeighbour)
        {
            averagePosition += fish.transform.position;
            FishAlone otherfish = fish.GetComponent<FishAlone>();
            //groupSpeed += otherfish.speed;
        }

        averagePosition = averagePosition / noticedNeighbourNumber + (goal - this.transform.position);
        //groupSpeed = groupSpeed / noticedNeighbourNumber;

        Vector3 direction = averagePosition - this.transform.position;
        float diffDir = Quaternion.Dot(transform.rotation, Quaternion.LookRotation(direction));
        this.speed = 0.3f + Mathf.Abs(diffDir * startSpeed);
        this.rotationSpeed = this.speed;
        if (direction!= Vector3.zero)
        {
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        }
    }

    void HuntPlayer()
    {
        Vector3 towardPlayer = this.transform.position - player.transform.position;
        if (towardPlayer != Vector3.zero)
        {
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(towardPlayer), rotationSpeed * Time.deltaTime * 2);
        }
    }

}
