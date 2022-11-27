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

    private void Awake()
    {
        enemyController = this.gameObject.GetComponent<EnemyController>();
    }

    private void Start()
    {
        Heartbeat.heartbeatSwitch += HeartbeatState;
        lightState = LightState.NORMAL;
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
