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
    [SerializeField] private SpriteRenderer signalFeedback;
    [SerializeField] private Color signalPositiveColor;
    [SerializeField] private Color signalNegativeColor;
    [SerializeField] private SpriteRenderer explorationFeedback;
    [SerializeField] private Color explorationPositiveColor;
    [SerializeField] private Color explorationNegativeColor;

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
        SetSignalGraphics();
    }

    private void SetSignalGraphics()
    {
        if (signalState == 0) signalFeedback.gameObject.SetActive(false);
        else if (signalState == 1)
        {
            signalFeedback.gameObject.SetActive(true);
            signalFeedback.color = signalPositiveColor;
        }
        else if (signalState == -1)
        {
            signalFeedback.gameObject.SetActive(true);
            signalFeedback.color = signalNegativeColor;
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
        SetExplorationGraphics();
    }

    private void SetExplorationGraphics()
    {
        if (explorationState == 0) explorationFeedback.gameObject.SetActive(false);
        else if (explorationState == 1)
        {
            explorationFeedback.gameObject.SetActive(true);
            explorationFeedback.color = explorationPositiveColor;
        }
        else if (explorationState == -1)
        {
            explorationFeedback.gameObject.SetActive(true);
            explorationFeedback.color = explorationNegativeColor;
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

    public void ResetSeed()
    {
        signalState = 0;
        explorationState = 0;
        SetSignalGraphics();
        SetExplorationGraphics();
    }
}
