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
    }

    public override void FixedUpdate()
    {
        ExitLanternUpTimer();
    }

    private void ExitLanternUpTimer()
    {
        if (timer > 0f) timer -= Time.fixedDeltaTime;
        else
        {
            Debug.Log("exit");
            _pcStateMachine.pcController.pcReferences.pcLight.lanternUp = false;
            Transitions();
        }
    }

    #region Transitions
    private void Transitions()
    {
        Inputs inputs = _pcStateMachine.pcController.pcReferences.inputs;
        GoToIdleState(inputs);
        GoToMovementState(inputs);
        GoToAttackState(inputs);
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
        if (inputs.MovementInput != Vector3.zero) _pcStateMachine.SetState(new PCMoving(_pcStateMachine));
    }
    #endregion
    #region ToAttackState
    private void GoToAttackState(Inputs inputs)
    {
        if (inputs.LeftClickInput) _pcStateMachine.SetState(new PCAttack(_pcStateMachine));
    }
    #endregion
    #region ToDodgeState
    private void GoToDodgeState(Inputs inputs)
    {
        if (inputs.DodgeInput) _pcStateMachine.SetState(new PCDodge(_pcStateMachine, _pcStateMachine.pcController.pcReferences.pcData.defaultDirection));
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
