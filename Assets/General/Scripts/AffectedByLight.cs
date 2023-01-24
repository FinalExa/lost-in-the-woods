using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffectedByLight : MonoBehaviour
{
    public enum LightState { CALM, NORMAL, BERSERK }
    [HideInInspector] public LightState lightState;
    [HideInInspector] public LightState previousLightState;
    [HideInInspector] public bool isInsideLight;
    [HideInInspector] public bool isInHeartbeatState;
    [HideInInspector] public EnemyController enemyController;
    [HideInInspector] public Interaction interaction;

    private void Awake()
    {
        enemyController = this.gameObject.GetComponent<EnemyController>();
        interaction = this.gameObject.GetComponent<Interaction>();
    }

    private void Start()
    {
        Heartbeat.heartbeatSwitch += HeartbeatState;
        isInHeartbeatState = FindObjectOfType<Heartbeat>().InHeartbeat;
        LightStateChange();
        previousLightState = lightState;
    }
    private void HeartbeatState(bool heartbeatState)
    {
        isInHeartbeatState = heartbeatState;
        LightStateChange();
    }
    public virtual void LightStateChange()
    {
        if (isInsideLight && !isInHeartbeatState) lightState = LightState.CALM;
        else if (isInHeartbeatState && !isInsideLight) lightState = LightState.BERSERK;
        else lightState = LightState.NORMAL;
        if (enemyController != null) enemyController.LightStateUpdate();
        if (interaction != null) interaction.ExecuteLightInteraction(this, lightState);
    }

    public bool CheckForSwitchState()
    {
        if (lightState != previousLightState)
        {
            previousLightState = lightState;
            return true;
        }
        else return false;
    }
}
