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
    [SerializeField] private UXEffect uxOnAnticipation;
    [SerializeField] private float anticipationTime;
    [SerializeField] private UXEffect uxOnHeartbeat;

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
        if (uxOnAnticipation.hasSound &&
            heartbeatTimer <= anticipationTime &&
            !uxOnAnticipation.sound.IsPlaying() &&
            !inHeartbeat) uxOnAnticipation.sound.PlayAudio();
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
            if (uxOnHeartbeat.hasSound)
            {
                if (uxOnAnticipation.hasSound) uxOnAnticipation.sound.StopAudio();
                uxOnHeartbeat.sound.PlayAudio();
            }
        }
    }
}
