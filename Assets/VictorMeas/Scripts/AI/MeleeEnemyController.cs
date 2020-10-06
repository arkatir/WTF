using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine;

public class MeleeEnemyController : MonoBehaviour
{
    #region Private Variables
    
    private float currentTime;
    private float chosenMaxTime;
    private float maxNavSpeed;

    //For speed intake
    private Vector3 lastSavedPosition;
    #endregion
    #region Public Variables
    public NavMeshAgent nav;
    public float maxRoamTime;
    public float maxRadiusPoint;
    public bool isPursuing;
    public bool isDying;
    [Header("Animation")]
    public Animator enemyAnimator;
    #endregion

    // Start is called before the first frame update
    void OnEnable()
    {
        InitializeState();
    }

    public void InitializeState()
    {
        nav = this.GetComponent<NavMeshAgent>();
        isPursuing = false;
        isDying = false;
        lastSavedPosition = transform.position;
        currentTime = 0f;
        maxNavSpeed = nav.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDying)
        {
            CheckMovement();
            TransmitInfoToAnimator();
        }
    }

    public void CheckMovement()
    {
        if (!isPursuing)
        {
            if(currentTime >= chosenMaxTime)
            {
                nav.SetDestination(GetRandomPoint(transform.position, maxRadiusPoint));
                currentTime = 0f;
                chosenMaxTime = Random.Range(4f, maxRoamTime);
                Debug.Log("new max chosen time is: " + chosenMaxTime.ToString()); 
                return;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }



    public void TransmitInfoToAnimator()
    {
        float currentSpeed = (transform.position - lastSavedPosition).magnitude ;
        lastSavedPosition = transform.position;
        enemyAnimator.SetFloat("Speed", Vector3.Distance(nav.desiredVelocity, new Vector3(0, 0, 0)));
    }

    public static Vector3 GetRandomPoint(Vector3 center, float maxDistance)
    {
        // Get Random Point inside Sphere which position is center, radius is maxDistance
        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;
        NavMeshHit hit; // NavMesh Sampling Info Container
        // from randomPos find a nearest point on NavMesh surface in range of maxDistance
        NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas);
        return hit.position;
    }
}
