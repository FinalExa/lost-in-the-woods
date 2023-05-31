using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrifyingPlant : MonoBehaviour, ISendSignalToSelf
{
    private Interaction interaction;
    private LightPayCombo lightPayCombo;
    [SerializeField] private string rejuvenateName;
    [SerializeField] private Weapon rejuvenateWeaponRef;
    private Weapon rejuvenateWeapon;
    [SerializeField] private string destroyName;
    [SerializeField] private Weapon destroyWeaponRef;
    private Weapon destroyWeapon;
    [SerializeField] private GameObject weaponParent;
    [SerializeField] private GameObject directionObj;

    private void Awake()
    {
        interaction = this.gameObject.GetComponent<Interaction>();
        lightPayCombo = this.gameObject.GetComponent<LightPayCombo>();
    }

    private void Start()
    {
        CreateWeapons();
    }

    private void CreateWeapons()
    {
        rejuvenateWeapon = Instantiate(rejuvenateWeaponRef, weaponParent.transform);
        destroyWeapon = Instantiate(destroyWeaponRef, weaponParent.transform);
    }

    public void OnSignalReceived(GameObject source)
    {
        SetPetrifyingPlantStatus(interaction.namedInteractionOperations);
    }

    private void Update()
    {
        SetDirectionToWeapon();
    }

    private void SetPetrifyingPlantStatus(NamedInteractionOperations namedOps)
    {
        if (namedOps.ActiveNamedInteractions.ContainsKey(rejuvenateName) && !namedOps.ActiveNamedInteractions.ContainsKey(destroyName)) lightPayCombo.currentWeapon = rejuvenateWeapon;
        else if (!namedOps.ActiveNamedInteractions.ContainsKey(rejuvenateName) && namedOps.ActiveNamedInteractions.ContainsKey(destroyName)) lightPayCombo.currentWeapon = destroyWeapon;
        else lightPayCombo.currentWeapon = null;
    }

    private void SetDirectionToWeapon()
    {
        lightPayCombo.LastDirection = directionObj.transform.position - this.transform.position;
    }
}
