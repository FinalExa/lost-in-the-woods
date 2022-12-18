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
        public bool isMoved;
        public float movementDistance;
        public float movementTime;
        [Header("For special functionalities, touch only if you script")]
        public bool sendsSignalToSelf;
        public bool hasSpecialCondition;
        [Header("UX Effects on Interaction")]
        public UXEffect uxOnInteraction;
        [HideInInspector] public bool uxOnInteractionInitialized;
    }
    [SerializeField] private bool turnsOff;
    [SerializeField] private AttackTypeInteraction[] attackTypeInteractions;
    [SerializeField] private NamedInteraction[] namedInteractions;
    [SerializeField] private bool onDeathEnabled;
    [SerializeField] private Options onDeathInteraction;
    [SerializeField] private LightInteraction lightInteraction;
    private AttackInteractionOptions attackInteractionOptions;
    private bool forcedMovementActive;
    private float forcedMovementDuration;
    private float forcedMovementDistance;
    private Vector3 forcedMovementDirection;

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
        if (forcedMovementActive) ForcedMovement();
    }
    public void CheckIfAttackTypeIsTheSame(List<WeaponAttack.WeaponAttackType> attackTypes, GameObject source)
    {
        foreach (AttackTypeInteraction attackTypeInteraction in attackTypeInteractions)
        {
            if (attackTypes.Contains(attackTypeInteraction.attackType)) attackInteractionOptions.Interact(attackTypeInteraction.options, source, turnsOff);
        }
    }

    public string NamedInteractionExecute(string enemyName, GameObject source)
    {
        foreach (NamedInteraction namedInteraction in namedInteractions)
        {
            if (namedInteraction.name == enemyName)
            {
                attackInteractionOptions.Interact(namedInteraction.options, source, turnsOff);
                return enemyName;
            }
        }
        return string.Empty;
    }
    public void OnDeathInteraction()
    {
        if (onDeathEnabled) attackInteractionOptions.Interact(onDeathInteraction, this.gameObject, turnsOff);
    }
    public void OnDeathInteraction(float lifeTime)
    {
        if (onDeathEnabled) attackInteractionOptions.Interact(onDeathInteraction, this.gameObject, turnsOff, lifeTime);
    }

    private void ExecuteLightInteraction(AffectedByLight receivedRef, AffectedByLight.LightState receivedLightState)
    {
        if (receivedRef != null && lightInteraction.hasLightInteraction && receivedRef == lightInteraction.affectedByLightRef)
        {
            switch (receivedLightState)
            {
                case AffectedByLight.LightState.CALM:
                    if (!lightInteraction.calmRefreshes) attackInteractionOptions.Interact(lightInteraction.calmOptions, this.gameObject, turnsOff);
                    break;
                case AffectedByLight.LightState.BERSERK:
                    if (!lightInteraction.berserkRefreshes) attackInteractionOptions.Interact(lightInteraction.berserkOptions, this.gameObject, turnsOff);
                    break;
                case AffectedByLight.LightState.NORMAL:
                    if (!lightInteraction.normalRefreshes) attackInteractionOptions.Interact(lightInteraction.normalOptions, this.gameObject, turnsOff);
                    break;
            }
        }
    }
    private void ExecuteLightInteractionRefresh(AffectedByLight receivedRef, AffectedByLight.LightState receivedLightState)
    {
        if (receivedRef != null && lightInteraction.hasLightInteraction && receivedRef == lightInteraction.affectedByLightRef)
        {
            switch (receivedLightState)
            {
                case AffectedByLight.LightState.CALM:
                    if (lightInteraction.calmRefreshes) attackInteractionOptions.Interact(lightInteraction.calmOptions, this.gameObject, turnsOff);
                    break;
                case AffectedByLight.LightState.BERSERK:
                    if (lightInteraction.berserkRefreshes) attackInteractionOptions.Interact(lightInteraction.berserkOptions, this.gameObject, turnsOff);
                    break;
                case AffectedByLight.LightState.NORMAL:
                    if (lightInteraction.normalRefreshes) attackInteractionOptions.Interact(lightInteraction.normalOptions, this.gameObject, turnsOff);
                    break;
            }
        }
    }

    public GameObject InstantiateNew(Options options)
    {
        GameObject objectRef = Instantiate(options.transformedRef, this.gameObject.transform.position, this.gameObject.transform.rotation, this.gameObject.transform.parent);
        return objectRef;
    }

    public void StartForcedMovement(Vector3 direction, float distance, float duration)
    {
        forcedMovementDirection = direction;
        forcedMovementDistance = distance;
        forcedMovementDuration = duration;
        forcedMovementActive = true;
    }

    private void ForcedMovement()
    {
        if (forcedMovementDuration > 0)
        {
            this.gameObject.transform.Translate(forcedMovementDirection * forcedMovementDistance * Time.deltaTime);
            forcedMovementDuration -= Time.deltaTime;
        }
        else forcedMovementActive = false;
    }
}
