using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackReceivedData", menuName = "ScriptableObjects/AttackReceivedData", order = 4)]
public class AttackReceivedData : ScriptableObject
{
    public enum GameTargets { PLAYER, ENEMY, PUZZLE_ELEMENT }
    public GameTargets targetType;
    public bool ignoresDamage;
}
