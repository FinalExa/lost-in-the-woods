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
    private GameSaveSystem gameSave;
    private void Awake()
    {
        pcReferences = this.gameObject.GetComponent<PCReferences>();
        pcLight = this.gameObject.GetComponentInChildren<PCLight>();
        gameSave = FindObjectOfType<GameSaveSystem>();
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
    public override void HealthAddValue(float healthToAdd, bool feedbackActive)
    {
        base.HealthAddValue(healthToAdd, feedbackActive);
        pcLight.LightRadiusUpdate(currentHP);
    }

    public override void OnDeath(bool skipOnDeathInteraction)
    {
        if (uxOnDeath.hasSound) uxOnDeath.sound.PlayAudio();
        gameSave.LoadData();
        HealthAddValue(maxHP, true);
    }

    public override void OnHitReceived(bool feedbackActive)
    {
        base.OnHitReceived(feedbackActive);
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
            HealthAddValue(valueToRegen, true);
            if (currentHP == pcReferences.pcData.maxHP) regen = false;
        }
    }

    public override void SetHPStartup(float givenMaxHP)
    {
        base.SetHPStartup(givenMaxHP);
        pcLight.LightRadiusUpdate(currentHP);
    }
}
