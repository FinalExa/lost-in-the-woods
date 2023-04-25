using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSignalSet : MonoBehaviour, ISendSignalToSelf, ISendWeaponAttackType
{
    public int startingState;
    public int currentState;
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
        print(source.name);
        for (int i = 0; i < plantSignalStates.Length; i++)
        {
            if ((interaction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(plantSignalStates[i].stateRequiredName) || (ReceivedWeaponAttackType == plantSignalStates[i].stateRequiredAttackType)) && currentState != i)
            {
                SetPlantState(i);
            }
        }
    }

    public void SetPlantState(int stateIndex)
    {
        currentState = stateIndex;
        namedInteractionExecutor.NameAndStateChange(plantSignalStates[stateIndex].stateSignalName, plantSignalStates[stateIndex].stateActive);
        spriteRenderer.sprite = plantSignalStates[stateIndex].stateSprite;
        interaction.objectToSetActiveStatus.SetActive(false);
        interaction.objectToSetActiveStatus.SetActive(true);
    }
}
