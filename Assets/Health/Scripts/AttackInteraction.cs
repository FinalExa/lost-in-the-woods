using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInteraction : MonoBehaviour
{
    [System.Serializable]
    public struct AttackTypeInteraction
    {
        public WeaponAttack.WeaponAttackType attackType;
        public Options options;
    }
    [System.Serializable]
    public struct EnemyInteraction
    {
        public string enemyName;
        public Options options;
    }
    [System.Serializable]
    public struct Options
    {
        public bool isDestroyed;
        public bool isTransformed;
        public GameObject transformedRef;
    }
    [SerializeField] private AttackTypeInteraction[] attackTypeInteractions;
    [SerializeField] private EnemyInteraction[] enemyInteractions;

    public void CheckIfAttackTypeIsTheSame(List<WeaponAttack.WeaponAttackType> attackTypes)
    {
        foreach (AttackTypeInteraction attackTypeInteraction in attackTypeInteractions)
        {
            if (attackTypes.Contains(attackTypeInteraction.attackType)) Interact(attackTypeInteraction.options);
        }
    }

    public void CheckIfEnemyIsTheSame(string enemyName)
    {
        foreach (EnemyInteraction enemyInteraction in enemyInteractions)
        {
            if (enemyInteraction.enemyName == enemyName) Interact(enemyInteraction.options);
        }
    }

    private void Interact(Options options)
    {
        if (options.isDestroyed) GameObject.Destroy(this.gameObject);
        else if (options.isTransformed)
        {
            Instantiate(options.transformedRef, this.gameObject.transform.position, this.gameObject.transform.rotation, this.gameObject.transform.parent);
            GameObject.Destroy(this.gameObject);
        }
    }
}
