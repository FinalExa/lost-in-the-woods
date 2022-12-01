using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackReceived : MonoBehaviour
{
    private Health health;
    private AttackInteraction attackInteraction;
    public enum GameTargets { PLAYER, ENEMY, PUZZLE_ELEMENT, ENVIRONMENT }
    [SerializeField] private GameTargets thisType;
    [SerializeField] private bool ignoresDamage;

    private void Awake()
    {
        health = this.gameObject.GetComponent<Health>();
        attackInteraction = this.gameObject.GetComponent<AttackInteraction>();
    }

    public void AttackReceivedOperation(List<GameTargets> receivedTargets, float damage, List<WeaponAttack.WeaponAttackType> weaponAttackTypes, bool invulnerable)
    {
        if (receivedTargets.Contains(thisType))
        {
            if (!ignoresDamage && health != null && !invulnerable) health.HealthAddValue(-damage);
        }
        if (attackInteraction != null) attackInteraction.CheckIfAttackTypeIsTheSame(weaponAttackTypes);
    }
}
