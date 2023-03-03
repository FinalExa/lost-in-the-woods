using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject temporaryHoleRef;
    [SerializeField] private GameObject permanentHoleRef;
    [SerializeField] private string permanentHoleTerrainTag;
    [SerializeField] private string baseTerrainName;
    [SerializeField] private string deadTerrainName;
    [SerializeField] private BoxCollider thisCollider;

    private void Start()
    {
        CheckForTerrain();
    }

    private void CheckForTerrain()
    {
        Collider[] colliders = Physics.OverlapBox(this.transform.position, thisCollider.transform.localScale);
        bool checkForPermanent = false;
        bool checkForSuitableTerrain = false;
        foreach (Collider collider in colliders)
        {
            NamedInteractionExecutor namedInteractionExecutor = collider.gameObject.GetComponent<NamedInteractionExecutor>();
            if (namedInteractionExecutor != null)
            {
                if (namedInteractionExecutor.thisName == deadTerrainName)
                {
                    checkForSuitableTerrain = false;
                    break;
                }
                else if (namedInteractionExecutor.thisName == baseTerrainName) checkForSuitableTerrain = true;
            }
        }
        if (!checkForSuitableTerrain)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag(permanentHoleTerrainTag))
            {
                SpawnHole(permanentHoleRef, collider.gameObject.GetComponent<PermanentHoleTerrain>());
                checkForPermanent = true;
                break;
            }
        }
        if (!checkForPermanent) SpawnHole(temporaryHoleRef);
    }


    private void SpawnHole(GameObject holeToSpawn)
    {
        Instantiate(holeToSpawn, this.transform.position, Quaternion.identity);
        GameObject.Destroy(this.gameObject);
    }
    private void SpawnHole(GameObject holeToSpawn, PermanentHoleTerrain permanentHoleTerrain)
    {
        GameObject hole = Instantiate(holeToSpawn, this.transform.position, Quaternion.identity);
        permanentHoleTerrain.AddPermanentHole(hole);
        GameObject.Destroy(this.gameObject);
    }
}
