using UnityEngine;
public class PCIdle : PCState
{
    public PCIdle(PCStateMachine pcStateMachine) : base(pcStateMachine)
    {
    }
    public override void Update()
    {
        IdleSpeedStop();
        Transitions();
    }

    private void IdleSpeedStop()
    {
        if (_pcStateMachine.pcController.pcReferences.rb.velocity != Vector3.zero) _pcStateMachine.pcController.pcReferences.rb.velocity = new Vector3(0f, _pcStateMachine.pcController.pcReferences.rb.velocity.y, 0f);
    }

    #region Transitions
    private void Transitions()
    {
        Inputs inputs = _pcStateMachine.pcController.pcReferences.inputs;
        GoToGrabState(inputs);
        GoToMovementState(inputs);
        GoToAttackState(inputs);
        GoToDodgeState(inputs);
        GoToEnterLanternUpState(inputs);
    }
    #region ToGrabState
    private void GoToGrabState(Inputs inputs)
    {
        if (_pcStateMachine.pcController.pcReferences.pcGrabbing.GrabbedObjectExists())
        {
            if (inputs.MovementInput == Vector3.zero) _pcStateMachine.SetState(new PCIdleGrab(_pcStateMachine));
            else _pcStateMachine.SetState(new PCMovingGrab(_pcStateMachine));
        }
    }
    #endregion
    #region ToMovementState
    private void GoToMovementState(Inputs inputs)
    {
        if ((inputs.MovementInput != Vector3.zero)) _pcStateMachine.SetState(new PCMoving(_pcStateMachine));
    }
    #endregion
    #region ToAttackState
    private void GoToAttackState(Inputs inputs)
    {
        if (inputs.LeftClickInput || inputs.RightClickInput)
        {
            if (inputs.RightClickInput) _pcStateMachine.SetState(new PCAttack(_pcStateMachine, true));
            else _pcStateMachine.SetState(new PCAttack(_pcStateMachine, false));
        }
    }
    #endregion
    #region ToDodgeState
    private void GoToDodgeState(Inputs inputs)
    {
        if (inputs.DodgeInput)
        {
            _pcStateMachine.SetState(new PCDodge(_pcStateMachine, _pcStateMachine.pcController.lookDirection));
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