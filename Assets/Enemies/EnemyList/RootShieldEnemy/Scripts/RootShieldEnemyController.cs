using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootShieldEnemyController : EnemyController, ISendSignalToSelf
{
    [HideInInspector] public bool shieldUp;
    private string shieldCounterName;
    private float shieldCurrentHealth;
    public float exposedLightDamagePerSecond;
    public float protectedLightDamagePerSecond;
    [SerializeField] private float shieldMaxHealth;
    [SerializeField] private float shieldDownTime;
    private float shieldDownTimer;
    [SerializeField] private string lightShieldName;
    [SerializeField] private string fogShieldName;
    [SerializeField] private bool randomizeShieldTypeEverytime;
    [SerializeField] private bool isFog;
    [SerializeField] private RootShieldObjectBlocker objectBlockerRef;
    private GameObject objectToBlock;

    private void OnEnable()
    {
        SetShieldUp();
    }

    private void Update()
    {
        ShieldDown();
    }

    public void SetObjectToBlock(GameObject receivedObject)
    {
        objectToBlock = receivedObject;
    }

    private void RemoveObjectBlocker()
    {

    }

    public void OnSignalReceived(GameObject source)
    {
        if (InteractionContainsName(shieldCounterName)) SetShieldDown();
    }

    public void ShieldAddValue(float valueToAdd)
    {
        shieldCurrentHealth = Mathf.Clamp(shieldCurrentHealth + valueToAdd, 0f, shieldMaxHealth);
        if (shieldCurrentHealth == 0) SetShieldDown();
    }

    private void SetShieldUp()
    {
        shieldUp = true;
        ShieldAddValue(shieldMaxHealth);
        SetShieldName();
    }

    private void SetShieldName()
    {
        if (!randomizeShieldTypeEverytime)
        {
            if (!isFog) shieldCounterName = lightShieldName;
            else shieldCounterName = fogShieldName;
        }
        else
        {
            if (Random.Range(0, 1) == 0) shieldCounterName = lightShieldName;
            else shieldCounterName = fogShieldName;
        }
    }

    private void SetShieldDown()
    {
        shieldCurrentHealth = 0;
        shieldDownTimer = shieldDownTime;
        shieldUp = false;
    }

    private void ShieldDown()
    {
        if (!shieldUp)
        {
            if (shieldDownTimer > 0) shieldDownTimer -= Time.deltaTime;
            else SetShieldUp();
        }
    }

    public override void LightStateUpdate()
    {
        if (affectedByLight.lightState == AffectedByLight.LightState.BERSERK) SetShieldUp();
    }
}
