using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    private Zone thisZone;
    private GameSaveSystem gameSave;
    private float timer;
    private bool timerActive;

    private void Awake()
    {
        thisZone = this.transform.GetComponentInParent<Zone>();
        gameSave = FindObjectOfType<GameSaveSystem>();
    }
    private void OnEnable()
    {
        SetupCancellationPrevention();

    }
    private void Update()
    {
        CancellationPreventionTimer();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) Save();
    }
    private void SetupCancellationPrevention()
    {
        timer = 0.5f;
        timerActive = true;
    }
    private void CancellationPreventionTimer()
    {
        if (timerActive)
        {
            if (timer > 0) timer -= Time.deltaTime;
            else
            {
                timerActive = false;
                Save();
            }
        }
    }
    public void Save()
    {
        if (thisZone != null && !thisZone.zonePuzzle.puzzleActive) gameSave.SaveData(this.transform.position);
    }
}
