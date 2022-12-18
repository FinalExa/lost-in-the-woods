using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInteractionOptions
{
    public GameObject selfObject;
    public AttackInteraction attackInteraction;
    public void Interact(AttackInteraction.Options options, GameObject source, bool turnsOff)
    {
        if (!options.hasSpecialCondition || SpecialConditionsCheck(options))
        {
            UXEffectExecute(options);
            if (options.isDestroyed) DestroyOrTurnOff(turnsOff);
            else if (options.isTransformed && options.transformedRef != null) Transform(options, turnsOff);
            else if (options.sendsSignalToSelf) SendSignalToSelf(source);
            else if (options.isMoved) MoveObject(options, source.transform.position - selfObject.transform.position);
            else if (options.canSetObjectActiveStatus) SetObjectActiveStatus(options);
            else if (options.rotates && options.objectToRotate != null) RotateObject(options);
        }
    }
    public void Interact(AttackInteraction.Options options, GameObject source, bool turnsOff, float lifeTime)
    {
        if (!options.hasSpecialCondition || SpecialConditionsCheck(options))
        {
            if (options.isDestroyed) DestroyOrTurnOff(turnsOff);
            else if (options.isTransformed && options.transformedRef != null) Transform(options, turnsOff, lifeTime);
            else if (options.sendsSignalToSelf) SendSignalToSelf(source);
            else if (options.isMoved) MoveObject(options, source.transform.position - selfObject.transform.position);
            else if (options.canSetObjectActiveStatus) SetObjectActiveStatus(options);
            else if (options.rotates && options.objectToRotate != null) RotateObject(options);
        }
    }

    private bool SpecialConditionsCheck(AttackInteraction.Options options)
    {
        bool check = false;
        if (options.hasSpecialCondition)
        {
            IHaveSpecialConditions haveSpecialConditions = attackInteraction.GetComponent<IHaveSpecialConditions>();
            if (haveSpecialConditions != null) check = haveSpecialConditions.SpecialConditions();
        }
        return check;
    }

    private GameObject Transform(AttackInteraction.Options options, bool turnsOff)
    {
        GameObject objectRef = attackInteraction.InstantiateNew(options);
        DestroyOrTurnOff(turnsOff);
        return objectRef;
    }
    private GameObject Transform(AttackInteraction.Options options, bool turnsOff, float lifeTime)
    {
        GameObject objectRef = attackInteraction.InstantiateNew(options);
        Lifetime lifetimeRef = objectRef.GetComponent<Lifetime>();
        if (lifetimeRef != null) lifetimeRef.SetTimer(lifeTime);
        DestroyOrTurnOff(turnsOff);
        return objectRef;
    }
    private void SetObjectActiveStatus(AttackInteraction.Options options)
    {
        if (options.objectToSetActiveStatus != null) options.objectToSetActiveStatus.SetActive(options.objectActiveStatus);
    }

    private void RotateObject(AttackInteraction.Options options)
    {
        options.objectToRotate.transform.Rotate(0f, 0f, -options.rotateValue);
    }

    private void MoveObject(AttackInteraction.Options options, Vector3 direction)
    {
        attackInteraction.StartForcedMovement(direction, -options.movementDistance, options.movementTime);
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
    private void UXEffectExecute(AttackInteraction.Options options)
    {
        if (!options.uxOnInteractionInitialized)
        {
            options.uxOnInteraction.UXEffectStartup();
            options.uxOnInteractionInitialized = true;
        }
        if (options.uxOnInteraction.hasSound) options.uxOnInteraction.sound.PlayAudio();
    }
}
