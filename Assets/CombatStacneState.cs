using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStacneState : State
{
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        //检查攻击范围
        //围绕玩家走
        //如果在攻击范围内，返回攻击状态
        //如果我们在攻击处于停止状态，则返回此状态并继续跟随玩家
        //如果玩家超出范围追击目标状态
        return this;
    }
}
