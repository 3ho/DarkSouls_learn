﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotionManager : MonoBehaviour
{
    EnemyManager enemyManager;
    EnemyAnimatorManager enemyAnimatorManager;
    NavMeshAgent navMeshAgent;
    public Rigidbody enemyRigidbody;

    public CharacterStats currectTarget;
    public LayerMask detetionLayer;

    public float distaceFromTarget;
    public float stoppingDistance = 1f;

    public float rotationSpeed = 15;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        navMeshAgent.enabled = false;
        enemyRigidbody.isKinematic = false;
    }

    public void HandleDetetion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detetionLayer);

        for(int i=0;i<colliders.Length;i++)
        {
            CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

            if(characterStats != null)
            {
                Vector3 targetDirection = characterStats.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if(viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maxmumDetectionAngle)
                {
                    currectTarget = characterStats;
                }
            }
        }
    }

    public void HandleMoveToTarget()
    {
        Vector3 targetDirection = currectTarget.transform.position - transform.position;
        distaceFromTarget = Vector3.Distance(currectTarget.transform.position, transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

        if(enemyManager.isPrefromingAction)
        {
            enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            navMeshAgent.enabled = false;
        }
        else
        {
            if(distaceFromTarget > stoppingDistance)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            }else if(distaceFromTarget <= stoppingDistance)
            {
                enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            }
        }
        HandleRotateTowardsTarget();
        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;
    }

    private void HandleRotateTowardsTarget()
    {
        if (enemyManager.isPrefromingAction)
            return;

        if(enemyManager.isPrefromingAction)
        {
            Vector3 direction = currectTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if(direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        }
        else
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyRigidbody.velocity;

            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(currectTarget.transform.position);
            enemyRigidbody.velocity = targetVelocity;
            transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
        }

        
    }
}
