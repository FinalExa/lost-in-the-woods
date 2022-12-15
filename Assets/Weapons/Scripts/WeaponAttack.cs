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
    public bool ignoresWalls;
    public GameObject attackObject;
    public enum WeaponAttackType { GENERIC, PLAYER, WATER, DOOR }
    public List<WeaponAttackType> weaponAttackTypes;
    [System.Serializable]
    public struct WeaponAttackHitboxSequence
    {
        public WeaponAttackHitbox attackRef;
        public float activationDelayAfterStart;
        public float deactivationDelayAfterStart;
        public WeaponAttackHitboxProjectile weaponAttackHitboxProjectile;
    }
    [System.Serializable]
    public struct WeaponAttackHitboxProjectile
    {
        public bool spawnsProjectile;
        public Projectile projectile;
        public float projectileLaunchTimeAfterStart;
    }
    public WeaponAttackHitboxSequence[] weaponAttackHitboxSequence;
    public bool hasAnimation;
    public bool playsSound;
    public string soundToPlay;
}