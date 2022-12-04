using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class TaskIsFarFromPlayer : Node
{
    private UnityEngine.AI.NavMeshAgent nav;
    private GameObject target;
    private float distanceFromPlayer;

    public TaskIsFarFromPlayer(EnemyController _enemyController, float distance)
    {
        nav = _enemyController.thisNavMeshAgent;
        target = _enemyController.playerTarget;
        distanceFromPlayer = distance;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(nav.gameObject.transform.position, target.transform.position);
        if (distance > distanceFromPlayer)
        {
            nav.isStopped = true;
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
