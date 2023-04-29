using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCIdleGrab : PCState
{
    public PCIdleGrab(PCStateMachine pcStateMachine) : base(pcStateMachine)
    {
    }

    public override void Update()
    {
        GetReleaseInput();
        GetLaunchInput();
        Transitions();
    }

    private void GetReleaseInput()
    {
        if (_pcStateMachine.pcController.pcReferences.inputs.RightClickInput) _pcStateMachine.pcController.pcReferences.pcGrabbing.RemoveGrabbedObject(false);
    }

    private void GetLaunchInput()
    {
        if (_pcStateMachine.pcController.pcReferences.inputs.LeftClickInput) _pcStateMachine.pcController.pcReferences.pcGrabbing.RemoveGrabbedObject(true);
    }

    private void Transitions()
    {
        Inputs inputs = _pcStateMachine.pcController.pcReferences.inputs;
        if (_pcStateMachine.pcController.pcReferences.pcGrabbing.grabbedObject == null)
        {
            GoToIdleState(inputs);
            GoToMovingState(inputs);
        }
        else GoToMovingGrabState(inputs);
    }

    private void GoToIdleState(Inputs inputs)
    {
        if (inputs.MovementInput == Vector3.zero) _pcStateMachine.SetState(new PCIdle(_pcStateMachine));
    }
    private void GoToMovingState(Inputs inputs)
    {
        if (inputs.MovementInput != Vector3.zero) _pcStateMachine.SetState(new PCMoving(_pcStateMachine));
    }
    private void GoToMovingGrabState(Inputs inputs)
    {
        if (inputs.MovementInput != Vector3.zero) _pcStateMachine.SetState(new PCMovingGrab(_pcStateMachine));
    }
}
