using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskLeap : Node
{
    private WitchEnemyController _witchEnemyController;
    public TaskLeap(WitchEnemyController witchEnemyController)
    {
        _witchEnemyController = witchEnemyController;
    }

    public override NodeState Evaluate()
    {
        NodeState result = NodeState.SUCCESS;
        if (_witchEnemyController.witchLeap.GetIfWitchCanLeap() && _witchEnemyController.AttackDone) result = LeapAction();
        return result;
    }

    private NodeState LeapAction()
    {
        bool result = _witchEnemyController.witchLeap.CheckIfLeapIsFinishedByDistance();
        if (result) return NodeState.SUCCESS;
        return NodeState.RUNNING;
    }
}
