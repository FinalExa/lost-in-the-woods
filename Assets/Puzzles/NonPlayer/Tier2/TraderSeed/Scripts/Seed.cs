using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour, ISendSignalToSelf, ISaveIntValuesForSaveSystem
{
    [HideInInspector] public int signalState;
    [HideInInspector] public int explorationState;
    public int spawnerId;
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
    public int ValueToSave { get; set; }
    private bool skipInitialization;
    private Zone zoneRef;

    private void Awake()
    {
        zoneRef = this.gameObject.GetComponentInParent<Zone>();
        thisRb = this.gameObject.GetComponent<Rigidbody>();
        savedConstraints = thisRb.constraints;
    }
    private void Start()
    {
        Startup();
    }

    private void Startup()
    {
        if (!skipInitialization)
        {
            SetSignal(1);
            SetExploration(1);
            ValueToSave = (spawnerId * 100) + (signalState * 10) + explorationState;
        }
    }

    public void OnSignalReceived(GameObject source)
    {
        SetSeedStatus(source.gameObject.GetComponent<TraderPlant>());
    }

    private void SetSeedStatus(TraderPlant traderPlant)
    {
        SetSignal(traderPlant.signalSetState);
        SetExploration(traderPlant.explorationSetState);
        ValueToSave = (spawnerId * 100) + (signalState * 10) + explorationState;
    }

    private void SetSignal(int receivedSignalState)
    {
        signalState = receivedSignalState;
        if (signalState == 1)
        {
            signalNamedInteractionExecutor.thisName = string.Empty;
            signalNamedInteractionExecutor.active = false;
        }
        else if (signalState == 2)
        {
            signalNamedInteractionExecutor.thisName = signalPositiveName;
            signalNamedInteractionExecutor.active = true;
        }
        else if (signalState == 0)
        {
            signalNamedInteractionExecutor.thisName = signalNegativeName;
            signalNamedInteractionExecutor.active = true;
        }
        SetSignalGraphics();
    }

    private void SetSignalGraphics()
    {
        if (signalState == 1) signalFeedback.gameObject.SetActive(false);
        else if (signalState == 2)
        {
            signalFeedback.gameObject.SetActive(true);
            signalFeedback.color = signalPositiveColor;
        }
        else if (signalState == 0)
        {
            signalFeedback.gameObject.SetActive(true);
            signalFeedback.color = signalNegativeColor;
        }
    }

    private void SetExploration(int receivedExplorationState)
    {
        explorationState = receivedExplorationState;
        if (explorationState == 1)
        {
            explorationNamedInteractionExecutor.thisName = string.Empty;
            explorationNamedInteractionExecutor.active = false;
        }
        else if (explorationState == 2)
        {
            explorationNamedInteractionExecutor.thisName = explorationPositiveName;
            explorationNamedInteractionExecutor.active = true;
        }
        else if (explorationState == 0)
        {
            explorationNamedInteractionExecutor.thisName = explorationNegativeName;
            explorationNamedInteractionExecutor.active = true;
        }
        SetExplorationGraphics();
    }

    private void SetExplorationGraphics()
    {
        if (explorationState == 1) explorationFeedback.gameObject.SetActive(false);
        else if (explorationState == 2)
        {
            explorationFeedback.gameObject.SetActive(true);
            explorationFeedback.color = explorationPositiveColor;
        }
        else if (explorationState == 0)
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
        signalState = 1;
        explorationState = 1;
        SetSignalGraphics();
        SetExplorationGraphics();
    }

    public void SetValue()
    {
        skipInitialization = true;
        spawnerId = ValueToSave / 100;
        int bothStates = (ValueToSave - ((ValueToSave / 100) * 100));
        SetSignal(bothStates / 10);
        SetExploration(bothStates - ((bothStates / 10) * 10));
        SeedSpawner[] seedSpawners = zoneRef.GetComponentsInChildren<SeedSpawner>();
        foreach (SeedSpawner seedSpawner in seedSpawners)
        {
            if (seedSpawner.thisSpawnerId == spawnerId) seedSpawner.ReplaceSeed(this);
        }
        SeedPillar pillar = this.transform.parent.parent.gameObject.GetComponent<SeedPillar>();
        if (pillar != null) pillar.SetSeed(this);
    }
}
