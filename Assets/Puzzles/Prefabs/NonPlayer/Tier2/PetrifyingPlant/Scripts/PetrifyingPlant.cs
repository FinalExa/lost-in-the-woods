using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetrifyingPlant : MonoBehaviour, ISendSignalToSelf
{
    private Interaction interaction;
    private LightPayCombo lightPayCombo;
    [SerializeField] private string rejuvenateName;
    [SerializeField] private Weapon rejuvenateWeapon;
    [SerializeField] private string destroyName;
    [SerializeField] private Weapon destroyWeapon;

    private void Awake()
    {
        interaction = this.gameObject.GetComponent<Interaction>();
        lightPayCombo = this.gameObject.GetComponent<LightPayCombo>();
    }

    public void OnSignalReceived(GameObject source)
    {
        SetPetrifyingPlantStatus(interaction.namedInteractionOperations);
    }

    private void SetPetrifyingPlantStatus(NamedInteractionOperations namedOps)
    {
        if (namedOps.ActiveNamedInteractions.ContainsKey(rejuvenateName) && !namedOps.ActiveNamedInteractions.ContainsKey(destroyName)) lightPayCombo.ChangeWeapon(rejuvenateWeapon);
        else if (!namedOps.ActiveNamedInteractions.ContainsKey(rejuvenateName) && namedOps.ActiveNamedInteractions.ContainsKey(destroyName)) lightPayCombo.ChangeWeapon(destroyWeapon);
        else lightPayCombo.ChangeWeapon(null);
    }
}
