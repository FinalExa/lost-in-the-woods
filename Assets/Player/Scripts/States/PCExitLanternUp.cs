using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCExitLanternUp : PCState
{
    private float timer;
    public PCExitLanternUp(PCStateMachine pcStateMachine) : base(pcStateMachine)
    {
    }
    public override void Start()
    {
        timer = _pcStateMachine.pcController.pcReferences.pcData.exitLanternUpTimer;
        _pcStateMachine.pcController.pcReferences.rb.velocity = Vector3.zero;
    }

    public override void Update()
    {
        ExitLanternUpTimer();
    }

    private void ExitLanternUpTimer()
    {
        if (timer > 0f) timer -= Time.deltaTime;
        else
        {
            _pcStateMachine.pcController.pcReferences.pcLight.PlayLanternSwitchSound(false);
            _pcStateMachine.pcController.pcReferences.pcLight.lanternUp = false;
            _pcStateMachine.pcController.pcReferences.pcLight.LightRadiusUpdate(_pcStateMachine.pcController.pcReferences.pcHealth.currentHP);
            Transitions();
        }
    }

    #region Transitions
    private void Transitions()
    {
        Inputs inputs = _pcStateMachine.pcController.pcReferences.inputs;
        GoToMovementState(inputs);
        GoToAttackState(inputs);
        GoToDodgeState(inputs);
        GoToEnterLanternUpState(inputs);
        GoToIdleState(inputs);
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
        if (inputs.MovementInput != Vector3.zero) _pcStateMachine.SetState(new PCMoving(_pcStateMachine));
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
        if (inputs.DodgeInput) _pcStateMachine.SetState(new PCDodge(_pcStateMachine, _pcStateMachine.pcController.lookDirection));
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
