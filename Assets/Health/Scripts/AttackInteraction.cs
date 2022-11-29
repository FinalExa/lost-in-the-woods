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
    public struct NamedInteraction
    {
        public string name;
        public Options options;
    }
    [System.Serializable]
    public struct LightInteraction
    {
        public bool hasLightInteraction;
        public AffectedByLight affectedByLightRef;
        public Options normalOptions;
        public Options calmOptions;
        public Options berserkOptions;
    }
    [System.Serializable]
    public struct Options
    {
        [Header("Destruction")]
        public bool isDestroyed;
        [Header("Transformation into another object")]
        public bool isTransformed;
        public GameObject transformedRef;
        [Header("Activates/Deactivates other objects")]
        public bool canSetObjectActiveStatus;
        public bool objectActiveStatus;
        public GameObject objectToSetActiveStatus;
        [Header("Rotates object in ref")]
        public bool rotates;
        public GameObject objectToRotate;
        public float rotateValue;
        [Header("For special functionalities, touch only if you script")]
        public bool sendsSignalToSelf;
    }
    [SerializeField] private bool turnsOff;
    [SerializeField] private AttackTypeInteraction[] attackTypeInteractions;
    [SerializeField] private NamedInteraction[] namedInteractions;
    [SerializeField] private bool onDeathEnabled;
    [SerializeField] private Options onDeathInteraction;
    [SerializeField] private LightInteraction lightInteraction;

    private void Start()
    {
        if (lightInteraction.hasLightInteraction && lightInteraction.affectedByLightRef != null)
        {
            AffectedByLight.lightStateChangedSignal += ExecuteLightInteraction;
        }
    }
    public void CheckIfAttackTypeIsTheSame(List<WeaponAttack.WeaponAttackType> attackTypes)
    {
        foreach (AttackTypeInteraction attackTypeInteraction in attackTypeInteractions)
        {
            if (attackTypes.Contains(attackTypeInteraction.attackType)) Interact(attackTypeInteraction.options);
        }
    }

    public void NamedInteractionExecute(string enemyName)
    {
        foreach (NamedInteraction namedInteraction in namedInteractions)
        {
            if (namedInteraction.name == enemyName) Interact(namedInteraction.options);
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

    private void ExecuteLightInteraction(AffectedByLight receivedRef, AffectedByLight.LightState receivedLightState)
    {
        if (lightInteraction.hasLightInteraction && receivedRef == lightInteraction.affectedByLightRef)
        {
            switch (receivedLightState)
            {
                case AffectedByLight.LightState.CALM:
                    Interact(lightInteraction.calmOptions);
                    break;
                case AffectedByLight.LightState.BERSERK:
                    Interact(lightInteraction.berserkOptions);
                    break;
                case AffectedByLight.LightState.NORMAL:
                    Interact(lightInteraction.normalOptions);
                    break;
            }
        }
    }

    private void Interact(Options options)
    {
        if (options.isDestroyed) DestroyOrTurnOff();
        else if (options.isTransformed && options.transformedRef != null) Transform(options);
        else if (options.sendsSignalToSelf) SendSignalToSelf();
        else if (options.canSetObjectActiveStatus) SetObjectActiveStatus(options);
        else if (options.rotates && options.objectToRotate != null) RotateObject(options);
    }
    private void Interact(Options options, float lifeTime)
    {
        if (options.isDestroyed) DestroyOrTurnOff();
        else if (options.isTransformed && options.transformedRef != null) Transform(options, lifeTime);
        else if (options.sendsSignalToSelf) SendSignalToSelf();
        else if (options.canSetObjectActiveStatus) SetObjectActiveStatus(options);
        else if (options.rotates && options.objectToRotate != null) RotateObject(options);
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
    private void SetObjectActiveStatus(Options options)
    {
        options.objectToSetActiveStatus.SetActive(options.objectActiveStatus);
    }

    private void RotateObject(Options options)
    {
        options.objectToRotate.transform.Rotate(0f, 0f, -options.rotateValue);
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
