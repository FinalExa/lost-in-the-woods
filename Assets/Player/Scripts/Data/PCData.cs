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
    public float comboDamage;
    [Header("Movement Section")]
    public float defaultMovementSpeed;
    [Header("Dodge Section")]
    public Vector3 defaultDirection;
    public string invulnerabilityTag;
    public float dodgeDuration;
    public float dodgeDistance;
    public float dodgeStopTime;
}
