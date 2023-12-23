using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    EnemyLocomotionManager enemyLocomotionManager;
    EnemyAnimatorManager enemyAnimatorManager;

    public bool isPrefromingAction;

    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;

    [Header("A.I Setting")]
    public float detectionRadius = 20;
    public float minimumDetectionAngle = -50;
    public float maxmumDetectionAngle = 50;

    public float currentRecoveryTime = 0;

    private void Awake()
    {
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
    }

    private void Update()
    {
        HandleRecoveryTime();
    }

    private void FixedUpdate()
    {
        HandleCurrectAction();
    }

    private void HandleCurrectAction()
    {
        if(enemyLocomotionManager.currectTarget != null)
        {
            enemyLocomotionManager.distaceFromTarget = Vector3.Distance(enemyLocomotionManager.currectTarget.transform.position, transform.position);
        }

        if(enemyLocomotionManager.currectTarget == null)
        {
            enemyLocomotionManager.HandleDetetion();
        }
        else if(enemyLocomotionManager.distaceFromTarget > enemyLocomotionManager.stoppingDistance)
        {
            enemyLocomotionManager.HandleMoveToTarget();
        }else if(enemyLocomotionManager.distaceFromTarget <= enemyLocomotionManager.stoppingDistance)
        {
            //
            AttackTarget();
        }
    }

    private void HandleRecoveryTime()
    {
        if(currentRecoveryTime >=0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if(isPrefromingAction)
        {
            if(currentRecoveryTime <=0)
            {
                isPrefromingAction = false;
            }
        }
    }

    private void AttackTarget()
    {
        if (isPrefromingAction)
            return;

        if(currentAttack == null)
        {
            GetNewAttack();
        }
        else
        {
            isPrefromingAction = true;
            currentRecoveryTime = currentAttack.recoveryTime;
            enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
            currentAttack = null;
        }
    }

    private void GetNewAttack()
    {
        Vector3 targetsDirection = enemyLocomotionManager.currectTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetsDirection, transform.forward);
        enemyLocomotionManager.distaceFromTarget = Vector3.Distance(enemyLocomotionManager.currectTarget.transform.position, transform.position);

        int maxScore = 0;

        for(int i=0;i<enemyAttacks.Length;i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if(enemyLocomotionManager.distaceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && enemyLocomotionManager.distaceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if(viewableAngle <= enemyAttackAction.maxmumAttackAngle
                    && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    maxScore += enemyAttackAction.attackScore;
                }
            }
        }

        int randomValue = Random.Range(0, maxScore);
        int temporaryScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (enemyLocomotionManager.distaceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && enemyLocomotionManager.distaceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maxmumAttackAngle
                    && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    if (currentAttack != null)
                        return;

                    temporaryScore += enemyAttackAction.attackScore;

                    if(temporaryScore > randomValue)
                    {
                        currentAttack = enemyAttackAction;
                    }
                }
            }
        }
    }
}
