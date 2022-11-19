using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 3)]
public class EnemyData : ScriptableObject
{
    [Header("Normal state")]
    public float normalMovementSpeed;
    public float normalDistanceFromPlayer;
    public float normalDistanceTolerance;
    public bool hasNormalWeapon;
    public Weapon normalWeapon;
    [Header("Calm state")]
    public float calmMovementSpeed;
    public float calmDistanceFromPlayer;
    public float calmDistanceTolerance;
    public bool hasCalmWeapon;
    public Weapon calmWeapon;
    [Header("Berserk state")]
    public float berserkMovementSpeed;
    public float berserkDistanceFromPlayer;
    public float berserDistanceTolerance;
    public bool hasBerserkWeapon;
    public Weapon berserkWeapon;
}
