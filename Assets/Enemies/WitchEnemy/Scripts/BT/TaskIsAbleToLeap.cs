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
        if (!witchBT.canLeap && witchBT.enemyController.attackDone)
        {
            Debug.Log("got here");
            witchBT.canLeap = true;
            RaycastHit hit;
            Vector3 hitDirection = -(witchBT.enemyController.playerTarget.transform.position - witchBT.enemyController.gameObject.transform.position).normalized;
            if (Physics.Raycast(witchBT.gameObject.transform.position, hitDirection, out hit, witchBT.witchEnemyData.leapDistance))
            {
                Debug.Log(hit.distance);
            }
            return NodeState.FAILURE;
        }
        else return NodeState.SUCCESS;
    }
}
