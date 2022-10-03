using UnityEngine;
public class PCMoving : PCState
{
    private Vector3 lastDirection;
    public PCMoving(PCStateMachine pcStateMachine) : base(pcStateMachine)
    {
    }
    public override void Update()
    {
        Movement();
        Transitions();
        UpdateSpeedValue();
    }

    #region Movement
    private void UpdateSpeedValue()
    {
        PCData pcData = _pcStateMachine.pcController.pcReferences.pcData;
        PCController pcController = _pcStateMachine.pcController;
        pcController.actualSpeed = pcData.defaultMovementSpeed;
    }
    private void Movement()
    {
        Rigidbody rigidbody = _pcStateMachine.pcController.pcReferences.rb;
        PCController pcController = _pcStateMachine.pcController;
        Vector3 movementWithDirection = MovementDirection(_pcStateMachine.pcController.pcReferences.cam, _pcStateMachine.pcController.pcReferences.inputs);
        if (movementWithDirection != Vector3.zero) lastDirection = movementWithDirection;
        rigidbody.velocity = movementWithDirection * pcController.actualSpeed;
    }

    private Vector3 MovementDirection(Camera camera, Inputs inputs)
    {
        Vector3 forward = new Vector3(-camera.transform.forward.x, 0f, -camera.transform.forward.z).normalized;
        Vector3 right = new Vector3(camera.transform.right.x, 0f, camera.transform.right.z).normalized;
        return (inputs.MovementInput.x * forward) + (inputs.MovementInput.z * right);
    }
    #endregion

    #region Transitions
    private void Transitions()
    {
        Inputs inputs = _pcStateMachine.pcController.pcReferences.inputs;
        GoToIdleState(inputs);
        GoToAttackState(inputs);
        GoToDodgeState(inputs);
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
    #region ToAttackState
    private void GoToAttackState(Inputs inputs)
    {
        if (inputs.LeftClickInput && !_pcStateMachine.pcController.pcReferences.pcCombo.comboDelay)
        {
            _pcStateMachine.SetState(new PCAttack(_pcStateMachine));
            _pcStateMachine.pcController.pcReferences.rb.velocity = Vector3.zero;
        }
    }
    #endregion
    #region ToDodgeState
    private void GoToDodgeState(Inputs inputs)
    {
        if (inputs.DodgeInput)
        {
            _pcStateMachine.SetState(new PCDodge(_pcStateMachine, lastDirection));
        }
    }
    #endregion
    #endregion
}
