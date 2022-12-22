using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTeleports : MonoBehaviour
{
    private PCController pcController;
    [System.Serializable]
    public struct LevelTeleport
    {
        public GameObject teleportObj;
        public KeyCode associatedKey;
    }
    public LevelTeleport[] levelTeleports;

    private void Awake()
    {
        pcController = FindObjectOfType<PCController>();
    }

    private void Update()
    {
        if (pcController != null && Input.anyKeyDown) Teleport();
    }

    private void Teleport()
    {
        foreach (LevelTeleport levelTeleport in levelTeleports)
        {
            if (Input.GetKeyDown(levelTeleport.associatedKey)) pcController.transform.position = levelTeleport.teleportObj.transform.position;
        }
    }
}
