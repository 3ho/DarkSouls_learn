using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueTargetState : State
{
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        //追逐目标状态
        // 如果在攻击范围内，则返回战斗姿态
        //如果目标超出范围，则返回此状态并继续追逐
        return this;
    }
}