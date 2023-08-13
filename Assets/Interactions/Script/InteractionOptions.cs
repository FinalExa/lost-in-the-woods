using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionOptions
{
    public GameObject selfObject;
    public Interaction interaction;
    public InteractionOptions(Interaction reference)
    {
        interaction = reference;
        selfObject = reference.gameObject;
    }
    public void Interact(SetOfInteractions.Options options, GameObject source, bool turnsOff)
    {
        if (!interaction.locked && (!options.hasSpecialCondition || SpecialConditionsCheck(options)))
        {
            UXEffectExecute(options);
            if (options.isDestroyed) DestroyOrTurnOff(turnsOff);
            else if (options.isTransformed && options.transformedRef != null) Transform(options, turnsOff);
            else if (options.canSpawnObject && options.objectToSpawn != null) SpawnObject(options.objectToSpawn, options.objectSpawnPositionOffset);
            else if (options.sendsSignalToSelf) SendSignalToSelf(source);
            else if (options.secondaryAttackInteraction) SecondaryAttackInteraction();
            else if (options.isMoved) MoveObject(options, source.transform.position - selfObject.transform.position);
            else if (options.canSetObjectActiveStatus) SetObjectActiveStatus(options);
            else if (options.rotates) RotateObject(options);
        }
    }

    private bool SpecialConditionsCheck(SetOfInteractions.Options options)
    {
        bool check = false;
        if (options.hasSpecialCondition)
        {
            IHaveSpecialConditions haveSpecialConditions = interaction.GetComponent<IHaveSpecialConditions>();
            if (haveSpecialConditions != null) check = haveSpecialConditions.SpecialConditions();
        }
        return check;
    }

    private GameObject SpawnObject(GameObject objectToSpawn, Vector3 offset)
    {
        GameObject instantiatedObject = GameObject.Instantiate(objectToSpawn, interaction.gameObject.transform.position + offset, Quaternion.identity, interaction.gameObject.transform.parent);
        GrabbableByPlayer thisGrabbableByPlayer = interaction.gameObject.GetComponent<GrabbableByPlayer>();
        GrabbableByPlayer grabbableByPlayerOfSpawnedObject = instantiatedObject.GetComponent<GrabbableByPlayer>();
        if (grabbableByPlayerOfSpawnedObject != null)
        {
            grabbableByPlayerOfSpawnedObject.ManualStartup();
            grabbableByPlayerOfSpawnedObject.ReleaseFromBeingGrabbed();
            Transform parent;
            if (thisGrabbableByPlayer != null)
            {
                parent = thisGrabbableByPlayer.startParent;
                if (interaction.pcGrabbing != null && interaction.pcGrabbing.grabbedObject == thisGrabbableByPlayer) grabbableByPlayerOfSpawnedObject.needsToBeGrabbedAgainByPlayer = true;
            }
            else parent = interaction.gameObject.transform.parent;
            grabbableByPlayerOfSpawnedObject.SetStartParent(parent);
            instantiatedObject.transform.parent = parent;
        }
        return instantiatedObject;
    }

    private void Transform(SetOfInteractions.Options options, bool turnsOff)
    {
        GameObject spawnedObject = SpawnObject(options.transformedRef, Vector3.zero);
        PlantSignalSet plantSignalSetOfSpawnedObject = spawnedObject.gameObject.GetComponent<PlantSignalSet>();
        if (!options.hasPlantId)
        {
            PlantSignalSet plantSignalSetOfThisObject = interaction.gameObject.GetComponent<PlantSignalSet>();
            if (plantSignalSetOfThisObject != null && plantSignalSetOfSpawnedObject != null) plantSignalSetOfSpawnedObject.startingState = plantSignalSetOfThisObject.currentState;
        }
        else if (plantSignalSetOfSpawnedObject != null) plantSignalSetOfSpawnedObject.startingState = options.plantId;
        DestroyOrTurnOff(turnsOff);
    }

    private void SetObjectActiveStatus(SetOfInteractions.Options options)
    {
        if (interaction.objectToSetActiveStatus != null) interaction.objectToSetActiveStatus.SetActive(options.objectActiveStatus);
    }

    private void SecondaryAttackInteraction()
    {
        GrabbableByPlayer grabbableByPlayer = selfObject.GetComponent<GrabbableByPlayer>();
        if (grabbableByPlayer != null) grabbableByPlayer.ReceivedSecondary();
    }

    private void RotateObject(SetOfInteractions.Options options)
    {
        if (interaction.rotator != null) interaction.rotator.transform.Rotate(0f, 0f, -options.rotateValue);
    }

    private void MoveObject(SetOfInteractions.Options options, Vector3 direction)
    {
        interaction.StartForcedMovement(direction, -options.movementDistance, options.movementTime);
    }

    private void SendSignalToSelf(GameObject source)
    {
        ISendSignalToSelf sendSignalToSelf = selfObject.GetComponent<ISendSignalToSelf>();
        if (sendSignalToSelf != null) sendSignalToSelf.OnSignalReceived(source);
    }
    private void DestroyOrTurnOff(bool turnsOff)
    {
        if (!turnsOff) GameObject.Destroy(selfObject);
        else selfObject.SetActive(false);
    }
    private void UXEffectExecute(SetOfInteractions.Options options)
    {
        if (!options.uxOnInteractionInitialized)
        {
            options.uxOnInteraction.UXEffectStartup();
            options.uxOnInteractionInitialized = true;
        }
        if (options.uxOnInteraction.hasSound) options.uxOnInteraction.sound.PlayAudio();
    }
}
