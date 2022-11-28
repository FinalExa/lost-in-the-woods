using System.Collections;
using System.Collections.Generic;
using BehaviorTree;

public class TaskBrambleIsCoveredByLight : Node
{
    private BrambleController enemyController;

    public TaskBrambleIsCoveredByLight(EnemyController _enemyController)
    {
        enemyController = (BrambleController)_enemyController;
    }

    public override NodeState Evaluate()
    {
        enemyController.RetractionUpdate(-enemyController.brambleData.unretractedScaleSize);
        return NodeState.RUNNING;
    }
}
