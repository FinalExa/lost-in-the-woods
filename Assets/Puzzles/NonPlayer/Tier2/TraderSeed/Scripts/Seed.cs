using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour, ISendSignalToSelf
{
    [HideInInspector] public int signalState;
    [HideInInspector] public int explorationState;
    [SerializeField] private NamedInteractionExecutor signalNamedInteractionExecutor;
    [SerializeField] private string signalPositiveName;
    [SerializeField] private string signalNegativeName;
    [SerializeField] private NamedInteractionExecutor explorationNamedInteractionExecutor;
    [SerializeField] private string explorationPositiveName;
    [SerializeField] private string explorationNegativeName;
    private Rigidbody thisRb;
    private RigidbodyConstraints savedConstraints;

    private void Awake()
    {
        thisRb = this.gameObject.GetComponent<Rigidbody>();
        savedConstraints = thisRb.constraints;
    }

    public void OnSignalReceived(GameObject source)
    {
        SetSeedStatus(source.gameObject.GetComponent<TraderPlant>());
    }

    private void SetSeedStatus(TraderPlant traderPlant)
    {
        SetSignal(traderPlant);
        SetExploration(traderPlant);
    }

    private void SetSignal(TraderPlant traderPlant)
    {
        signalState = traderPlant.signalSetState;
        if (signalState == 0)
        {
            signalNamedInteractionExecutor.thisName = string.Empty;
            signalNamedInteractionExecutor.active = false;
        }
        else if (signalState == 1)
        {
            signalNamedInteractionExecutor.thisName = signalPositiveName;
            signalNamedInteractionExecutor.active = true;
        }
        else if (signalState == -1)
        {
            signalNamedInteractionExecutor.thisName = signalNegativeName;
            signalNamedInteractionExecutor.active = true;
        }
    }

    private void SetExploration(TraderPlant traderPlant)
    {
        explorationState = traderPlant.explorationSetState;
        if (explorationState == 0)
        {
            explorationNamedInteractionExecutor.thisName = string.Empty;
            explorationNamedInteractionExecutor.active = false;
        }
        else if (explorationState == 1)
        {
            explorationNamedInteractionExecutor.thisName = explorationPositiveName;
            explorationNamedInteractionExecutor.active = true;
        }
        else if (explorationState == -1)
        {
            explorationNamedInteractionExecutor.thisName = explorationNegativeName;
            explorationNamedInteractionExecutor.active = true;
        }
    }

    public void LockRb()
    {
        thisRb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void UnlockRb()
    {
        thisRb.constraints = savedConstraints;
    }
}
