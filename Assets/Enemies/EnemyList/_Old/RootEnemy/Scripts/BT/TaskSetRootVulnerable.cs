using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class TaskSetRootVulnerable : Node
{
    private RootEnemyController rootEnemyController;
    public TaskSetRootVulnerable(RootEnemyController _rootEnemyController)
    {
        rootEnemyController = _rootEnemyController;
    }

    public override NodeState Evaluate()
    {
        if (rootEnemyController.affectedByLight.lightState == AffectedByLight.LightState.CALM)
        {
            rootEnemyController.SetRootOutside();
            return NodeState.SUCCESS;
        }
        if (rootEnemyController.rootUnderground)
        {
            if (rootEnemyController.AttackDone && rootEnemyController.affectedByLight.lightState == AffectedByLight.LightState.NORMAL)
            {
                rootEnemyController.SetRootOutside();
                rootEnemyController.AttackDone = false;
                return NodeState.FAILURE;
            }
            return NodeState.SUCCESS;
        }
        else return NodeState.FAILURE;
    }
}
