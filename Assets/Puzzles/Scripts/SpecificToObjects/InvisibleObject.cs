using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleObject : MonoBehaviour, ISendSignalToSelf
{
    [SerializeField] private bool baseActiveState;
    [SerializeField] private GameObject objectToOperate;
    [SerializeField] private string lightBulbName;
    [SerializeField] private string fogPlantName;
    private bool lightBulbIn;
    private bool fogPlantIn;
    public void OnSignalReceived(GameObject source)
    {

    }
}
