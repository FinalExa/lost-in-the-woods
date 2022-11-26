using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [HideInInspector] public float currentHP;
    [HideInInspector] public float maxHP;

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
    }

    public virtual void OnDeath()
    {
        this.gameObject.SetActive(false);
    }
}
