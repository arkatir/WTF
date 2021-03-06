﻿using System.Collections;
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
    private string attackProjectileName;
    //For speed intake
    private Vector3 lastSavedPosition;
    #endregion
    #region Public Variables
    public NavMeshAgent nav;
    public float maxRoamTime;
    public float maxRadiusPoint;
    public float attackDistance;
    public float attackCooldown;
    public GameObject attackProjectilePrefab;
    public bool isPursuing = false;
    public bool isDodging = false;
    public bool isDying = false;
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
        isDodging = false;
        isDying = false;
        lastSavedPosition = transform.position;
        currentTime = 0f;
        maxNavSpeed = nav.speed;
        attackProjectileName = attackProjectilePrefab.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDying)
        {
            CheckMovement();
            TransmitInfoToAnimator();
        }
        if(enemyDetector.isDetected == true && !isPursuing && !isDodging) //Can only pursue if not dodging and if player is visible
        {
            isPursuing = true;
        }
        else if (enemyDetector.isDetected == false && isPursuing)
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
            float distanceToPlayer = (this.transform.position - enemyDetector.lastSavedPosition).magnitude;
            if ((distanceToPlayer < attackDistance) && (currentAttackTime >= attackCooldown))
            {
                Attack();
                currentAttackTime = 0f; //To not attack all the time
            }
            else if (distanceToPlayer > attackDistance)
            {
                nav.SetDestination(enemyDetector.lastSavedPosition);
            }
        }
    }

    public void Attack()
    {
        enemyAnimator.SetTrigger("Attack");
        ObjectPoolManager.managerInstance.CreateObject(attackProjectileName, transform.position + transform.forward * 1f, transform.rotation);
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
