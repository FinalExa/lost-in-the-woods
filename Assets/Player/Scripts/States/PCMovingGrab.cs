using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCMovingGrab : PCState
{
    private Vector3 lastDirection;
    public PCMovingGrab(PCStateMachine pcStateMachine) : base(pcStateMachine)
    {
    }

    public override void Update()
    {
        UpdateSpeedValue();
        Movement();
        GetReleaseInput();
        GetLaunchInput();
        Transitions();
    }

    #region Movement
    private void UpdateSpeedValue()
    {
        _pcStateMachine.pcController.actualSpeed = _pcStateMachine.pcController.pcReferences.pcData.grabObjectMovementSpeed;
    }
    private void Movement()
    {
        Rigidbody rigidbody = _pcStateMachine.pcController.pcReferences.rb;
        PCController pcController = _pcStateMachine.pcController;
        Vector3 movementWithDirection = MovementDirection(_pcStateMachine.pcController.pcReferences.cam, _pcStateMachine.pcController.pcReferences.inputs);
        if (movementWithDirection != Vector3.zero)
        {
            lastDirection = movementWithDirection;
            pcController.pcReferences.pcCombo.LastDirection = movementWithDirection;
        }
        rigidbody.velocity = movementWithDirection * pcController.actualSpeed;
    }

    private Vector3 MovementDirection(Camera camera, Inputs inputs)
    {
        Vector3 forward = new Vector3(camera.transform.forward.x, 0f, camera.transform.forward.z).normalized;
        Vector3 right = new Vector3(camera.transform.right.x, 0f, camera.transform.right.z).normalized;
        return (inputs.MovementInput.x * forward) + (inputs.MovementInput.z * right);
    }
    #endregion

    private void GetReleaseInput()
    {
        if (_pcStateMachine.pcController.pcReferences.inputs.RightClickInput) _pcStateMachine.pcController.RemoveGrabbedObject(false);
    }

    private void GetLaunchInput()
    {
        if (_pcStateMachine.pcController.pcReferences.inputs.LeftClickInput) _pcStateMachine.pcController.RemoveGrabbedObject(true);
    }

    private void Transitions()
    {
        Inputs inputs = _pcStateMachine.pcController.pcReferences.inputs;
        if (!_pcStateMachine.pcController.GrabbedObjectExists())
        {
            GoToIdleState(inputs);
            GoToMovingState(inputs);
        }
        else GoToIdleGrabState(inputs);
    }

    private void GoToIdleState(Inputs inputs)
    {
        if (inputs.MovementInput == Vector3.zero) _pcStateMachine.SetState(new PCIdle(_pcStateMachine));
    }
    private void GoToMovingState(Inputs inputs)
    {
        if (inputs.MovementInput != Vector3.zero) _pcStateMachine.SetState(new PCMoving(_pcStateMachine));
    }

    private void GoToIdleGrabState(Inputs inputs)
    {
        if (inputs.MovementInput == Vector3.zero) _pcStateMachine.SetState(new PCIdleGrab(_pcStateMachine));
    }
}
