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
    public enum WeaponAttackType { GENERIC, PLAYER, INFESTATION, LIGHT, WIND, CORRUPTION_BOMB, PLAYER_SECONDARY }
    public List<WeaponAttackType> weaponAttackTypes;
    [System.Serializable]
    public struct WeaponAttackHitboxSequence
    {
        public WeaponAttackHitbox attackRef;
        public int activationFrame;
        public float deactivationFrame;
    }
    [System.Serializable]
    public struct WeaponSpawnsObjectDuringThisAttack
    {
        public GameObject objectRef;
        public GameObject objectStartPosition;
        public int launchFrame;
        public float launchSpeed;
        [HideInInspector] public bool spawned;
    }
    public WeaponAttackHitboxSequence[] weaponAttackHitboxSequence;
    public WeaponSpawnsObjectDuringThisAttack[] weaponSpawnsObjectDuringThisAttack;
    public bool hasAnimation;
    public UXEffect uxOnWeaponAttack;
}