using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCLight : MonoBehaviour
{
    private PCReferences pcReferences;
    private float currentLightValue;

    [SerializeField] private Light playerLight;
    [SerializeField] private Collider LightCollider;

    public bool lanternUp;

    private void Awake()
    {
        pcReferences = this.gameObject.GetComponent<PCReferences>();
    }
    private void FixedUpdate()
    {
        LightVisualUpdate();
    }
    private void LightVisualUpdate()
    {
        float hpPercentage = (100f * pcReferences.pcHealth.currentHP) / pcReferences.pcData.maxHP;
        float lightValueToAdd = LanternModeCheck() * (hpPercentage / 100f);
        currentLightValue = pcReferences.pcData.minLightRadius + lightValueToAdd;
        playerLight.range = currentLightValue;
    }
    private float LanternModeCheck()
    {
        float valueDiff;
        if (lanternUp) valueDiff = pcReferences.pcData.lightUpMaxLightRadius - pcReferences.pcData.lightUpMinLightRadius;
        else valueDiff = pcReferences.pcData.maxLightRadius - pcReferences.pcData.minLightRadius;
        return valueDiff;
    }
}
