using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskLeapBack : Node
{
    private WitchEnemyBT witchBT;
    public TaskLeapBack(WitchEnemyBT tree)
    {
        witchBT = tree;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(witchBT.transform.position, witchBT.leapDestination);
        if (distance > witchBT.witchEnemyData.leapTolerance)
        {
            if (witchBT.enemyController.thisNavMeshAgent.speed != witchBT.witchEnemyData.leapSpeed) witchBT.enemyController.thisNavMeshAgent.speed = witchBT.witchEnemyData.leapSpeed;
            if (witchBT.enemyController.thisNavMeshAgent.isStopped) witchBT.enemyController.thisNavMeshAgent.isStopped = false;
            witchBT.enemyController.thisNavMeshAgent.SetDestination(witchBT.leapDestination);
        }
        else
        {
            witchBT.enemyController.attackDone = false;
            witchBT.canLeap = false;
        }
        return NodeState.RUNNING;
    }
}
