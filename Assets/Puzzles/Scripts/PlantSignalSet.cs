using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSignalSet : MonoBehaviour, ISendSignalToSelf, ISendWeaponAttackType, ISaveIntValuesForSaveSystem
{
    public int startingState;
    [SerializeField] private bool interactAfterSet;
    [HideInInspector] public int currentState;
    [System.Serializable]
    public struct PlantSignalState
    {
        public string stateRequiredName;
        public WeaponAttack.WeaponAttackType stateRequiredAttackType;
        public string stateSignalName;
        public bool stateActive;
        public Sprite stateSprite;
    }
    [SerializeField] private PlantSignalState[] plantSignalStates;
    [SerializeField] private NamedInteractionExecutor namedInteractionExecutor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Interaction interaction;
    public WeaponAttack.WeaponAttackType ReceivedWeaponAttackType { get; set; }
    public int ValueToSave { get; set; }

    private void Awake()
    {
        interaction = this.gameObject.GetComponent<Interaction>();
    }

    private void Start()
    {
        startingState = Mathf.Clamp(startingState, 0, plantSignalStates.Length - 1);
        SetPlantState(startingState);
    }

    public void OnSignalReceived(GameObject source)
    {
        for (int i = 0; i < plantSignalStates.Length; i++)
        {
            if ((interaction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(plantSignalStates[i].stateRequiredName) || (ReceivedWeaponAttackType == plantSignalStates[i].stateRequiredAttackType)) && currentState != i) SetPlantState(i);
        }
    }

    public void SetPlantState(int stateIndex)
    {
        currentState = stateIndex;
        namedInteractionExecutor.NameAndStateChange(plantSignalStates[stateIndex].stateSignalName, plantSignalStates[stateIndex].stateActive);
        if (plantSignalStates[stateIndex].stateSprite != null && spriteRenderer != null) spriteRenderer.sprite = plantSignalStates[stateIndex].stateSprite;
        interaction.objectToSetActiveStatus.SetActive(false);
        interaction.objectToSetActiveStatus.SetActive(true);
        if (interactAfterSet) interaction.ExecuteCallByCodeInteraction();
    }
}
