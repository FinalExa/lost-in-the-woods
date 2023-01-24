using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [System.Serializable]
    public struct AttackTypeInteraction
    {
        public WeaponAttack.WeaponAttackType attackType;
        public bool destroyAttackSourceObject;
        public Options options;
    }
    [System.Serializable]
    public struct NamedInteraction
    {
        public string name;
        public bool destroyNamedObjectOnInteraction;
        public Options options;
    }
    [System.Serializable]
    public struct TimeAfterStartInteraction
    {
        public bool hasTimeAfterStartInteraction;
        public float timeAfterStart;
        public bool repeats;
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
        [Header("Spawns other object")]
        public bool canSpawnObject;
        public GameObject objectToSpawn;
        public Vector3 objectSpawnPositionOffset;
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
    [SerializeField] private TimeAfterStartInteraction timeAfterStartInteraction;
    [SerializeField] private SetOfInteractions setOfInteractions;
    private InteractionOptions interactionOptions;
    private bool forcedMovementActive;
    private float forcedMovementDuration;
    private float forcedMovementDistance;
    private Vector3 forcedMovementDirection;
    [HideInInspector] public bool despawned;

    private void Start()
    {
        CreateAttackInteractionOptions();
        interactionOptions.selfObject = this.gameObject;
        interactionOptions.interaction = this;
    }

    private void OnEnable()
    {
        if (timeAfterStartInteraction.hasTimeAfterStartInteraction) LaunchTimeInteraction();
        despawned = false;
    }

    private void CreateAttackInteractionOptions()
    {
        if (interactionOptions == null) interactionOptions = new InteractionOptions();
    }

    private void Update()
    {
        if (lightInteraction.affectedByLightRef != null) ExecuteLightInteractionRefresh(lightInteraction.affectedByLightRef, lightInteraction.affectedByLightRef.lightState);
        if (forcedMovementActive) ForcedMovement();
    }
    public void CheckIfAttackTypeIsTheSame(List<WeaponAttack.WeaponAttackType> attackTypes, GameObject source)
    {
        ISendWeaponAttackType sendWeaponAttackType = this.gameObject.GetComponent<ISendWeaponAttackType>();
        foreach (AttackTypeInteraction attackTypeInteraction in attackTypeInteractions)
        {
            if (attackTypes.Contains(attackTypeInteraction.attackType))
            {
                if (sendWeaponAttackType != null) sendWeaponAttackType.ReceivedWeaponAttackType = attackTypeInteraction.attackType;
                interactionOptions.Interact(attackTypeInteraction.options, source, turnsOff);
                if (attackTypeInteraction.destroyAttackSourceObject) GameObject.Destroy(source);
            }
        }
    }

    public string NamedInteractionExecute(string enemyName, GameObject source, NamedInteractionExecutor namedInteractionRef)
    {
        foreach (NamedInteraction namedInteraction in namedInteractions)
        {
            if (namedInteraction.name == enemyName)
            {
                interactionOptions.Interact(namedInteraction.options, source, turnsOff);
                if (namedInteraction.destroyNamedObjectOnInteraction) namedInteractionRef.DestroyOnDone();
                return enemyName;
            }
        }
        return string.Empty;
    }
    public void OnDeathInteraction()
    {
        if (onDeathEnabled)
        {
            if (!despawned) interactionOptions.Interact(onDeathInteraction, this.gameObject, turnsOff);
            else despawned = false;
        }
    }

    public void ExecuteLightInteraction(AffectedByLight receivedRef, AffectedByLight.LightState receivedLightState)
    {
        if (receivedRef != null && lightInteraction.hasLightInteraction && receivedRef == lightInteraction.affectedByLightRef)
        {
            CreateAttackInteractionOptions();
            if (receivedLightState == AffectedByLight.LightState.CALM && !lightInteraction.calmRefreshes) interactionOptions.Interact(lightInteraction.calmOptions, this.gameObject, turnsOff);
            if (receivedLightState == AffectedByLight.LightState.BERSERK && !lightInteraction.berserkRefreshes) interactionOptions.Interact(lightInteraction.berserkOptions, this.gameObject, turnsOff);
            if (receivedLightState == AffectedByLight.LightState.NORMAL && !lightInteraction.normalRefreshes) interactionOptions.Interact(lightInteraction.normalOptions, this.gameObject, turnsOff);
        }
    }
    private void ExecuteLightInteractionRefresh(AffectedByLight receivedRef, AffectedByLight.LightState receivedLightState)
    {
        if (receivedRef != null && lightInteraction.hasLightInteraction && receivedRef == lightInteraction.affectedByLightRef)
        {
            CreateAttackInteractionOptions();
            if (receivedLightState == AffectedByLight.LightState.CALM && lightInteraction.calmRefreshes) interactionOptions.Interact(lightInteraction.calmOptions, this.gameObject, turnsOff);
            if (receivedLightState == AffectedByLight.LightState.BERSERK && lightInteraction.berserkRefreshes) interactionOptions.Interact(lightInteraction.berserkOptions, this.gameObject, turnsOff);
            if (receivedLightState == AffectedByLight.LightState.NORMAL && lightInteraction.normalRefreshes) interactionOptions.Interact(lightInteraction.normalOptions, this.gameObject, turnsOff);

        }
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
            float yValueSave = this.gameObject.transform.position.y;
            this.gameObject.transform.Translate(forcedMovementDirection * forcedMovementDistance * Time.deltaTime);
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, yValueSave, this.gameObject.transform.position.z);
            forcedMovementDuration -= Time.deltaTime;
        }
        else forcedMovementActive = false;
    }

    private void LaunchTimeInteraction()
    {
        StartCoroutine(TimeAfterStartInteractionExecute());
    }

    private IEnumerator TimeAfterStartInteractionExecute()
    {
        yield return new WaitForSeconds(timeAfterStartInteraction.timeAfterStart);
        interactionOptions.Interact(timeAfterStartInteraction.options, this.gameObject, turnsOff);
        if (timeAfterStartInteraction.repeats) LaunchTimeInteraction();
    }
}
