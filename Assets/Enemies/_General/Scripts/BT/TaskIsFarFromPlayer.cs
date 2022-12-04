using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;
public class TaskIsFarFromPlayer : Node
{
    private NavMeshAgent nav;
    private GameObject target;
    private float distanceFromPlayer;
    private float distanceToleranceFromPlayer;

    public TaskIsFarFromPlayer(EnemyController _enemyController, float distance, float distanceTolerance)
    {
        nav = _enemyController.thisNavMeshAgent;
        target = _enemyController.playerTarget;
        distanceFromPlayer = distance;
        distanceToleranceFromPlayer = distanceTolerance;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(nav.gameObject.transform.position, target.transform.position);
        if (distance >= distanceFromPlayer - distanceToleranceFromPlayer && distance <= distanceFromPlayer + distanceToleranceFromPlayer)
        {
            nav.isStopped = true;
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
