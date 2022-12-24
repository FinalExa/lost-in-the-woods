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
    public enum WeaponAttackType { GENERIC, PLAYER, WATER, DOOR, FIRE }
    public List<WeaponAttackType> weaponAttackTypes;
    [System.Serializable]
    public struct WeaponAttackHitboxSequence
    {
        public WeaponAttackHitbox attackRef;
        public float activationDelayAfterStart;
        public float deactivationDelayAfterStart;
    }
    [System.Serializable]
    public struct WeaponSpawnsObjectDuringThisAttack
    {
        public GameObject objectRef;
        public GameObject objectStartPosition;
        public float launchTimeAfterStart;
        [HideInInspector] public bool spawned;
    }
    public WeaponAttackHitboxSequence[] weaponAttackHitboxSequence;
    public WeaponSpawnsObjectDuringThisAttack[] weaponSpawnsObjectDuringThisAttack;
    public bool hasAnimation;
    public UXEffect uxOnWeaponAttack;
}