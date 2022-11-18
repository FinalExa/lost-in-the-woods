using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackReceivedData", menuName = "ScriptableObjects/AttackReceivedData", order = 4)]
public class AttackReceivedData : ScriptableObject
{
    public bool ignoresDamage;
}
