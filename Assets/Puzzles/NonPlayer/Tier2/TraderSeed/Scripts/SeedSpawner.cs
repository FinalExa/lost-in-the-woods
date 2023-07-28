using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawner : MonoBehaviour, ISendSignalToSelf
{
    [SerializeField] private Seed seedRef;
    [SerializeField] private GameObject seedPosition;
    private Seed seedInstance;

    private void Start()
    {
        SpawnSeed();
    }

    public void OnSignalReceived(GameObject source)
    {
        SpawnSeed();
    }

    private void SpawnSeed()
    {
        if (seedInstance == null) InstantiateSeed();
        else ResetSeed();
    }

    private void InstantiateSeed()
    {
        seedInstance = Instantiate(seedRef, seedPosition.transform.position, Quaternion.identity);
    }

    private void ResetSeed()
    {
        seedInstance.transform.position = seedPosition.transform.position;
        seedInstance.ResetSeed();
    }
}
