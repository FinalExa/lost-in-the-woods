using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class TaskIsCloseToPlayer : Node
{
    private NavMeshAgent nav;
    private GameObject target;
    private float distanceFromPlayer;

    public TaskIsCloseToPlayer(EnemyController _enemyController, float distance)
    {
        nav = _enemyController.thisNavMeshAgent;
        target = _enemyController.playerTarget;
        distanceFromPlayer = distance;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(nav.gameObject.transform.position, target.transform.position);
        if (distance <= distanceFromPlayer)
        {
            nav.isStopped = true;
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
