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

    protected override void Start()
    {
        base.Start();
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
        base.HealthAddValue(healthToAdd);
        pcLight.LightRadiusUpdate(currentHP);
    }

    public override void OnDeath()
    {
        if (uxOnDeath.hasSound) uxOnDeath.sound.PlayAudio();
        //TEMPORARY
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public override void OnHitReceived()
    {
        base.OnHitReceived();
        regenWait = false;
        regen = false;
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
