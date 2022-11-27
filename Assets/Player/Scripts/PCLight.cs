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
    [HideInInspector] public List<AffectedByLight> entitiesAffectedByLight;
    [HideInInspector] public bool lanternUp;

    private void Awake()
    {
        playerLight = this.gameObject.GetComponent<Light>();
        lightTrigger = this.gameObject.GetComponent<SphereCollider>();
    }
    private void Start()
    {
        lightRange = new Vector3(1f, 1f, 0f);
        lightTrigger.enabled = false;
        entitiesAffectedByLight = new List<AffectedByLight>();
    }
    private void Update()
    {
        LightTriggerSet();
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
            ClearLightList();
        }
    }
    private float LanternModeCheck()
    {
        float valueDiff;
        if (lanternUp) valueDiff = pcData.lightUpMaxLightRadius - pcData.lightUpMinLightRadius;
        else valueDiff = pcData.maxLightRadius - pcData.minLightRadius;
        return valueDiff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            AffectedByLight affectedByLight = other.gameObject.GetComponent<AffectedByLight>();
            if (!entitiesAffectedByLight.Contains(affectedByLight))
            {
                entitiesAffectedByLight.Add(affectedByLight);
                affectedByLight.isInsideLight = true;
                affectedByLight.LightStateChange();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            if (entitiesAffectedByLight.Contains(enemy.affectedByLight))
            {
                entitiesAffectedByLight.Remove(enemy.affectedByLight);
                EntityExitLight(enemy.affectedByLight);
            }
        }
    }

    private void ClearLightList()
    {
        AffectedByLight[] affectedByLightArray = entitiesAffectedByLight.ToArray();
        entitiesAffectedByLight.Clear();
        foreach (AffectedByLight affectedByLight in affectedByLightArray) EntityExitLight(affectedByLight);
    }

    private void EntityExitLight(AffectedByLight affectedByLight)
    {
        affectedByLight.isInsideLight = false;
        affectedByLight.LightStateChange();
    }
}
