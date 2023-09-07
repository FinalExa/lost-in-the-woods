using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWarpPlant : MonoBehaviour, ISendSignalToSelf, ISendWeaponAttackType, ISaveIntValuesForSaveSystem
{
    private Color[] availableColors;
    public int currentColorIndex;
    public int currentStatus;
    [SerializeField] private Color[] statusColors;
    [SerializeField] private string positiveName;
    [SerializeField] private string negativeName;
    [SerializeField] private string warpableName;
    [SerializeField] private SpriteRenderer coreSprite;
    [SerializeField] private SpriteRenderer areaSprite;
    public GameObject outputPosition;
    private ItemWarpPlant connectedPlant;
    [SerializeField] private GameObject activateWhenWarpActive;
    public WeaponAttack.WeaponAttackType ReceivedWeaponAttackType { get; set; }
    private Interaction interaction;
    private NamedInteractionExecutor namedInteractionExecutor;
    [HideInInspector] public ItemWarpPlantMaster itemWarpPlantMaster;
    public int ValueToSave { get; set; }
    private bool skipInitialize;

    private void Awake()
    {
        interaction = this.gameObject.GetComponent<Interaction>();
        namedInteractionExecutor = this.gameObject.GetComponent<NamedInteractionExecutor>();
    }

    private void Start()
    {
        UpdateCurrentStatus(currentStatus);
    }

    public void OnSignalReceived(GameObject source)
    {
        if (interaction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(positiveName)) UpdateCurrentStatus(1);
        else if (interaction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(negativeName)) UpdateCurrentStatus(-1);
        else UpdateCurrentStatus(0);
        if (ReceivedWeaponAttackType == WeaponAttack.WeaponAttackType.PLAYER) UpdateCurrentStatus(0);
        else if (ReceivedWeaponAttackType == WeaponAttack.WeaponAttackType.PLAYER_SECONDARY) GoToNextCoreColor();
        itemWarpPlantMaster.UpdateCurrentPlantStatus();
    }

    private void UpdateCurrentStatus(int receivedValue)
    {
        currentStatus = receivedValue;
        areaSprite.color = statusColors[currentStatus + 1];
    }

    private void GoToNextCoreColor()
    {
        currentColorIndex++;
        if (currentColorIndex == availableColors.Length) currentColorIndex = 0;
        SetCoreColor();
    }

    public void InitializeColors(Color[] receivedColors)
    {
        availableColors = receivedColors;
        if (!skipInitialize)
        {
            SetCoreColor();
        }
    }

    private void SetCoreColor()
    {
        coreSprite.color = availableColors[currentColorIndex];
        ValueToSave = currentColorIndex;
    }

    public void SetWarpActive(ItemWarpPlant itemWarpPlantToConnect)
    {
        namedInteractionExecutor.thisName = warpableName;
        namedInteractionExecutor.active = true;
        activateWhenWarpActive.SetActive(true);
        connectedPlant = itemWarpPlantToConnect;
    }

    public void SetWarpInactive()
    {
        namedInteractionExecutor.thisName = string.Empty;
        namedInteractionExecutor.active = false;
        if (activateWhenWarpActive != null) activateWhenWarpActive.SetActive(false);
        connectedPlant = null;
    }

    public void Warp(GameObject objectToWarp)
    {
        if (connectedPlant != null) objectToWarp.transform.position = connectedPlant.outputPosition.transform.position;
    }

    public void SetValue()
    {
        skipInitialize = true;
        itemWarpPlantMaster = this.transform.parent.gameObject.GetComponent<ItemWarpPlantMaster>();
        itemWarpPlantMaster.SetupWarpPlants();
        currentColorIndex = ValueToSave;
        SetCoreColor();
        itemWarpPlantMaster.UpdateCurrentPlantStatus();
    }
}
