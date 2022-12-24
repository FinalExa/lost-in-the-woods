using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject temporaryHoleRef;
    [SerializeField] private GameObject permanentHoleRef;
    [SerializeField] private string permanentHoleTerrainTag;

    private void Start()
    {
        CheckForTerrain();
    }

    private void CheckForTerrain()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, 0.1f);
        bool check = false;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag(permanentHoleTerrainTag))
            {
                SpawnHole(permanentHoleRef);
                check = true;
                break;
            }
        }
        if (!check) SpawnHole(temporaryHoleRef);
    }

    private void SpawnHole(GameObject holeToSpawn)
    {
        Instantiate(holeToSpawn, this.transform.position, Quaternion.identity);
        GameObject.Destroy(this.gameObject);
    }
}
