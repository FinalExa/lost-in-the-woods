using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WitchWeak
{
    private WitchEnemyController witchEnemyController;
    public bool witchIsWeak;
    [SerializeField] private string[] weakNames;
    public void SetController(WitchEnemyController controller)
    {
        witchEnemyController = controller;
    }
    private bool CheckSetWeak()
    {
        if (witchEnemyController.affectedByLight.lightState == AffectedByLight.LightState.CALM) return SetWeak();
        else
        {
            foreach (string name in weakNames)
            {
                if (witchEnemyController.interaction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(name))
                {
                    return SetWeak();
                }
            }
        }
        return SetNotWeak();
    }
    public void WitchWeakOperations()
    {
        bool status = CheckSetWeak();
        if (status && !witchIsWeak)
        {
            witchIsWeak = true;
            witchEnemyController.witchCrying.WitchStopCryingAction();
        }
        else if (!status && witchIsWeak)
        {
            witchEnemyController.witchCrying.WitchStartCryingAction();
            witchIsWeak = false;
        }
    }

    private bool SetWeak()
    {
        witchEnemyController.thisNavMeshAgent.isStopped = true;
        return true;
    }

    private bool SetNotWeak()
    {
        witchEnemyController.thisNavMeshAgent.isStopped = false;
        return false;
    }

    public bool GetIfWitchIsWeak()
    {
        return witchIsWeak;
    }
}
