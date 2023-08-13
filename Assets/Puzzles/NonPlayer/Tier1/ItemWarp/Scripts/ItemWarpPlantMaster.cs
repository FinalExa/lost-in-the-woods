using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWarpPlantMaster : MonoBehaviour
{
    [SerializeField] private ItemWarpPlant[] itemWarpPlants;
    [SerializeField] private Color[] availableColors;
    private List<ItemWarpPlant> checkingPlants;

    private void Awake()
    {
        checkingPlants = new List<ItemWarpPlant>();
    }

    private void Start()
    {
        SetupWarpPlants();
    }

    private void SetupWarpPlants()
    {
        foreach (ItemWarpPlant itemWarpPlant in itemWarpPlants)
        {
            itemWarpPlant.InitializeColors(availableColors);
        }
    }
}
