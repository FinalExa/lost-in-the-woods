using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveLightFeedback : MonoBehaviour
{
    public AdaptiveLightActivator currentRef;
    public List<string> currentAdaptiveLight;
    public static Action adaptiveLightCheck;

    private void Start()
    {
        currentAdaptiveLight = new List<string>();
    }

    public void ChangeAdaptiveLightList(AdaptiveLightActivator refToSet, List<string> receivedList)
    {
        currentRef = refToSet;
        SetList(receivedList);
        if (adaptiveLightCheck != null) adaptiveLightCheck();
    }

    private void SetList(List<string> receivedList)
    {
        foreach (string nameToInsert in receivedList)
        {
            currentAdaptiveLight.Add(nameToInsert);
        }
    }

    public void ClearAdaptiveLightList()
    {
        currentRef = null;
        currentAdaptiveLight.Clear();
        if (adaptiveLightCheck != null) adaptiveLightCheck();
    }

    public bool QueryCurrentAdaptiveLight(string receivedName)
    {
        return currentAdaptiveLight.Contains(receivedName);
    }
}
