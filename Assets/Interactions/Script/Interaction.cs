using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public SetOfInteractions setOfInteractions;
    [HideInInspector] public InteractionOptions interactionOptions;
    [HideInInspector] public NamedInteractionOperations namedInteractionOperations;
    private bool forcedMovementActive;
    private float forcedMovementDuration;
    private float forcedMovementDistance;
    private Vector3 forcedMovementDirection;
    [SerializeField] private AffectedByLight affectedByLightRef;
    public GameObject rotator;
    public GameObject objectToSetActiveStatus;
    [HideInInspector] public bool despawned;
    [HideInInspector] public bool locked;

    private void Awake()
    {
        CreateAttackInteractionOptions();
    }

    private void OnEnable()
    {
        if (setOfInteractions.startsLocked) SetLocked(setOfInteractions.lockedTime);
        if (!locked && setOfInteractions.timeAfterStartInteraction.hasTimeAfterStartInteraction) LaunchTimeInteraction();
        despawned = false;
    }

    public void SetLocked(float lockedTime)
    {
        StartCoroutine(LockedTimer(lockedTime));
    }

    private void CreateAttackInteractionOptions()
    {
        if (interactionOptions == null) interactionOptions = new InteractionOptions(this);
        if (namedInteractionOperations == null) namedInteractionOperations = new NamedInteractionOperations(this);
    }

    private void Update()
    {
        if (affectedByLightRef != null) ExecuteLightInteractionRefresh(affectedByLightRef, affectedByLightRef.lightState);
        if (forcedMovementActive) ForcedMovement();
    }
    public void CheckIfAttackTypeIsTheSame(List<WeaponAttack.WeaponAttackType> attackTypes, GameObject source)
    {
        ISendWeaponAttackType sendWeaponAttackType = this.gameObject.GetComponent<ISendWeaponAttackType>();
        foreach (SetOfInteractions.AttackTypeInteraction attackTypeInteraction in setOfInteractions.attackTypeInteractions)
        {
            if (attackTypes.Contains(attackTypeInteraction.attackType))
            {
                if (sendWeaponAttackType != null) sendWeaponAttackType.ReceivedWeaponAttackType = attackTypeInteraction.attackType;
                interactionOptions.Interact(attackTypeInteraction.options, source, setOfInteractions.turnsOff);
                if (attackTypeInteraction.destroyAttackSourceObject) GameObject.Destroy(source);
            }
        }
    }

    public void NamedInteractionExecute(NamedInteractionExecutor namedInteractionRef, GameObject source)
    {
        namedInteractionOperations.NamedInteractionEnter(namedInteractionRef, source);
    }

    public void ExitFromNamedInteraction(NamedInteractionExecutor namedInteractionRef, GameObject source)
    {
        namedInteractionOperations.NamedInteractionExit(namedInteractionRef, source);
    }
    public void OnDeathInteraction()
    {
        if (setOfInteractions.onDeathEnabled)
        {
            if (!despawned) interactionOptions.Interact(setOfInteractions.onDeathInteraction, this.gameObject, setOfInteractions.turnsOff);
            else despawned = false;
        }
    }

    public void ExecuteLightInteraction(AffectedByLight receivedRef, AffectedByLight.LightState receivedLightState)
    {
        if (receivedRef != null && setOfInteractions.lightInteraction.hasLightInteraction && receivedRef == affectedByLightRef)
        {
            CreateAttackInteractionOptions();
            if (receivedLightState == AffectedByLight.LightState.CALM && !setOfInteractions.lightInteraction.calmRefreshes) interactionOptions.Interact(setOfInteractions.lightInteraction.calmOptions, this.gameObject, setOfInteractions.turnsOff);
            if (receivedLightState == AffectedByLight.LightState.BERSERK && !setOfInteractions.lightInteraction.berserkRefreshes) interactionOptions.Interact(setOfInteractions.lightInteraction.berserkOptions, this.gameObject, setOfInteractions.turnsOff);
            if (receivedLightState == AffectedByLight.LightState.NORMAL && !setOfInteractions.lightInteraction.normalRefreshes) interactionOptions.Interact(setOfInteractions.lightInteraction.normalOptions, this.gameObject, setOfInteractions.turnsOff);
        }
    }
    private void ExecuteLightInteractionRefresh(AffectedByLight receivedRef, AffectedByLight.LightState receivedLightState)
    {
        if (receivedRef != null && setOfInteractions.lightInteraction.hasLightInteraction && receivedRef == affectedByLightRef)
        {
            CreateAttackInteractionOptions();
            if (receivedLightState == AffectedByLight.LightState.CALM && setOfInteractions.lightInteraction.calmRefreshes) interactionOptions.Interact(setOfInteractions.lightInteraction.calmOptions, this.gameObject, setOfInteractions.turnsOff);
            if (receivedLightState == AffectedByLight.LightState.BERSERK && setOfInteractions.lightInteraction.berserkRefreshes) interactionOptions.Interact(setOfInteractions.lightInteraction.berserkOptions, this.gameObject, setOfInteractions.turnsOff);
            if (receivedLightState == AffectedByLight.LightState.NORMAL && setOfInteractions.lightInteraction.normalRefreshes) interactionOptions.Interact(setOfInteractions.lightInteraction.normalOptions, this.gameObject, setOfInteractions.turnsOff);

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
        yield return new WaitForSeconds(setOfInteractions.timeAfterStartInteraction.timeAfterStart);
        interactionOptions.Interact(setOfInteractions.timeAfterStartInteraction.options, this.gameObject, setOfInteractions.turnsOff);
        if (setOfInteractions.timeAfterStartInteraction.repeats) LaunchTimeInteraction();
    }

    private IEnumerator LockedTimer(float lockedTime)
    {
        locked = true;
        yield return new WaitForSeconds(lockedTime);
        locked = false;
    }

    public void ExecuteCallByCodeInteraction()
    {
        interactionOptions.Interact(setOfInteractions.callByCodeInteraction, this.gameObject, setOfInteractions.turnsOff);
    }
}
