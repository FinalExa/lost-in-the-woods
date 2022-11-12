using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCLight : MonoBehaviour
{
    [SerializeField] private PCData pcData;
    private float currentLightValue;
    private Light playerLight;
    private SphereCollider lightTrigger;
    public List<EnemyController> enemies;

    [HideInInspector] public bool lanternUp;

    private void Awake()
    {
        playerLight = this.gameObject.GetComponent<Light>();
        lightTrigger = this.gameObject.GetComponent<SphereCollider>();
    }
    private void Start()
    {
        lightTrigger.enabled = false;
        enemies = new List<EnemyController>();
    }
    private void FixedUpdate()
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
        if (lanternUp)
        {
            if (!lightTrigger.enabled) lightTrigger.enabled = true;
            lightTrigger.radius = currentLightValue / 2;
        }
        else
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
            EnemyController controller = other.gameObject.GetComponent<EnemyController>();
            if (!enemies.Contains(controller))
            {
                enemies.Add(controller);
                controller.isInsideLight = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController controller = other.gameObject.GetComponent<EnemyController>();
            if (enemies.Contains(controller))
            {
                enemies.Remove(controller);
                controller.isInsideLight = false;
            }
        }
    }

    private void ClearLightList()
    {
        EnemyController[] enemiesArray = enemies.ToArray();
        enemies.Clear();
        foreach (EnemyController enemy in enemiesArray) enemy.isInsideLight = false;
    }
}
