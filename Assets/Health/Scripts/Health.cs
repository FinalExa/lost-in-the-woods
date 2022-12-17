using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [HideInInspector] public float currentHP;
    [HideInInspector] public float maxHP;
    [SerializeField] protected UXEffect uxOnDeath;
    [SerializeField] protected UXEffect uxOnHit;
    protected float onHitColorChangeTimer;

    protected virtual void Start()
    {
        uxOnDeath.UXEffectStartup();
        uxOnHit.UXEffectStartup();
    }

    public virtual void SetHPStartup(float givenMaxHP)
    {
        maxHP = givenMaxHP;
        currentHP = maxHP;
    }

    public virtual void HealthAddValue(float healthToAdd)
    {
        currentHP += healthToAdd;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
        if (currentHP <= 0) OnDeath();
        else if (healthToAdd < 0) OnHitReceived();
    }

    protected virtual void Update()
    {
        if (uxOnHit.hasSpriteColorChange && uxOnHit.spriteColorChange.spritehasChangedColor) SpriteColorTimer();
    }

    public virtual void OnDeath()
    {
        OnDeathSound();
        this.gameObject.SetActive(false);
    }

    public void OnHitSetSpriteColorChange()
    {
        if (uxOnHit.spriteColorChange.spriteRef != null)
        {
            onHitColorChangeTimer = uxOnHit.spriteColorChange.spriteColorChangeDuration;
            uxOnHit.spriteColorChange.SetSpriteColor();
        }
    }

    public virtual void OnHitReceived()
    {
        OnHitSetSpriteColorChange();
        OnHitSound();
    }

    public void OnHitSound()
    {
        if (uxOnHit.hasSound) uxOnHit.sound.PlayAudio();
    }

    public void OnDeathSound()
    {
        if (uxOnDeath.hasSound) uxOnDeath.sound.PlayAudio();
    }

    protected void SpriteColorTimer()
    {
        if (onHitColorChangeTimer > 0) onHitColorChangeTimer -= Time.deltaTime;
        else uxOnHit.spriteColorChange.ResetSpriteColor();
    }
}
