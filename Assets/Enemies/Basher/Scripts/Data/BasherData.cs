using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "BasherData", menuName = "ScriptableObjects/BasherData", order = 2)]
public class BasherData : ScriptableObject
{
    [Header("Health Section")]
    public float maxHP;
    [Header("Combat Section")]
    public float attackChargeTime;
    public float postAttackTime;
    public float attackDamage;
    [Header("Movement Section")]
    public float defaultMovementSpeed;
    public float distanceFromPlayer;
}
