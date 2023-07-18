using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCAttack : PCState
{
    private PlayerCombo playerCombo;
    private bool secondary;
    public PCAttack(PCStateMachine pcStateMachine, bool isSecondary) : base(pcStateMachine)
    {
        secondary = isSecondary;
        playerCombo = _pcStateMachine.pcController.pcReferences.pcCombo;
    }

    public override void Start()
    {
        if (!playerCombo.pcLockedAttack)
        {
            RigidbodyStop();
            playerCombo.StartHitOnWeapon(secondary);
        }
        else Transitions();
    }

    public override void Update()
    {
        if (playerCombo.GetHitOver()) Transitions();
    }

    private void RigidbodyStop()
    {
        if (_pcStateMachine.pcController.pcReferences.rb.velocity != Vector3.zero) _pcStateMachine.pcController.pcReferences.rb.velocity = new Vector3(0f, _pcStateMachine.pcController.pcReferences.rb.velocity.y, 0f);
    }

    #region Transitions
    private void Transitions()
    {
        Inputs inputs = _pcStateMachine.pcController.pcReferences.inputs;
        GoToGrabState(inputs);
        GoToIdleState(inputs);
        GoToMovementState(inputs);
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
