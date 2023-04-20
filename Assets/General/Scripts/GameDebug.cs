using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDebug : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    private GameSave gameSave;
    private PCReferences pcReferences;

    private void Awake()
    {
        pcReferences = FindObjectOfType<PCReferences>();
        gameSave = this.gameObject.GetComponent<GameSave>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.I)) pcReferences.attackReceived.ignoresDamage = !pcReferences.attackReceived.ignoresDamage;
        if (Input.GetKeyDown(KeyCode.B) && pcReferences != null) pcReferences.heartbeat.SetHeartbeatTimer(true);
        if (Input.GetKeyDown(KeyCode.N) && pcReferences != null) pcReferences.heartbeat.SetHeartbeatTimer(false);
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (Input.GetKeyDown(KeyCode.K)) gameSave.SaveData();
        if (Input.GetKeyDown(KeyCode.L)) gameSave.LoadData();
    }
}
