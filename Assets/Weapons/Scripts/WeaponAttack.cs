using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WeaponAttack
{
    public float damage;
    public int frameDuration;
    public int framesOfDelay;
    public float movementDistance;
    public bool ignoresWalls;
    public GameObject attackObject;
    public enum WeaponAttackType { GENERIC, PLAYER, WATER, DOOR }
    public List<WeaponAttackType> weaponAttackTypes;
    [System.Serializable]
    public struct WeaponAttackHitboxSequence
    {
        public WeaponAttackHitbox attackRef;
        public int activationFrame;
        public float deactivationFrame;
        public WeaponAttackHitboxProjectile weaponAttackHitboxProjectile;
    }
    [System.Serializable]
    public struct WeaponAttackHitboxProjectile
    {
        public bool spawnsProjectile;
        public Projectile projectile;
        public float projectileLaunchFrame;
    }
    public WeaponAttackHitboxSequence[] weaponAttackHitboxSequence;
    public bool hasAnimation;
    public UXEffect uxOnWeaponAttack;
}