using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PCHealth : Health
{
    private PCReferences pcReferences;
    private PCLight pcLight;
    private bool regenWait;
    private float regenWaitTimer;
    private bool regen;
    private void Awake()
    {
        pcReferences = this.gameObject.GetComponent<PCReferences>();
        pcLight = this.gameObject.GetComponentInChildren<PCLight>();
    }

    private void Start()
    {
        SetHPStartup(pcReferences.pcData.maxHP);
    }

    private void Update()
    {
        RegenCheck();
        RegenWait();
        Regen();
    }
    public override void HealthAddValue(float healthToAdd)
    {
        currentHP += healthToAdd;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        if (healthToAdd < 0)
        {
            regenWait = false;
            regen = false;
        }
        pcLight.LightRadiusUpdate(currentHP);
        //THIS IS SUPER TEMP
        if (currentHP <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void RegenCheck()
    {
        if (currentHP < pcReferences.pcData.maxHP && !regenWait && !regen)
        {
            regenWait = true;
            if (regen) regen = false;
            regenWaitTimer = pcReferences.pcData.healthRegenMaxTimer;
        }
    }

    private void RegenWait()
    {
        if (regenWait)
        {
            if (regenWaitTimer > 0) regenWaitTimer -= Time.deltaTime;
            else
            {
                regenWait = false;
                regen = true;
            }
        }
    }

    private void Regen()
    {
        if (regen)
        {
            float valueToRegen = pcReferences.pcData.healthRegenRatePerSecond * Time.deltaTime;
            HealthAddValue(valueToRegen);
            if (currentHP == pcReferences.pcData.maxHP) regen = false;
        }
    }

    public override void SetHPStartup(float givenMaxHP)
    {
        base.SetHPStartup(givenMaxHP);
        pcLight.LightRadiusUpdate(currentHP);
    }
}
