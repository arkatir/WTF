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
    private float currentAttackTime;
    //For speed intake
    private Vector3 lastSavedPosition;
    #endregion
    #region Public Variables
    public NavMeshAgent nav;
    public float maxRoamTime;
    public float maxRadiusPoint;
    public float attackDistance;
    public float attackCooldown;
    public bool isPursuing;
    public bool isDying;
    [Header("Animation")]
    public Animator enemyAnimator;
    [Header("Detection")]
    public EnemyDetection enemyDetector;
    #endregion

    // Start is called before the first frame update
    void OnEnable()
    {
        InitializeState();
    }

    public void InitializeState()
    {
        nav = this.GetComponent<NavMeshAgent>();
        nav.isStopped = false;
        this.GetComponent<Rigidbody>().isKinematic = true;
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
        if(enemyDetector.isDetected = true && !isPursuing)
        {
            isPursuing = true;
        }
        else
        {
            isPursuing = false;
        }
    }

    public void CheckMovement()
    {
        currentAttackTime += Time.deltaTime;
        if (!isPursuing) //Roaming
        {
            if(currentTime >= chosenMaxTime)
            {
                nav.SetDestination(GetRandomPoint(transform.position, maxRadiusPoint));
                currentTime = 0f;
                chosenMaxTime = Random.Range(4f, maxRoamTime);
                return;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
        else //Pursuing
        {
            if (((this.transform.position - enemyDetector.lastSavedPosition).magnitude < attackDistance) && (currentAttackTime >= attackCooldown))
            {
                Attack();
                currentAttackTime = 0f; //To not attack all the time
            }
            else
            {
                nav.SetDestination(enemyDetector.lastSavedPosition);
            }
        }
    }

    public void Attack()
    {
        enemyAnimator.SetTrigger("Attack");
        //Spawn un "projectile" immobile trigger qui peut faire des degats au joueur on trigger enter
    }

    public void TransmitInfoToAnimator()
    {
        float currentSpeed = nav.velocity.magnitude; //(transform.position - lastSavedPosition).magnitude ;
        enemyAnimator.SetFloat("Speed", currentSpeed/maxNavSpeed);
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
