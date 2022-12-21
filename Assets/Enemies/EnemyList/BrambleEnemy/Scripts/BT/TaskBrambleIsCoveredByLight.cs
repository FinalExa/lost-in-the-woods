using System.Collections;
using System.Collections.Generic;
using BehaviorTree;

public class TaskBrambleIsCoveredByLight : Node
{
    private BrambleController brambleController;

    public TaskBrambleIsCoveredByLight(EnemyController _enemyController)
    {
        brambleController = (BrambleController)_enemyController;
    }

    public override NodeState Evaluate()
    {
        if (brambleController.affectedByLight.lightState == AffectedByLight.LightState.CALM)
        {
            brambleController.OnLightReceived(-brambleController.brambleData.unretractedScaleSize);
            return NodeState.FAILURE;
        }
        return NodeState.SUCCESS;
    }
}
