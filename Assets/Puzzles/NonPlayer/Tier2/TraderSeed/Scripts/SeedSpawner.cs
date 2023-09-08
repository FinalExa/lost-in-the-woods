using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawner : MonoBehaviour, ISendSignalToSelf
{
    [SerializeField] private Seed seedRef;
    [SerializeField] private GameObject seedPosition;
    [HideInInspector] public int thisSpawnerId;
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
        seedInstance = Instantiate(seedRef, seedPosition.transform.position, Quaternion.identity, this.transform);
        seedInstance.spawnerId = thisSpawnerId;
    }

    private void ResetSeed()
    {
        seedInstance.transform.position = seedPosition.transform.position;
        seedInstance.ResetSeed();
    }

    public void AssignID(int assignedID)
    {
        thisSpawnerId = assignedID;
    }

    public void ReplaceSeed(Seed newInstance)
    {
        Seed oldInstance = seedInstance;
        seedInstance = newInstance;
        GameObject.Destroy(oldInstance.gameObject);
    }
}
