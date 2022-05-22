using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "PCData", menuName = "ScriptableObjects/PCData", order = 1)]
public class PCData : ScriptableObject
{
    [Header("Health Section")]
    public float maxHP;
    public float maxLightRadius;
    public float minLightRadius;
    public float healthRegenMaxTimer;
    public float healthRegenRatePerSecond;
    [Header("Combat Section")]
    public float comboDelayBetweenHits;
    public float comboResetCooldown;
    public float comboEndCooldown;
    [Header("Movement section")]
    public float defaultMovementSpeed;
}
