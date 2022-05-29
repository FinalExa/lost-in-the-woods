using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskBasherAttack : Node
{
    private float _attackChargeMaxTime;
    private float _attackChargeTimer;

    public TaskBasherAttack(float attackChargeMaxTime)
    {
        _attackChargeMaxTime = attackChargeMaxTime;
        _attackChargeTimer = _attackChargeMaxTime;
    }

    public override NodeState Evaluate()
    {
        if (_attackChargeTimer > 0)
        {
            _attackChargeTimer -= Time.deltaTime;
            Debug.Log("charge");
        }
        else
        {
            Debug.Log("attack");
            _attackChargeTimer = _attackChargeMaxTime;
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.RUNNING;
        return state;
    }
}
