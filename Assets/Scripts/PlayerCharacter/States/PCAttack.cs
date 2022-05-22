using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCAttack : PCState
{
    private bool isOver = true;
    public PCAttack(PCStateMachine pcStateMachine) : base(pcStateMachine)
    {
    }

    public override void Start()
    {
        _pcStateMachine.pcController.pcReferences.pcRotation.rotationEnabled = false;
        isOver = true;
    }

    public override void StateUpdate()
    {
        if (isOver) Transitions();
    }

    #region Transitions
    private void Transitions()
    {
        Inputs inputs = _pcStateMachine.pcController.pcReferences.inputs;
        GoToIdleState(inputs);
        GoToMovementState(inputs);
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
    #endregion
}
