using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestorativeObject : MonoBehaviour, ISendSignalToSelf, ISendWeaponAttackType
{
    [HideInInspector] public WeaponAttack.WeaponAttackType ReceivedWeaponAttackType { get; set; }
    [SerializeField] private GameObject restoredObject;
    [SerializeField] private GameObject destroyedObject;
    [SerializeField] private bool startsDestroyed;

    private void Start()
    {
        SetStartingActiveStates();
    }

    private void SetStartingActiveStates()
    {
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
            restoredObject.SetActive(false);
            destroyedObject.SetActive(true);
        }
        else if (ReceivedWeaponAttackType == WeaponAttack.WeaponAttackType.REJUVENATE_PTF)
        {
            restoredObject.SetActive(true);
            destroyedObject.SetActive(false);
        }
    }
}
