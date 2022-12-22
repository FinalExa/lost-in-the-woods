using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCAttack : PCState
{
    private Combo combo;
    public PCAttack(PCStateMachine pcStateMachine) : base(pcStateMachine)
    {
    }

    public override void Start()
    {
        combo = _pcStateMachine.pcController.pcReferences.pcCombo;
        if (combo.StartComboHitCheck()) _pcStateMachine.pcController.pcReferences.rb.velocity = Vector3.zero;
    }

    public override void Update()
    {
        if (combo.GetHitOver()) Transitions();
    }

    #region Transitions
    private void Transitions()
    {
        Inputs inputs = _pcStateMachine.pcController.pcReferences.inputs;
        GoToIdleState(inputs);
        GoToMovementState(inputs);
        GoToDodgeState(inputs);
        GoToEnterLanternUpState(inputs);
    }
    #region ToIdleState
    private void GoToIdleState(Inputs inputs)
    {
        if (inputs.MovementInput == Vector3.zero) _pcStateMachine.SetState(new PCIdle(_pcStateMachine));
    }
    #endregion
    #region ToMovementState
    private void GoToMovementState(Inputs inputs)
    {
        if ((inputs.MovementInput != UnityEngine.Vector3.zero)) _pcStateMachine.SetState(new PCMoving(_pcStateMachine));
    }
    #endregion
    #region ToDodgeState
    private void GoToDodgeState(Inputs inputs)
    {
        if (inputs.DodgeInput)
        {
            _pcStateMachine.SetState(new PCDodge(_pcStateMachine, _pcStateMachine.pcController.pcReferences.pcData.defaultDirection));
        }
    }
    #endregion
    #region ToEnterLanternUpState
    private void GoToEnterLanternUpState(Inputs inputs)
    {
        if (inputs.LanternInput) _pcStateMachine.SetState(new PCEnterLanternUp(_pcStateMachine));
    }
    #endregion
    #endregion
}
