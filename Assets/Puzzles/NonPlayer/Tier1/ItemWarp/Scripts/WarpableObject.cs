using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpableObject : MonoBehaviour, ISendSignalToSelf
{
    [SerializeField] private GameObject objectToWarp;
    public void OnSignalReceived(GameObject source)
    {
        Warp(source);
    }

    private void Warp(GameObject source)
    {
        ItemWarpPlant itemWarpPlant = source.GetComponent<ItemWarpPlant>();
        objectToWarp.transform.parent = this.transform.parent;
        itemWarpPlant.Warp(objectToWarp);
    }
}
