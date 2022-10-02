using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PCHealth : Health
{
    private PCController pcController;
    private PCReferences pcReferences;
    private bool regenWaitBool;
    private float regenWaitTimer;
    private bool regenBool;
    private void Awake()
    {
        pcController = this.gameObject.GetComponent<PCController>();
        pcReferences = this.gameObject.GetComponent<PCReferences>();
    }

    private void Start()
    {
        SetHPStartup(pcReferences.pcData.maxHP);
    }

    private void FixedUpdate()
    {
        if (regenWaitBool) RegenWait();
        if (regenBool) Regen();
    }
    public override void HealthAddValue(float healthToAdd)
    {
        currentHP += healthToAdd;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        LightVisualUpdate();
        RegenCheck();
        //THIS IS SUPER TEMP
        if (currentHP <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LightVisualUpdate()
    {
        float hpPercentage = (100f * currentHP) / pcReferences.pcData.maxHP;
        float lightValueDifference = pcReferences.pcData.maxLightRadius - pcReferences.pcData.minLightRadius;
        float lightValueToAdd = lightValueDifference * (hpPercentage / 100f);
        pcReferences.playerLight.range = pcReferences.pcData.minLightRadius + lightValueToAdd;
    }


    private void RegenCheck()
    {
        if (currentHP < pcReferences.pcData.maxHP && !regenWaitBool)
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
        currentHP += valueToRegen;
        currentHP = Mathf.Clamp(currentHP, 0, pcReferences.pcData.maxHP);
        LightVisualUpdate();
        if (currentHP == pcReferences.pcData.maxHP) regenBool = false;
    }
}
