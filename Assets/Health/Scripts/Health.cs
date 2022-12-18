using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [HideInInspector] public float currentHP;
    [HideInInspector] public float maxHP;
    [SerializeField] protected UXEffect uxOnDeath;
    [SerializeField] protected UXEffect uxOnHit;

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

    public virtual void OnDeath()
    {
        OnDeathSound();
        this.gameObject.SetActive(false);
    }

    public void OnHitSetSpriteColorChange()
    {
        if (uxOnHit.hasSpriteColorChange && uxOnHit.spriteColorChange.spriteRef != null)
        {
            uxOnHit.spriteColorChange.SetSpriteColor();
            StartCoroutine(OnHitSpriteColorChangeExecute(uxOnHit.spriteColorChange.spriteColorChangeDuration));
        }
    }

    public virtual void OnHitReceived()
    {
        OnHitSetSpriteColorChange();
        OnHitSound();
        OnHitCameraShake();
    }

    public void OnHitSound()
    {
        if (uxOnHit.hasSound) uxOnHit.sound.PlayAudio();
    }

    public void OnDeathSound()
    {
        if (uxOnDeath.hasSound) uxOnDeath.sound.PlayAudio();
    }

    public void OnHitCameraShake()
    {
        if (uxOnHit.hasCameraShake) uxOnHit.cameraShake.StartCameraShake();
    }

    private IEnumerator OnHitSpriteColorChangeExecute(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        uxOnHit.spriteColorChange.ResetSpriteColor();
    }
}
