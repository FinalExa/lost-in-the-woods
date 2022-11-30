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
    private Light globalLight;
    private Color globalLightBaseColor;
    [SerializeField] private Color globalLightHeartbeatColor;
    [SerializeField] private bool testScene;
    public static Action<bool> heartbeatSwitch;
    [SerializeField] private bool hasAnticipationSound;
    [SerializeField] private float anticipationTime;
    [SerializeField] private string anticipationSound;
    [SerializeField] private bool hasHeartbeatSound;
    [SerializeField] private string heartbeatSound;

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
        if (hasAnticipationSound && heartbeatTimer <= anticipationTime && !AudioManager.Instance.IsPlaying(anticipationSound)) AudioManager.Instance.PlaySound(anticipationSound);
        if (heartbeatTimer > 0) heartbeatTimer -= Time.deltaTime;
        else
        {
            SetHeartbeatTimer(!inHeartbeat);
            if (!testScene) heartbeatSwitch(inHeartbeat);
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
            if (hasHeartbeatSound) AudioManager.Instance.PlaySound(heartbeatSound);
        }
    }
}
