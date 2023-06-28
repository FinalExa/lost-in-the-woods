using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject positionObj;
    [SerializeField] private Teleport linkedTeleport;
    private PCController pcController;

    private void Awake()
    {
        pcController = FindObjectOfType<PCController>();
    }

    public void TeleportPlayerHere()
    {
        if (positionObj != null) pcController.transform.position = positionObj.transform.position;
    }

    private void StartTeleport()
    {
        if (linkedTeleport != null && linkedTeleport != this && pcController != null) linkedTeleport.TeleportPlayerHere();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) StartTeleport();
    }
}
