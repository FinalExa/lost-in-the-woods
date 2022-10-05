using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCIdleLanternUp : PCState
{
    public PCIdleLanternUp(PCStateMachine pcStateMachine) : base(pcStateMachine)
    {
    }
    public override void Update()
    {
        Transitions();
    }

    #region Transitions
    private void Transitions()
    {
        Inputs inputs = _pcStateMachine.pcController.pcReferences.inputs;
        GoToMovementLanternUpState(inputs);
        GoToExitLanternUpState(inputs);
    }
    #region ToMovementLanternUpState
    private void GoToMovementLanternUpState(Inputs inputs)
    {
        if (inputs.MovementInput != Vector3.zero) _pcStateMachine.SetState(new PCMovingLanternUp(_pcStateMachine));
    }
    #endregion
    #region ToExitLanternUpState
    private void GoToExitLanternUpState(Inputs inputs)
    {
        if (inputs.LanternInput) _pcStateMachine.SetState(new PCExitLanternUp(_pcStateMachine));
    }
    #endregion
    #endregion
}
