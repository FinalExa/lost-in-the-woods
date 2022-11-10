using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WeaponAttack
{
    public float damage;
    public float duration;
    public float afterDelay;
    public float movementDistance;
    public GameObject attackObject;
    [System.Serializable]
    public struct WeaponAttackHitboxSequence
    {
        public WeaponAttackHitbox attackRef;
        public float activationDelayAfterStart;
        public float deactivationDelayAfterStart;
        public bool spawnsProjectile;
        public Projectile projectile;
        public float projectileLaunchTimeAfterStart;
    }
    public WeaponAttackHitboxSequence[] weaponAttackHitboxSequence;
    public bool hasAnimation;
}