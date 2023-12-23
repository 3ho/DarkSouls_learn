using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocomotionManager : MonoBehaviour
{
    EnemyManager enemyManager;

    public CharacterStats currectTarget;
    public LayerMask detetionLayer;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
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
}
