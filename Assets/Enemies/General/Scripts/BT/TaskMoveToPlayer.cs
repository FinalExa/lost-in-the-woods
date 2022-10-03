using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class TaskMoveToPlayer : Node
{
    private NavMeshAgent nav;
    private GameObject target;

    public TaskMoveToPlayer(NavMeshAgent _nav, GameObject _target)
    {
        nav = _nav;
        target = _target;
    }

    public override NodeState Evaluate()
    {
        if (nav.isStopped) nav.isStopped = false;
        nav.SetDestination(target.transform.position);
        state = NodeState.RUNNING;
        return state;
    }
}