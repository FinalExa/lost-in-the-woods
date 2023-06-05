using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCLight : MonoBehaviour
{
    [SerializeField] private PCData pcData;
    [SerializeField] private GameObject lightSprite;
    private Vector3 lightRange;
    private float currentLightValue;
    private Light playerLight;
    private SphereCollider lightTrigger;
    public List<AffectedByLight> entitiesAffectedByLight;
    public List<LightPayCombo> entitiesWithLightPayCombo;
    [HideInInspector] public bool lanternUp;
    [SerializeField] private UXEffect uxOnLanternUp;
    [SerializeField] private UXEffect uxOnLanternDown;
    private PCHealth pcHealth;

    private void Awake()
    {
        playerLight = this.gameObject.GetComponent<Light>();
        lightTrigger = this.gameObject.GetComponent<SphereCollider>();
        pcHealth = this.gameObject.transform.GetComponentInParent<PCHealth>();
    }
    private void Start()
    {
        uxOnLanternUp.UXEffectStartup();
        uxOnLanternDown.UXEffectStartup();
        lightRange = new Vector3(1f, 1f, 0f);
        lightTrigger.enabled = false;
        entitiesAffectedByLight = new List<AffectedByLight>();
        entitiesWithLightPayCombo = new List<LightPayCombo>();
    }
    private void Update()
    {
        LightTriggerSet();
    }

    public void LightPay(bool receivedState)
    {
        if (entitiesWithLightPayCombo.Count > 0)
        {
            if (receivedState && pcHealth.currentHP > pcData.healthLimit)
            {
                foreach (LightPayCombo lightPayCombo in entitiesWithLightPayCombo)
                {
                    if (lightPayCombo.affectedByLight.lightState == AffectedByLight.LightState.CALM && lightPayCombo.currentWeapon != null)
                    {
                        float healthToRemove = pcData.receivedDamagePerSecond * Time.deltaTime;
                        pcHealth.HealthAddValue(-healthToRemove, false);
                        lightPayCombo.ExecuteLightPayCombo();
                    }
                }
            }
            else foreach (LightPayCombo lightPayCombo in entitiesWithLightPayCombo) lightPayCombo.StopLightPayCombo();
        }
    }

    public void LightRadiusUpdate(float currentHP)
    {
        float hpPercentage = (100f * currentHP) / pcData.maxHP;
        float lightValueToAdd = LanternModeCheck() * (hpPercentage / 100f);
        currentLightValue = pcData.minLightRadius + lightValueToAdd;
        playerLight.range = currentLightValue;
    }
    private void LightTriggerSet()
    {
        if (lightSprite.transform.localScale != lightRange * currentLightValue) lightSprite.transform.localScale = lightRange * currentLightValue;
        if (lanternUp)
        {
            if (!lightTrigger.enabled) lightTrigger.enabled = true;
            lightTrigger.radius = currentLightValue / 2;
        }
        else if (lightTrigger.enabled)
        {
            lightTrigger.enabled = false;
            ClearLightLists();
        }
    }
    private float LanternModeCheck()
    {
        float valueDiff;
        if (lanternUp) valueDiff = pcData.lightUpMaxLightRadius - pcData.lightUpMinLightRadius;
        else valueDiff = pcData.maxLightRadius - pcData.minLightRadius;
        return valueDiff;
    }

    public void PlayLanternSwitchSound(bool isUp)
    {
        if (isUp && uxOnLanternUp.hasSound) uxOnLanternUp.sound.PlayAudio();
        if (!isUp && uxOnLanternDown.hasSound) uxOnLanternDown.sound.PlayAudio();
    }

    private void OnTriggerEnter(Collider other)
    {
        AffectedByLight affectedByLight = other.gameObject.GetComponent<AffectedByLight>();
        if (affectedByLight != null && !entitiesAffectedByLight.Contains(affectedByLight))
        {
            entitiesAffectedByLight.Add(affectedByLight);
            affectedByLight.isInsideLight = true;
            affectedByLight.LightStateChange();
            LightPayCombo lightPayCombo = affectedByLight.gameObject.GetComponent<LightPayCombo>();
            if (lightPayCombo != null && !entitiesWithLightPayCombo.Contains(lightPayCombo)) entitiesWithLightPayCombo.Add(lightPayCombo);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        AffectedByLight affectedByLight = other.gameObject.GetComponent<AffectedByLight>();
        if (affectedByLight != null && entitiesAffectedByLight.Contains(affectedByLight))
        {
            entitiesAffectedByLight.Remove(affectedByLight);
            EntityExitLight(affectedByLight);
            LightPayCombo lightPayCombo = affectedByLight.gameObject.GetComponent<LightPayCombo>();
            if (lightPayCombo != null && entitiesWithLightPayCombo.Contains(lightPayCombo)) entitiesWithLightPayCombo.Remove(lightPayCombo);
        }
    }

    private void ClearLightLists()
    {
        AffectedByLight[] affectedByLightArray = entitiesAffectedByLight.ToArray();
        entitiesAffectedByLight.Clear();
        foreach (AffectedByLight affectedByLight in affectedByLightArray) EntityExitLight(affectedByLight);
        LightPayCombo[] lightPayComboArray = entitiesWithLightPayCombo.ToArray();
        entitiesWithLightPayCombo.Clear();
    }

    private void EntityExitLight(AffectedByLight affectedByLight)
    {
        affectedByLight.isInsideLight = false;
        affectedByLight.LightStateChange();
    }
}
