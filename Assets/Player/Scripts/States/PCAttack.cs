using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCAttack : PCState
{
    private PCCombo combo;
    public PCAttack(PCStateMachine pcStateMachine) : base(pcStateMachine)
    {
    }

    public override void Start()
    {
        combo = _pcStateMachine.pcController.pcReferences.pcCombo;
        combo.StartComboHitCheck();
    }

    public override void Update()
    {
        if (combo.comboHitOver) Transitions();
    }

    #region Transitions
    private void Transitions()
    {
        Inputs inputs = _pcStateMachine.pcController.pcReferences.inputs;
        GoToIdleState(inputs);
        GoToMovementState(inputs);
        GoToDodgeState(inputs);
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
    #endregion
}
