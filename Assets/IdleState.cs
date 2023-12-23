using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        //寻找潜在目标
        //如果找到目录，切换到追逐目标状态
        return this;
    }
}
