using UnityEngine;
public class PCMoving : PCState
{
    private Vector3 lastDirection;
    public PCMoving(PCStateMachine pcStateMachine) : base(pcStateMachine)
    {
    }

    public override void Start()
    {
        StartWalkingSound();
    }
    public override void Update()
    {
        UpdateSpeedValue();
        Movement();
        Transitions();
    }
    private void StartWalkingSound()
    {
        if (_pcStateMachine.pcController.pcReferences.uxOnWalking.hasSound) _pcStateMachine.pcController.pcReferences.uxOnWalking.sound.PlayAudio();
    }
    private void EndWalkingSound()
    {
        if (_pcStateMachine.pcController.pcReferences.uxOnWalking.hasSound) _pcStateMachine.pcController.pcReferences.uxOnWalking.sound.StopAudio();
    }
    private void UpdateSpeedValue()
    {
        _pcStateMachine.pcController.actualSpeed = _pcStateMachine.pcController.pcReferences.pcData.defaultMovementSpeed;
    }

    private void Movement()
    {
        Rigidbody rigidbody = _pcStateMachine.pcController.pcReferences.rb;
        PCController pcController = _pcStateMachine.pcController;
        Vector3 movementDirection = MovementDirection(_pcStateMachine.pcController.pcReferences.cam, _pcStateMachine.pcController.pcReferences.inputs);
        if (movementDirection != Vector3.zero)
        {
            lastDirection = movementDirection;
            pcController.pcReferences.pcCombo.LastDirection = movementDirection;
            pcController.lookDirection = movementDirection;
        }
        Vector3 partialVelocity = movementDirection * pcController.actualSpeed;
        rigidbody.velocity = new Vector3(partialVelocity.x, rigidbody.velocity.y, partialVelocity.z);
    }

    private Vector3 MovementDirection(Camera camera, Inputs inputs)
    {
        Vector3 forward = new Vector3(camera.transform.forward.x, 0f, camera.transform.forward.z).normalized;
        Vector3 right = new Vector3(camera.transform.right.x, 0f, camera.transform.right.z).normalized;
        return (inputs.MovementInput.x * forward) + (inputs.MovementInput.z * right);
    }
    private void Transitions()
    {
        Inputs inputs = _pcStateMachine.pcController.pcReferences.inputs;
        GoToGrabState(inputs);
        GoToIdleState(inputs);
        GoToAttackState(inputs);
        GoToDodgeState(inputs);
        GoToEnterLanternUpState(inputs);
    }
    private void GoToGrabState(Inputs inputs)
    {
        if (_pcStateMachine.pcController.pcReferences.pcGrabbing.GrabbedObjectExists())
        {
            EndWalkingSound();
            if (inputs.MovementInput == Vector3.zero) _pcStateMachine.SetState(new PCIdleGrab(_pcStateMachine));
            else _pcStateMachine.SetState(new PCMovingGrab(_pcStateMachine));
        }
    }
    private void GoToIdleState(Inputs inputs)
    {
        if (inputs.MovementInput == Vector3.zero)
        {
            EndWalkingSound();
            _pcStateMachine.SetState(new PCIdle(_pcStateMachine));
            _pcStateMachine.pcController.pcReferences.rb.velocity = Vector3.zero;
        }
    }
    private void GoToAttackState(Inputs inputs)
    {
        if (inputs.LeftClickInput || inputs.RightClickInput)
        {
            EndWalkingSound();
            if (inputs.RightClickInput) _pcStateMachine.SetState(new PCAttack(_pcStateMachine, true));
            else _pcStateMachine.SetState(new PCAttack(_pcStateMachine, false));
            _pcStateMachine.pcController.pcReferences.rb.velocity = Vector3.zero;
        }
    }
    private void GoToDodgeState(Inputs inputs)
    {
        if (inputs.DodgeInput)
        {
            EndWalkingSound();
            _pcStateMachine.SetState(new PCDodge(_pcStateMachine, lastDirection));
        }
    }
    private void GoToEnterLanternUpState(Inputs inputs)
    {
        if (inputs.LanternInput)
        {
            EndWalkingSound();
            _pcStateMachine.SetState(new PCEnterLanternUp(_pcStateMachine));
            _pcStateMachine.pcController.pcReferences.rb.velocity = Vector3.zero;
        }
    }
}
