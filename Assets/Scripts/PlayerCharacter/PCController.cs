using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : MonoBehaviour
{
    [HideInInspector] public string curState;
    [HideInInspector] public PCReferences pcReferences;
    [HideInInspector] public float actualSpeed;
    [HideInInspector] public float actualHP;
    private bool regenWaitBool;
    private float regenWaitTimer;
    private bool regenBool;

    private void Start()
    {
        actualHP = pcReferences.pcData.maxHP;
        regenWaitTimer = pcReferences.pcData.healthRegenMaxTimer;
    }

    private void Awake()
    {
        pcReferences = this.gameObject.GetComponent<PCReferences>();
    }

    private void FixedUpdate()
    {
        if (regenWaitBool) RegenWait();
        if (regenBool) Regen();
    }

    public void HealthAddValue(float valueToAdd)
    {
        actualHP += valueToAdd;
        actualHP = Mathf.Clamp(actualHP, 0, pcReferences.pcData.maxHP);
        LightVisualUpdate();
        RegenCheck();
    }

    public void LightVisualUpdate()
    {
        float hpPercentage = (100f * actualHP) / pcReferences.pcData.maxHP;
        float lightValueDifference = pcReferences.pcData.maxLightRadius - pcReferences.pcData.minLightRadius;
        float lightValueToAdd = lightValueDifference * (hpPercentage / 100f);
        pcReferences.playerLight.range = pcReferences.pcData.minLightRadius + lightValueToAdd;
    }


    private void RegenCheck()
    {
        if (actualHP < pcReferences.pcData.maxHP && !regenWaitBool)
        {
            regenWaitBool = true;
            if (regenBool) regenBool = false;
            regenWaitTimer = pcReferences.pcData.healthRegenMaxTimer;
        }
    }

    private void RegenWait()
    {
        if (regenWaitTimer > 0) regenWaitTimer -= Time.fixedDeltaTime;
        else
        {
            regenWaitBool = false;
            regenBool = true;
        }
    }

    private void Regen()
    {
        float valueToRegen = pcReferences.pcData.healthRegenRatePerSecond * Time.fixedDeltaTime;
        actualHP += valueToRegen;
        actualHP = Mathf.Clamp(actualHP, 0, pcReferences.pcData.maxHP);
        LightVisualUpdate();
        if (actualHP == pcReferences.pcData.maxHP) regenBool = false;
    }
}
