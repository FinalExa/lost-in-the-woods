using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboFrameMaster : MonoBehaviour
{
    [SerializeField] protected int framesPerSecond;
    [HideInInspector] public float frameValueTime;
    protected float countTimer;
    public static Action frameIsBeingExecuted;

    private void Start()
    {
        FrameSetup();
    }

    private void Update()
    {
        ComboFrameTimer();
    }

    private void FrameSetup()
    {
        frameValueTime = 1f / (float)framesPerSecond;
        frameIsBeingExecuted();
        countTimer = 0f;
    }

    private void ComboFrameTimer()
    {
        if (countTimer < frameValueTime) countTimer += Time.deltaTime;
        else LaunchFrame();
    }

    private void LaunchFrame()
    {
        countTimer -= frameValueTime;
        frameIsBeingExecuted();
        CheckForRelaunch();
    }
    private void CheckForRelaunch()
    {
        if (countTimer >= frameValueTime) LaunchFrame();
    }
}
