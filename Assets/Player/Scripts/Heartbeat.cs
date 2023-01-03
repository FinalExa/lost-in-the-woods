using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeat : MonoBehaviour
{
    public float heartbeatCooldown;
    public float heartbeatDuration;
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
    [SerializeField] private UXEffect uxWithoutHeartbeat;

    private void Awake()
    {
        globalLight = GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Light>();
    }

    private void Start()
    {
        globalLightBaseColor = globalLight.color;
        uxOnAnticipation.UXEffectStartup();
        uxOnHeartbeat.UXEffectStartup();
        uxWithoutHeartbeat.UXEffectStartup();
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
            !inHeartbeat)
        {
            if (uxWithoutHeartbeat.hasSound) uxWithoutHeartbeat.sound.StopAudio();
            uxOnAnticipation.sound.PlayAudio();
        }
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
            if (uxOnHeartbeat.hasSound && uxOnHeartbeat.sound.IsPlaying()) uxOnHeartbeat.sound.StopAudio();
            if (uxWithoutHeartbeat.hasSound) uxWithoutHeartbeat.sound.PlayAudio();
        }
        else
        {
            heartbeatTimer = heartbeatDuration;
            globalLight.color = globalLightHeartbeatColor;
            if (uxOnHeartbeat.hasSound)
            {
                if (uxOnAnticipation.hasSound) uxOnAnticipation.sound.StopAudio();
                if (uxWithoutHeartbeat.hasSound) uxWithoutHeartbeat.sound.StopAudio();
                if (uxOnHeartbeat.hasSound) uxOnHeartbeat.sound.PlayAudio();
            }
        }
    }

    public void ChangeHeartbeatCooldownAndDuration(float newCooldown, float newDuration)
    {
        float currentTimerPercentageValue;
        if (inHeartbeat) currentTimerPercentageValue = GetCurrentTimerPercentage(heartbeatDuration);
        else currentTimerPercentageValue = GetCurrentTimerPercentage(heartbeatCooldown);
        heartbeatCooldown = newCooldown;
        heartbeatDuration = newDuration;
        if (inHeartbeat) heartbeatTimer = ConvertPercentageIntoTimer(currentTimerPercentageValue, heartbeatDuration);
        else heartbeatTimer = ConvertPercentageIntoTimer(currentTimerPercentageValue, heartbeatCooldown);
    }

    private float GetCurrentTimerPercentage(float maxTimerValue)
    {
        return (heartbeatTimer * 100f) / maxTimerValue;
    }

    private float ConvertPercentageIntoTimer(float percentage, float maxTimerValue)
    {
        return (percentage * maxTimerValue) / 100f;
    }
}
