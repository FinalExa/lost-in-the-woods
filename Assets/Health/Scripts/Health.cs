using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [HideInInspector] public float currentHP;
    [HideInInspector] public float maxHP;
    [SerializeField] protected bool hasDeathSound;
    [SerializeField] protected string deathSound;
    [SerializeField] protected bool hasOnHitSound;
    [SerializeField] protected string onHitSound;
    [SerializeField] protected bool hasOnHitSpriteColorChange;
    [SerializeField] protected float spriteColorChangeDuration;
    [SerializeField] protected Color onHitSpriteColor;
    protected bool spritehasChangedColor;
    protected float spriteColorChangeTimer;
    protected SpriteRenderer spriteRef;
    protected Color spriteRefBaseColor;

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
        if (hasOnHitSpriteColorChange && spritehasChangedColor) SpriteColorTimer();
    }

    public virtual void OnDeath()
    {
        PlayDeathSound();
        this.gameObject.SetActive(false);
    }

    protected void PlayDeathSound()
    {
        if (hasDeathSound) AudioManager.Instance.PlaySound(deathSound);
    }

    public virtual void OnHitReceived()
    {
        SetSpriteColorChange();
    }

    protected void PlayOnHitSound()
    {
        if (hasOnHitSound) AudioManager.Instance.PlaySound(onHitSound);
    }

    protected virtual void SetSpriteRenderer()
    {
        return;
    }

    protected void SetSpriteColorChange()
    {
        if (spriteRef == null) SetSpriteRenderer();
        if (hasOnHitSpriteColorChange && spriteRef != null)
        {
            spriteRef.color = onHitSpriteColor;
            spriteColorChangeTimer = spriteColorChangeDuration;
            spritehasChangedColor = true;
        }
    }

    protected void SpriteColorTimer()
    {
        if (spriteColorChangeTimer > 0) spriteColorChangeTimer -= Time.deltaTime;
        else
        {
            spriteRef.color = spriteRefBaseColor;
            spritehasChangedColor = false;
        }
    }
}
