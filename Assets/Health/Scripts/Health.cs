using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [HideInInspector] public float currentHP;
    [HideInInspector] public float maxHP;
    [SerializeField] private bool hasDeathSound;
    [SerializeField] private string deathSound;
    [SerializeField] private bool hasOnHitSound;
    [SerializeField] private string onHitSound;

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
        PlayDeathSound();
        this.gameObject.SetActive(false);
    }

    protected void PlayDeathSound()
    {
        if (hasDeathSound) AudioManager.Instance.PlaySound(deathSound);
    }

    public virtual void OnHitReceived()
    {
        return;
    }

    protected void PlayOnHitSound()
    {
        if (hasOnHitSound) AudioManager.Instance.PlaySound(onHitSound);
    }
}
