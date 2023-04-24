using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCMovingLanternUp : PCState
{
    private Vector3 lastDirection;
    public PCMovingLanternUp(PCStateMachine pcStateMachine) : base(pcStateMachine)
    {
    }
    public override void Update()
    {
        CheckForLightPay();
        Movement();
        Transitions();
        UpdateSpeedValue();
    }
    private void CheckForLightPay()
    {
        _pcStateMachine.pcController.pcReferences.pcLight.LightPay(_pcStateMachine.pcController.pcReferences.inputs.LeftClickInput);
    }

    #region Movement
    private void UpdateSpeedValue()
    {
        PCData pcData = _pcStateMachine.pcController.pcReferences.pcData;
        PCController pcController = _pcStateMachine.pcController;
        pcController.actualSpeed = pcData.lightUpMovementSpeed;
    }
    private void Movement()
    {
        Rigidbody rigidbody = _pcStateMachine.pcController.pcReferences.rb;
        PCController pcController = _pcStateMachine.pcController;
        Vector3 movementWithDirection = MovementDirection(_pcStateMachine.pcController.pcReferences.cam, _pcStateMachine.pcController.pcReferences.inputs);
        if (movementWithDirection != Vector3.zero) lastDirection = movementWithDirection;
        Vector3 partialVelocity = movementWithDirection * pcController.actualSpeed;
        rigidbody.velocity = new Vector3(partialVelocity.x, rigidbody.velocity.y, partialVelocity.z);
    }

    private Vector3 MovementDirection(Camera camera, Inputs inputs)
    {
        Vector3 forward = new Vector3(camera.transform.forward.x, 0f, camera.transform.forward.z).normalized;
        Vector3 right = new Vector3(camera.transform.right.x, 0f, camera.transform.right.z).normalized;
        return (inputs.MovementInput.x * forward) + (inputs.MovementInput.z * right);
    }
    #endregion
    #region Transitions
    private void Transitions()
    {
        Inputs inputs = _pcStateMachine.pcController.pcReferences.inputs;
        GoToIdleLanternUpState(inputs);
        GoToExitLanternUpState(inputs);
    }
    #region ToIdleLanternUpState
    private void GoToIdleLanternUpState(Inputs inputs)
    {
        if (inputs.MovementInput == Vector3.zero) _pcStateMachine.SetState(new PCIdleLanternUp(_pcStateMachine));
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
