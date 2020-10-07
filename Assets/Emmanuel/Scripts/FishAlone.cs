using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAlone : MonoBehaviour
{
    float speed;
    float startSpeed;
    float rotationSpeed = 1;
    private bool notSeen = true;

    public float playerDistance;

    public GameObject[] flock;
    public GameObject[] noticedNeighbour;
    public GameObject player;

    Vector3 averagePosition;
    public int noticedNeighbourNumber = 4;

    Material m_Material;

    //countdown
    private bool isCountingDown = false;
    float timeAfterCountdown;

    bool isDeadFish = true;
    void Start()
    {
        player = GameObject.Find("Player");
        m_Material = GetComponent<Renderer>().material;
        m_Material.SetFloat("_RandVal", Random.Range(0f, 6.28f));
        m_Material.SetFloat("_Danger", 1f);

    }

    private void OnEnable()
    {
        timeAfterCountdown = 0;
        isCountingDown = false;
        startSpeed = Random.Range(0.8f, 1.5f);
        speed = startSpeed;
        noticedNeighbour = new GameObject[noticedNeighbourNumber];
        isDeadFish = false;
    }

    private void OnDisable()
    {
        isDeadFish = true;
    }
    void Update()
    {
        if (!isDeadFish)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) > playerDistance)
            {
                if (notSeen)
                {
                    m_Material.SetFloat("_Danger", 1f);
                    notSeen = false;
                }
                if (Random.Range(0, 4) < 1)
                {
                    ApplyRules();
                    Debug.Log("test");
                }
                transform.position += transform.forward * speed * Time.deltaTime;
            }

            else
            {
                if (!notSeen)
                {
                    notSeen = true;
                    m_Material.SetFloat("_Danger", 0f);
                }
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - this.transform.position), rotationSpeed * Time.deltaTime * 2);
                transform.position += transform.forward * speed * Time.deltaTime * 4;

                if (Vector3.Distance(this.transform.position, player.transform.position) < 0.4f)
                {
                    isCountingDown = true;
                }
            }

            if (isCountingDown)
            {
                timeAfterCountdown += Time.deltaTime;
            }

            if (timeAfterCountdown > 1.5)
            {
                this.gameObject.SetActive(false);
                //deactivate fish
            }
        }

    }


    //regle de flocking
    void ApplyRules()
    {
        flock = FishSchool.fishSchool;

        averagePosition = Vector3.zero;

        Vector3 goal = FishSchool.goal;
        int c = 0;

        //Choix des neigbours aléatoires
        GameObject neigbour;
        while (c < noticedNeighbourNumber)
        {
            neigbour = flock[Random.Range(0, flock.Length)]; // ne vérifie pas si les neighbours choisis sont différents
            if (neigbour != this.gameObject)
            {
                noticedNeighbour[c] = neigbour;
                c += 1;
            }
        }

        foreach (GameObject fish in noticedNeighbour)
        {
            averagePosition += fish.transform.position;
            FishAlone otherfish = fish.GetComponent<FishAlone>();
        }

        averagePosition = averagePosition / noticedNeighbourNumber + (goal - this.transform.position);
        Vector3 direction = averagePosition - this.transform.position;
        float diffDir = Quaternion.Dot(transform.rotation, Quaternion.LookRotation(direction));
        this.speed = 0.3f + Mathf.Abs(diffDir * startSpeed);
        this.rotationSpeed = this.speed;
        if (direction != Vector3.zero)
        {
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        }
    }

}
