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
        if (_pcStateMachine.pcController.pcReferences.rb.velocity != Vector3.zero) _pcStateMachine.pcController.pcReferences.rb.velocity = Vector3.zero;
    }

    #region Transitions
    private void Transitions()
    {
        Inputs inputs = _pcStateMachine.pcController.pcReferences.inputs;
        GoToMovementState(inputs);
        GoToAttackState(inputs);
        GoToDodgeState(inputs);
        GoToEnterLanternUpState(inputs);
    }
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