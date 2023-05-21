using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedRetractableObject : MonoBehaviour, ISendSignalToSelf
{
    [SerializeField] private string retractName;
    [SerializeField] private float retractTime;
    [SerializeField] private GameObject objectToRetract;
    private float retractTimer;
    private Interaction interaction;
    private bool retracted;
    private bool timerActive;

    private void Awake()
    {
        interaction = this.gameObject.GetComponent<Interaction>();
    }

    private void Update()
    {
        RetractTimer();
    }

    public void OnSignalReceived(GameObject source)
    {
        SetRetractState();
    }

    private void SetRetractState()
    {
        if (interaction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(retractName))
        {
            if (objectToRetract.activeSelf) objectToRetract.SetActive(false);
            retracted = true;
            timerActive = false;
        }
        else if (retracted) SetupTimer();
    }

    private void SetupTimer()
    {
        retractTimer = retractTime;
        timerActive = true;
    }

    private void RetractTimer()
    {
        if (timerActive)
        {
            if (retractTimer > 0) retractTimer -= Time.deltaTime;
            else
            {
                objectToRetract.SetActive(true);
                retracted = false;
                timerActive = false;
            }
        }
    }
}
