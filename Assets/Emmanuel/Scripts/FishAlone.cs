using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAlone : MonoBehaviour
{
    float speed;
    float startSpeed;
    float rotationSpeed = 1;
    private bool notSeen = true;

    Vector3 SpawnPoint;
    float initRange;

    float playerDistance;

    FishSchool flock;
    GameObject[] noticedNeighbour;
    public GameObject player;

    Vector3 averagePosition;
    int noticedNeighbourNumber = 4;

    Material m_Material;

    //countdown
    private bool isCountingDownDeath = false;
    float timeAfterDeathCountdown;

    private bool isCountingDownSpawn = false;
    float timeAfterSpawnCountdown;

    bool isDeadFish = true;

    void Start()
    {
        flock = FishSchool.sharedInstance;
        player = GameObject.Find("Player");
        SpawnPoint = flock.transform.position;
        initRange = flock.initRange;
        playerDistance = flock.PlayerDetectionDistance;
        isDeadFish = false;
    }

    private void OnEnable()
    {
        notSeen = true;
        timeAfterDeathCountdown = 0;
        isCountingDownDeath = false;
        timeAfterSpawnCountdown = 0;
        isCountingDownSpawn = false;
        startSpeed = Random.Range(0.8f, 1.5f);
        speed = startSpeed;
        noticedNeighbour = new GameObject[noticedNeighbourNumber];
        m_Material = GetComponent<Renderer>().material;
        m_Material.SetFloat("_RandVal", Random.Range(0f, 6.28f));
        m_Material.SetFloat("_Danger", 1f);
    }

    void Update()
    {
        if (!isDeadFish)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) > playerDistance) //Player is far
            {
                if (notSeen) //set the blue fish color 
                {
                    m_Material.SetFloat("_Danger", 1f);
                    notSeen = false;
                }
                if (Random.Range(0, 4) < 1) //Apply the flocking rules with a bit of randomness
                {
                    ApplyRules();
                }
                transform.position += transform.forward * speed * Time.deltaTime;
            }

            else //Player is nearby
            {

                HuntPlayer();
            }

            if (isCountingDownDeath) //Countdown before deactivation
            {
                timeAfterDeathCountdown += Time.deltaTime;
            }

            if (timeAfterDeathCountdown > 1) //deactivation
            {
                PlayerStats ps = player.GetComponent<PlayerStats>();
                ps.RemoveHealth(1);
                SpawnFish();
                isDeadFish = true;
                isCountingDownSpawn = true;
                //this.gameObject.SetActive(false);
            }
        }

        else
        {
            if (isCountingDownSpawn) //Countdown before reappearance
            {
                timeAfterSpawnCountdown += Time.deltaTime;
            }

            if (timeAfterSpawnCountdown > 17 + 1 * Random.Range(0.0f, 20.0f))
            {
                gameObject.GetComponent<Renderer>().enabled = true;
                isDeadFish = false;
            }
        }


        //flocking rules
        void ApplyRules()
        {
            //flock = FishSchool.sharedInstance;
                //FishSchool.fishSchool;

            averagePosition = Vector3.zero;

            Vector3 goal = FishSchool.goal;
            int c = 0;

            //random neighbour choice
            GameObject neigbour;
            while (c < noticedNeighbourNumber)
            {
                neigbour = flock.fishSchool[Random.Range(0, flock.fishNumber)]; // ne vérifie pas si les neighbours choisis sont différents mais osef ça marche
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


        void HuntPlayer()
        {
            if (!notSeen) //set the red fish color 
            {
                notSeen = true;
                m_Material.SetFloat("_Danger", 0f);
            }
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - this.transform.position), rotationSpeed * Time.deltaTime * 2);
            transform.position += transform.forward * speed * Time.deltaTime * 5;

            if (Vector3.Distance(this.transform.position, player.transform.position) < 0.4f)
            {
                isCountingDownDeath = true;

            }
        }

        void SpawnFish()
        {
            Vector3 startPosition = SpawnPoint + Random.insideUnitSphere * Random.Range(-initRange, initRange);
            this.transform.position = startPosition;
            OnEnable();
            gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

}
