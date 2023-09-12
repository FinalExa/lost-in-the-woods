using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWarpPlantMaster : MonoBehaviour
{
    private ItemWarpPlant[] itemWarpPlants;
    [SerializeField] private Color[] availableColors;
    private int[] colorCount;


    private void Start()
    {
        SetupWarpPlants();
        UpdateCurrentPlantStatus();
    }

    public void SetupWarpPlants()
    {
        itemWarpPlants = this.gameObject.GetComponentsInChildren<ItemWarpPlant>();
        foreach (ItemWarpPlant itemWarpPlant in itemWarpPlants)
        {
            itemWarpPlant.InitializeColors(availableColors);
            itemWarpPlant.itemWarpPlantMaster = this;
        }
        colorCount = new int[availableColors.Length];
    }

    public void UpdateCurrentPlantStatus()
    {
        CountPlantColors();
        CheckForRightCount();
    }

    private void CountPlantColors()
    {
        for (int i = 0; i < colorCount.Length; i++)
        {
            colorCount[i] = 0;
        }
        for (int i = 0; i < itemWarpPlants.Length; i++)
        {
            colorCount[itemWarpPlants[i].currentColorIndex]++;
        }
    }

    private void CheckForRightCount()
    {
        for (int i = 0; i < colorCount.Length; i++)
        {
            if (colorCount[i] == 2)
            {
                ItemWarpPlant firstPlant = null;
                foreach (ItemWarpPlant itemWarpPlant in itemWarpPlants)
                {
                    if (itemWarpPlant.currentColorIndex == i)
                    {
                        if (firstPlant == null) firstPlant = itemWarpPlant;
                        else ComparePlantStatus(firstPlant, itemWarpPlant);
                    }
                }
            }
            else DeactivateWarpOnPlantsOfACertainIndex(i);
        }
    }

    private void ComparePlantStatus(ItemWarpPlant firstPlant, ItemWarpPlant secondPlant)
    {
        if (firstPlant.currentStatus != 0 && firstPlant.currentStatus == -secondPlant.currentStatus)
        {
            firstPlant.SetWarpActive(secondPlant);
            secondPlant.SetWarpActive(firstPlant);
        }
        else
        {
            firstPlant.SetWarpInactive();
            secondPlant.SetWarpInactive();
        }
    }

    private void DeactivateWarpOnPlantsOfACertainIndex(int index)
    {
        foreach (ItemWarpPlant itemWarpPlant in itemWarpPlants)
        {
            if (itemWarpPlant.currentColorIndex == index)
            {
                itemWarpPlant.SetWarpInactive();
            }
        }
    }
}
