using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootEnemyController : EnemyController, IHaveSpecialConditions
{
    [HideInInspector] public bool rootUnderground;
    [SerializeField] private SpriteRenderer spriteRef;
    [SerializeField] private Sprite rootUndergroundSprite;
    [SerializeField] private Sprite rootOutsideSprite;
    [SerializeField] private float rootOutsideTime;
    private float rootOutsideTimer;
    private bool isBurning;
    [SerializeField] private float burningDamageTicksTime;
    private float burningDamageTicksTimer;

    private void OnEnable()
    {
        SetRootUnderground();
    }

    private void Update()
    {
        if (!rootUnderground) RootOutsideTime();
        if (isBurning) Burning();
    }

    public override void LightStateUpdate()
    {
        if (affectedByLight.lightState == AffectedByLight.LightState.CALM) SetBurning();
        else StopBurning();
    }

    private void SetRootUnderground()
    {
        rootUnderground = true;
        spriteRef.sprite = rootUndergroundSprite;
        attackReceived.SetInvincibility(true);
    }

    private void SetBurning()
    {
        if (!isBurning)
        {
            isBurning = true;
            burningDamageTicksTimer = burningDamageTicksTime;
        }
    }

    private void StopBurning()
    {
        isBurning = false;
    }

    public void SetRootOutside()
    {
        rootUnderground = false;
        spriteRef.sprite = rootOutsideSprite;
        rootOutsideTimer = rootOutsideTime;
        attackReceived.SetInvincibility(false);
    }

    private void RootOutsideTime()
    {
        if (rootOutsideTimer > 0 && affectedByLight.lightState != AffectedByLight.LightState.BERSERK) rootOutsideTimer -= Time.deltaTime;
        else SetRootUnderground();
    }

    private void Burning()
    {
        if (burningDamageTicksTimer > 0) burningDamageTicksTimer -= Time.deltaTime;
        else
        {
            attackReceived.DealDamage(false, 1);
            burningDamageTicksTimer = burningDamageTicksTime;
        }
    }

    public bool SpecialConditions()
    {
        return isBurning;
    }
}
