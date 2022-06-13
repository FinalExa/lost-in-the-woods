using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class TaskGetCloseToPlayer : Node
{
    private NavMeshAgent _thisAgent;
    private GameObject _target;

    public TaskGetCloseToPlayer(NavMeshAgent thisAgent, GameObject target)
    {
        _thisAgent = thisAgent;
        _target = target;
    }

    public override NodeState Evaluate()
    {
        if (_thisAgent.isStopped) _thisAgent.isStopped = false;
        _thisAgent.SetDestination(_target.transform.position);
        state = NodeState.RUNNING;
        return state;
    }
}