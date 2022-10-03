using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCStateMachine : StateMachine
{
    [HideInInspector] public PCController pcController;
    [HideInInspector] public string thisStateName;

    public override void SetState(State state)
    {
        base.SetState(state);
        thisStateName = state.ToString();
        pcController.curState = thisStateName;
    }

    private void Awake()
    {
        pcController = this.gameObject.GetComponent<PCController>();
        SetState(new PCIdle(this));
    }

    private void OnCollisionStay(Collision collision)
    {
        _state.Collisions(collision);
    }
}
