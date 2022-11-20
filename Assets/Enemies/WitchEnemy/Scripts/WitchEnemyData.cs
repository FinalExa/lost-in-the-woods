using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WitchEnemyData", menuName = "ScriptableObjects/Enemies/WitchEnemyData", order = 2)]
public class WitchEnemyData : EnemyData
{
    [Header("Back Leap Section")]
    public float leapTolerance;
    public float leapSpeed;
}
