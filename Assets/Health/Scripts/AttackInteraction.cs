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
        public bool sendsSignalToSelf;
    }
    [SerializeField] private bool turnsOff;
    [SerializeField] private AttackTypeInteraction[] attackTypeInteractions;
    [SerializeField] private EnemyInteraction[] enemyInteractions;
    [SerializeField] private bool onDeathEnabled;
    [SerializeField] private Options onDeathInteraction;
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
    public void OnDeathInteraction()
    {
        if (onDeathEnabled) Interact(onDeathInteraction);
    }
    public void OnDeathInteraction(float lifeTime)
    {
        if (onDeathEnabled) Interact(onDeathInteraction, lifeTime);
    }

    private void Interact(Options options)
    {
        if (options.isDestroyed) DestroyOrTurnOff();
        else if (options.isTransformed) Transform(options);
        else if (options.sendsSignalToSelf) SendSignalToSelf();
    }
    private void Interact(Options options, float lifeTime)
    {
        if (options.isDestroyed) DestroyOrTurnOff();
        else if (options.isTransformed) Transform(options, lifeTime);
        else if (options.sendsSignalToSelf) SendSignalToSelf();
    }

    private GameObject Transform(Options options)
    {
        GameObject objectRef = Instantiate(options.transformedRef, this.gameObject.transform.position, this.gameObject.transform.rotation, this.gameObject.transform.parent);
        DestroyOrTurnOff();
        return objectRef;
    }
    private GameObject Transform(Options options, float lifeTime)
    {
        GameObject objectRef = Instantiate(options.transformedRef, this.gameObject.transform.position, this.gameObject.transform.rotation, this.gameObject.transform.parent);
        Lifetime lifetimeRef = objectRef.GetComponent<Lifetime>();
        if (lifetimeRef != null) lifetimeRef.SetTimer(lifeTime);
        DestroyOrTurnOff();
        return objectRef;
    }

    private void SendSignalToSelf()
    {
        ISendSignalToSelf sendSignalToSelf = this.gameObject.GetComponent<ISendSignalToSelf>();
        if (sendSignalToSelf != null) sendSignalToSelf.OnSignalReceived();
    }

    private void DestroyOrTurnOff()
    {
        if (!turnsOff) GameObject.Destroy(this.gameObject);
        else this.gameObject.SetActive(false);
    }
}
