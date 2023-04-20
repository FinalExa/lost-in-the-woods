using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDebug : MonoBehaviour
{
    private PCReferences pcReferences;

    private void Awake()
    {
        pcReferences = FindObjectOfType<PCReferences>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.I)) pcReferences.attackReceived.ignoresDamage = !pcReferences.attackReceived.ignoresDamage;
        if (Input.GetKeyDown(KeyCode.B)) pcReferences.heartbeat.SetHeartbeatTimer(true);
        if (Input.GetKeyDown(KeyCode.N)) pcReferences.heartbeat.SetHeartbeatTimer(false);
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
