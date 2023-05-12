using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCDodge : PCState
{
    private Vector3 direction;
    private string playerTag;
    private float dodgeTimer;
    private float dodgeSpeed;
    private bool startDodge;
    private bool wait;
    public PCDodge(PCStateMachine pcStateMachine, Vector3 receivedDirection) : base(pcStateMachine)
    {
        direction = receivedDirection;
    }

    public override void Start()
    {
        DodgeSetup();
    }

    public override void FixedUpdate()
    {
        if (startDodge) Dodge();
        if (wait) DodgeEndWait();
    }

    private void DodgeSetup()
    {
        PCData pcData = _pcStateMachine.pcController.pcReferences.pcData;
        playerTag = _pcStateMachine.gameObject.tag;
        _pcStateMachine.gameObject.tag = pcData.invulnerabilityTag;
        dodgeTimer = pcData.dodgeDuration;
        dodgeSpeed = pcData.dodgeDistance / pcData.dodgeDuration;
        _pcStateMachine.pcController.pcReferences.pcRotation.rotationLocked = true;
        if (_pcStateMachine.pcController.pcReferences.uxOnDodge.hasSpriteColorChange) _pcStateMachine.pcController.pcReferences.uxOnDodge.spriteColorChange.StartColorChange();
        if (_pcStateMachine.pcController.pcReferences.uxOnDodge.hasSound) _pcStateMachine.pcController.pcReferences.uxOnDodge.sound.PlayAudio();
        startDodge = true;
    }

    private void Dodge()
    {
        if (dodgeTimer > 0)
        {
            dodgeTimer -= Time.fixedDeltaTime;
            _pcStateMachine.pcController.pcReferences.rb.velocity = direction * dodgeSpeed;
        }
        else
        {
            startDodge = false;
            DodgeEndSetup();
        }
    }

    private void DodgeEndSetup()
    {
        _pcStateMachine.pcController.pcReferences.rb.velocity = Vector3.zero;
        _pcStateMachine.gameObject.tag = playerTag;
        dodgeTimer = _pcStateMachine.pcController.pcReferences.pcData.dodgeStopTime;
        wait = true;
    }

    private void DodgeEndWait()
    {
        if (dodgeTimer > 0) dodgeTimer -= Time.fixedDeltaTime;
        else
        {
            _pcStateMachine.pcController.pcReferences.pcRotation.rotationLocked = false;
            wait = false;
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
        GoToEnterLanternUpState(inputs);
    }
    #region ToIdleState
    private void GoToIdleState(Inputs inputs)
    {
        if (inputs.MovementInput == Vector3.zero)
        {
            _pcStateMachine.SetState(new PCIdle(_pcStateMachine));
            _pcStateMachine.pcController.pcReferences.rb.velocity = Vector3.zero;
        }
    }
    #endregion
    #region ToMovementState
    private void GoToMovementState(Inputs inputs)
    {
        if ((inputs.MovementInput != UnityEngine.Vector3.zero)) _pcStateMachine.SetState(new PCMoving(_pcStateMachine));
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
    #region ToEnterLanternUpState
    private void GoToEnterLanternUpState(Inputs inputs)
    {
        if (inputs.LanternInput) _pcStateMachine.SetState(new PCEnterLanternUp(_pcStateMachine));
    }
    #endregion
    #endregion
}
