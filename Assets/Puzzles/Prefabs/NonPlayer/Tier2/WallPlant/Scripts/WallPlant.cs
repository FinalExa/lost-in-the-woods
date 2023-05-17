using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlant : MonoBehaviour, ISendSignalToSelf
{
    private Interaction interaction;
    [SerializeField] private string wallDeactivateName;
    [SerializeField] private GameObject wallContainer;
    [SerializeField] private string infertileAreaDeactivateName;
    [SerializeField] private GameObject infertileAreaContainer;
    private void Awake()
    {
        interaction = this.gameObject.GetComponent<Interaction>();
    }


    public void OnSignalReceived(GameObject source)
    {
        SetCurrentPlantState(interaction.namedInteractionOperations);
    }

    private void SetCurrentPlantState(NamedInteractionOperations namedOps)
    {
        if (namedOps.ActiveNamedInteractions.ContainsKey(wallDeactivateName) && !namedOps.ActiveNamedInteractions.ContainsKey(infertileAreaDeactivateName))
        {
            wallContainer.SetActive(false);
            infertileAreaContainer.SetActive(true);
        }
        else if (!namedOps.ActiveNamedInteractions.ContainsKey(wallDeactivateName) && namedOps.ActiveNamedInteractions.ContainsKey(infertileAreaDeactivateName))
        {
            wallContainer.SetActive(true);
            infertileAreaContainer.SetActive(false);
        }
        else
        {
            wallContainer.SetActive(true);
            infertileAreaContainer.SetActive(true);
        }
    }
}
