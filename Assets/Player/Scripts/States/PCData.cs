using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "PCData", menuName = "ScriptableObjects/PCData", order = 1)]
public class PCData : ScriptableObject
{
    [Header("Health")]
    public float maxHP;
    public float healthRegenMaxTimer;
    public float healthRegenRatePerSecond;
    [Header("Light")]
    public float maxLightRadius;
    public float minLightRadius;
    public float enterLanternUpTimer;
    public float exitLanternUpTimer;
    public float lightUpMaxLightRadius;
    public float lightUpMinLightRadius;
    [Header("Movement")]
    public float defaultMovementSpeed;
    public float lightUpMovementSpeed;
    [Header("Dodge")]
    public Vector3 defaultDirection;
    public string invulnerabilityTag;
    public float dodgeDuration;
    public float dodgeDistance;
    public float dodgeStopTime;
}
