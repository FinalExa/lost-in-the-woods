using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSignalSet : MonoBehaviour, ISendSignalToSelf
{
    [SerializeField] private int startingState;
    [System.Serializable]
    public struct PlantSignalState
    {
        public string stateRequiredName;
        public string stateSignalName;
        public bool stateNameActive;
        public Sprite stateSprite;
    }
    [SerializeField] private PlantSignalState[] plantSignalStates;
    [SerializeField] private NamedInteractionExecutor namedInteractionExecutor;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Interaction interaction;

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
            if (interaction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(plantSignalStates[i].stateRequiredName))
            {
                SetPlantState(i);
            }
        }
    }

    private void SetPlantState(int stateIndex)
    {
        namedInteractionExecutor.active = plantSignalStates[stateIndex].stateNameActive;
        namedInteractionExecutor.thisName = plantSignalStates[stateIndex].stateSignalName;
        spriteRenderer.sprite = plantSignalStates[stateIndex].stateSprite;
    }
}
