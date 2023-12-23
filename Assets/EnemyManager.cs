using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    EnemyLocomotionManager enemyLocomotionManager;
    public bool isPrefromingAction;

    [Header("A.I Setting")]
    public float detectionRadius = 20;
    public float minimumDetectionAngle = -50;
    public float maxmumDetectionAngle = 50;

    private void Awake()
    {
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        HandleCurrectAction();
    }

    private void HandleCurrectAction()
    {
        if(enemyLocomotionManager.currectTarget == null)
        {
            enemyLocomotionManager.HandleDetetion();
        }
        else
        {
            enemyLocomotionManager.HandleMoveToTarget();
        }
    }
}
