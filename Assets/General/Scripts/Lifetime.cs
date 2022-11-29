using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifetime : MonoBehaviour
{
    private bool isReady;
    private float timer;


    private void Update()
    {
        if (isReady) LifetimeTimer();
    }

    public void SetTimer(float timerValue)
    {
        timer = timerValue;
        isReady = true;
    }

    private void LifetimeTimer()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else GameObject.Destroy(this.gameObject);
    }
}
