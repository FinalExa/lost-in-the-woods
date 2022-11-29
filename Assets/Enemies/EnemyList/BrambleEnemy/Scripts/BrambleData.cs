using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BrambleData", menuName = "ScriptableObjects/Enemies/BrambleData", order = 3)]
public class BrambleData : EnemyData
{
    [Header("Retract cooldown")]
    public float unretractedScaleSize;
    public float unretractedGrowthRatePerSecond;
    public float onHitRetractReduction;
    public float onHitRetractTime;
    public float onLightRetractTime;
}
