using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class TaskIsCloseToPlayer : Node
{
    private GameObject _thisGameObject;
    private NavMeshAgent _thisAgent;
    private GameObject _target;
    private float _distanceFromPlayer;

    public TaskIsCloseToPlayer(GameObject thisGameObject, NavMeshAgent thisAgent, GameObject target, float distanceFromPlayer)
    {
        _thisGameObject = thisGameObject;
        _thisAgent = thisAgent;
        _target = target;
        _distanceFromPlayer = distanceFromPlayer;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(_thisGameObject.transform.position, _target.transform.position);
        if (distance <= _distanceFromPlayer)
        {
            _thisAgent.isStopped = true;
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }
}
