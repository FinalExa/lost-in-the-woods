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
        public bool normalRefreshes;
        public Options normalOptions;
        public bool calmRefreshes;
        public Options calmOptions;
        public bool berserkRefreshes;
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
        [Header("Moves by a certain amount of space this gameObject towards direction for a certain amount of time")]
        public bool movesThis;
        public float spaceToMove;
        public float movementTime;
        [Header("For special functionalities, touch only if you script")]
        public bool sendsSignalToSelf;
        public bool hasSpecialCondition;
        [Header("Sound to play")]
        public bool playsSoundOnInteraction;
        public string soundToPlay;
    }
    [SerializeField] private bool turnsOff;
    [SerializeField] private AttackTypeInteraction[] attackTypeInteractions;
    [SerializeField] private NamedInteraction[] namedInteractions;
    [SerializeField] private bool onDeathEnabled;
    [SerializeField] private Options onDeathInteraction;
    [SerializeField] private LightInteraction lightInteraction;
    private AttackInteractionOptions attackInteractionOptions;

    private void Start()
    {
        AffectedByLight.lightStateChangedSignal += ExecuteLightInteraction;
        attackInteractionOptions = new AttackInteractionOptions();
        attackInteractionOptions.selfObject = this.gameObject;
        attackInteractionOptions.attackInteraction = this;
    }

    private void Update()
    {
        if (lightInteraction.affectedByLightRef != null) ExecuteLightInteractionRefresh(lightInteraction.affectedByLightRef, lightInteraction.affectedByLightRef.lightState);
    }
    public void CheckIfAttackTypeIsTheSame(List<WeaponAttack.WeaponAttackType> attackTypes, GameObject source)
    {
        foreach (AttackTypeInteraction attackTypeInteraction in attackTypeInteractions)
        {
            if (attackTypes.Contains(attackTypeInteraction.attackType))
            {
                if (!attackTypeInteraction.options.movesThis) attackInteractionOptions.Interact(attackTypeInteraction.options, turnsOff);
                else
                {
                    Vector3 direction = (this.gameObject.transform.position - source.transform.position);
                    attackInteractionOptions.Interact(attackTypeInteraction.options, turnsOff, direction);
                }
            }
        }
    }

    public string NamedInteractionExecute(string enemyName)
    {
        foreach (NamedInteraction namedInteraction in namedInteractions)
        {
            if (namedInteraction.name == enemyName)
            {
                attackInteractionOptions.Interact(namedInteraction.options, turnsOff);
                return enemyName;
            }
        }
        return string.Empty;
    }
    public void OnDeathInteraction()
    {
        if (onDeathEnabled) attackInteractionOptions.Interact(onDeathInteraction, turnsOff);
    }
    public void OnDeathInteraction(float lifeTime)
    {
        if (onDeathEnabled) attackInteractionOptions.Interact(onDeathInteraction, turnsOff, lifeTime);
    }

    private void ExecuteLightInteraction(AffectedByLight receivedRef, AffectedByLight.LightState receivedLightState)
    {
        if (lightInteraction.hasLightInteraction && receivedRef == lightInteraction.affectedByLightRef)
        {
            switch (receivedLightState)
            {
                case AffectedByLight.LightState.CALM:
                    if (!lightInteraction.calmRefreshes) attackInteractionOptions.Interact(lightInteraction.calmOptions, turnsOff);
                    break;
                case AffectedByLight.LightState.BERSERK:
                    if (!lightInteraction.berserkRefreshes) attackInteractionOptions.Interact(lightInteraction.berserkOptions, turnsOff);
                    break;
                case AffectedByLight.LightState.NORMAL:
                    if (!lightInteraction.normalRefreshes) attackInteractionOptions.Interact(lightInteraction.normalOptions, turnsOff);
                    break;
            }
        }
    }
    private void ExecuteLightInteractionRefresh(AffectedByLight receivedRef, AffectedByLight.LightState receivedLightState)
    {
        if (lightInteraction.hasLightInteraction && receivedRef == lightInteraction.affectedByLightRef)
        {
            switch (receivedLightState)
            {
                case AffectedByLight.LightState.CALM:
                    if (lightInteraction.calmRefreshes) attackInteractionOptions.Interact(lightInteraction.calmOptions, turnsOff);
                    break;
                case AffectedByLight.LightState.BERSERK:
                    if (lightInteraction.berserkRefreshes) attackInteractionOptions.Interact(lightInteraction.berserkOptions, turnsOff);
                    break;
                case AffectedByLight.LightState.NORMAL:
                    if (lightInteraction.normalRefreshes) attackInteractionOptions.Interact(lightInteraction.normalOptions, turnsOff);
                    break;
            }
        }
    }

    public GameObject InstantiateNew(Options options)
    {
        GameObject objectRef = Instantiate(options.transformedRef, this.gameObject.transform.position, this.gameObject.transform.rotation, this.gameObject.transform.parent);
        return objectRef;
    }
}
