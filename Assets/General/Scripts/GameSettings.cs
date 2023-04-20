using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private int framerate;
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = framerate;
    }
}
