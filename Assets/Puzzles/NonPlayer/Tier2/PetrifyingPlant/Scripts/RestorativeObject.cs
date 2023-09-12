using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestorativeObject : MonoBehaviour, ISendSignalToSelf, ISendWeaponAttackType, ISaveIntValuesForSaveSystem
{
    [HideInInspector] public WeaponAttack.WeaponAttackType ReceivedWeaponAttackType { get; set; }
    [SerializeField] private GameObject restoredObject;
    [SerializeField] private GameObject destroyedObject;
    [SerializeField] private bool startsDestroyed;
    public int ValueToSave { get; set; }
    private bool skipInitialize;

    private void Start()
    {
        if (!skipInitialize) SetStartingActiveStates();
    }

    private void SetStartingActiveStates()
    {
        if (startsDestroyed) ValueToSave = 0;
        else ValueToSave = 1;
        restoredObject.SetActive(!startsDestroyed);
        destroyedObject.SetActive(startsDestroyed);
    }

    public void OnSignalReceived(GameObject source)
    {
        RestoreStatus();
    }

    private void RestoreStatus()
    {
        if (ReceivedWeaponAttackType == WeaponAttack.WeaponAttackType.DESTROY_PTF)
        {
            ValueToSave = 0;
            restoredObject.SetActive(false);
            destroyedObject.SetActive(true);
        }
        else if (ReceivedWeaponAttackType == WeaponAttack.WeaponAttackType.REJUVENATE_PTF)
        {
            ValueToSave = 1;
            restoredObject.SetActive(true);
            destroyedObject.SetActive(false);
        }
    }

    private void RestoreStatus(int numericalState)
    {
        if (numericalState == 0)
        {
            restoredObject.SetActive(false);
            destroyedObject.SetActive(true);
        }
        else if (numericalState == 1)
        {
            restoredObject.SetActive(true);
            destroyedObject.SetActive(false);
        }
    }

    public void SetValue()
    {
        skipInitialize = true;
        RestoreStatus(ValueToSave);
    }
}
