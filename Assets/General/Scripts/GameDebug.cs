using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDebug : MonoBehaviour
{
    private GameSaveSystem gameSave;
    private PCController pcController;
    [SerializeField] private Canvas debugUIOff;
    [SerializeField] private Canvas debugUIOn;

    private void Awake()
    {
        pcController = FindObjectOfType<PCController>();
        gameSave = FindObjectOfType<GameSaveSystem>();
    }

    private void Start()
    {
        debugUIOff.gameObject.SetActive(true);
        debugUIOn.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.I)) pcController.pcReferences.attackReceived.ignoresDamage = !pcController.pcReferences.attackReceived.ignoresDamage;
        if (Input.GetKeyDown(KeyCode.B) && pcController != null) pcController.pcReferences.heartbeat.SetHeartbeatTimer(true);
        if (Input.GetKeyDown(KeyCode.N) && pcController != null) pcController.pcReferences.heartbeat.SetHeartbeatTimer(false);
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (Input.GetKeyDown(KeyCode.L)) gameSave.LoadData();
        if (Input.GetKeyDown(KeyCode.K)) pcController.pcReferences.pcHealth.OnDeath(true);
        if (Input.GetKeyDown(KeyCode.P)) gameSave.DeleteLoadedData();
        if (Input.GetKeyDown(KeyCode.T))
        {
            debugUIOff.gameObject.SetActive(!debugUIOff.gameObject.activeInHierarchy);
            debugUIOn.gameObject.SetActive(!debugUIOn.gameObject.activeInHierarchy);
        }
    }
}
