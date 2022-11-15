using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeat : MonoBehaviour
{
    [SerializeField] private float heartbeatCooldown;
    [SerializeField] private float heartbeatDuration;
    private float heartbeatTimer;
    private bool inHeartbeat;
    public static Action<bool> heartbeatSwitch;
    private Light globalLight;
    private Color globalLightBaseColor;
    [SerializeField] private Color globalLightHeartbeatColor;

    private void Awake()
    {
        globalLight = GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Light>();
    }

    private void Start()
    {
        globalLightBaseColor = globalLight.color;
        SetHeartbeatTimer(false);
    }

    private void Update()
    {
        HeartbeatTimer();
    }

    private void SetHeartbeatTimer(bool heartbeatState)
    {
        inHeartbeat = heartbeatState;
        HeartbeatStateSetup();
    }
    private void HeartbeatTimer()
    {
        if (heartbeatTimer > 0) heartbeatTimer -= Time.deltaTime;
        else
        {
            SetHeartbeatTimer(!inHeartbeat);
            heartbeatSwitch(inHeartbeat);
        }
    }

    private void HeartbeatStateSetup()
    {
        if (!inHeartbeat)
        {
            heartbeatTimer = heartbeatCooldown;
            globalLight.color = globalLightBaseColor;
        }
        else
        {
            heartbeatTimer = heartbeatDuration;
            globalLight.color = globalLightHeartbeatColor;
        }
    }
}
