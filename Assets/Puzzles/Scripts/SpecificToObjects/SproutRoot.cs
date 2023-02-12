using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SproutRoot : MonoBehaviour, ISendSignalToSelf
{
    public void OnSignalReceived(GameObject source)
    {
        print("hey!");
    }
}
