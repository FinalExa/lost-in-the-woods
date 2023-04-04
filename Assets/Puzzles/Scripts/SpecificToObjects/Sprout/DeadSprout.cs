using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadSprout : MonoBehaviour, ISendSignalToSelf
{
    public SproutRoot[] sproutRoots;
    [SerializeField] private string puritynAuraSproutName;
    [SerializeField] private Sprout sproutRef;
    private Interaction thisInteraction;
    private void Awake()
    {
        thisInteraction = this.gameObject.GetComponent<Interaction>();
    }

    private void Start()
    {
        DeactivateSproutRoots();
    }

    private void DeactivateSproutRoots()
    {
        foreach (SproutRoot root in sproutRoots)
            root.gameObject.SetActive(false);
    }

    public void OnSignalReceived(GameObject source)
    {
        if (thisInteraction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(puritynAuraSproutName)) CreateSprout();
    }

    private void CreateSprout()
    {
        Sprout sprout = Instantiate(sproutRef, this.transform.position, Quaternion.identity, this.transform.parent);
        sprout.sproutRoots = sproutRoots;
        GameObject.Destroy(this.gameObject);
    }
}
