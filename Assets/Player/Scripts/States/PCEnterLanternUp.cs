using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCEnterLanternUp : PCState
{
    private float timer;
    public PCEnterLanternUp(PCStateMachine pcStateMachine) : base(pcStateMachine)
    {
    }
    public override void Start()
    {
        timer = _pcStateMachine.pcController.pcReferences.pcData.enterLanternUpTimer;
    }

    public override void Update()
    {
        EnterLanternUpTimer();
    }

    private void EnterLanternUpTimer()
    {
        if (timer > 0f) timer -= Time.deltaTime;
        else
        {
            _pcStateMachine.pcController.pcReferences.pcLight.lanternUp = true;
            _pcStateMachine.pcController.pcReferences.pcLight.LightRadiusUpdate(_pcStateMachine.pcController.pcReferences.pcHealth.currentHP);
            Transitions();
        }
    }

    #region Transitions
    private void Transitions()
    {
        Inputs inputs = _pcStateMachine.pcController.pcReferences.inputs;
        GoToMovementLanternUpState(inputs);
        GoToExitLanternUpState(inputs);
        GoToIdleLanternUpState(inputs);
    }
    #region ToIdleLanternUpState
    private void GoToIdleLanternUpState(Inputs inputs)
    {
        if (inputs.MovementInput == Vector3.zero) _pcStateMachine.SetState(new PCIdleLanternUp(_pcStateMachine));
    }
    #endregion
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
