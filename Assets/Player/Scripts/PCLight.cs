using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCLight : MonoBehaviour
{
    private PCReferences pcReferences;
    private float currentLightValue;
    private Light playerLight;
    private SphereCollider lightTrigger;

    [HideInInspector] public bool lanternUp;

    private void Awake()
    {
        pcReferences = this.gameObject.GetComponentInParent<PCReferences>();
        playerLight = this.gameObject.GetComponent<Light>();
        lightTrigger = this.gameObject.GetComponent<SphereCollider>();
    }
    private void Start()
    {
        lightTrigger.enabled = false;
    }
    private void FixedUpdate()
    {
        LightRadiusUpdate();
    }
    private void LightRadiusUpdate()
    {
        float hpPercentage = (100f * pcReferences.pcHealth.currentHP) / pcReferences.pcData.maxHP;
        float lightValueToAdd = LanternModeCheck() * (hpPercentage / 100f);
        currentLightValue = pcReferences.pcData.minLightRadius + lightValueToAdd;
        playerLight.range = currentLightValue;
        LightTriggerSet();
    }
    private void LightTriggerSet()
    {
        if (lanternUp)
        {
            if (!lightTrigger.enabled) lightTrigger.enabled = true;
            lightTrigger.radius = currentLightValue / 2;

        }
        else lightTrigger.enabled = false;
    }
    private float LanternModeCheck()
    {
        float valueDiff;
        if (lanternUp) valueDiff = pcReferences.pcData.lightUpMaxLightRadius - pcReferences.pcData.lightUpMinLightRadius;
        else valueDiff = pcReferences.pcData.maxLightRadius - pcReferences.pcData.minLightRadius;
        return valueDiff;
    }
}
