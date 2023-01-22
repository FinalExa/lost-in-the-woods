using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentHoleTerrain : MonoBehaviour
{
    [SerializeField] private int maxPermanentHolesInThisTerrain = 1;
    private List<GameObject> permanentHoles;

    private void Start()
    {
        permanentHoles = new List<GameObject>();
    }

    public void AddPermanentHole(GameObject holeToAdd)
    {
        if (permanentHoles.Count >= maxPermanentHolesInThisTerrain)
        {
            GameObject reference = permanentHoles[0];
            permanentHoles.RemoveAt(0);
            GameObject.Destroy(reference);
        }
        permanentHoles.Add(holeToAdd);
    }


}
