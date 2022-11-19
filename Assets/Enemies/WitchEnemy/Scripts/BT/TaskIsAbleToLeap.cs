using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskIsAbleToLeapBack : Node
{
    WitchEnemyBT witchBT;
    public TaskIsAbleToLeapBack(WitchEnemyBT tree)
    {
        witchBT = tree;
    }

    public override NodeState Evaluate()
    {
        if (witchBT.enemyController.attackDone)
        {
            if (!witchBT.canLeap)
            {
                Vector3 hitDirection = (witchBT.backLeap.transform.position - witchBT.enemyController.gameObject.transform.position);
                if (Physics.Raycast(witchBT.gameObject.transform.position, hitDirection, out RaycastHit hit)) witchBT.leapDestination = hit.point;
                witchBT.canLeap = true;
            }
            return NodeState.FAILURE;
        }
        else return NodeState.SUCCESS;
    }
}
