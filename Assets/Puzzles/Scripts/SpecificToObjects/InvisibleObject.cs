using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleObject : MonoBehaviour, ISendSignalToSelf
{
    [SerializeField] private bool baseActiveState;
    [SerializeField] private GameObject objectToOperate;
    [SerializeField] private string lightBulbName;
    [SerializeField] private string fogPlantName;
    [SerializeField] private Interaction thisInteraction;
    private bool lightBulbIn;
    private bool fogPlantIn;

    private void Start()
    {
        objectToOperate.SetActive(baseActiveState);
        SetSelfPosition();
    }

    private void SetSelfPosition()
    {
        GameObject objectToOperateChild = objectToOperate.transform.GetChild(0).gameObject;
        if (objectToOperateChild != null)
        {
            this.gameObject.transform.position = objectToOperateChild.transform.position;
            objectToOperateChild.transform.localPosition = Vector3.zero;
            this.gameObject.transform.localScale = objectToOperateChild.transform.localScale;
            objectToOperateChild.transform.localScale = Vector3.one;
        }
    }

    public void OnSignalReceived(GameObject source)
    {
        CheckPlantStatus();
    }

    private void CheckPlantStatus()
    {
        if (thisInteraction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(lightBulbName)) lightBulbIn = true;
        else lightBulbIn = false;
        if (thisInteraction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(fogPlantName)) fogPlantIn = true;
        else fogPlantIn = false;
        SetInvisibilityStatus();
    }

    private void SetInvisibilityStatus()
    {
        if (lightBulbIn && !fogPlantIn) objectToOperate.SetActive(true);
        else if (!lightBulbIn && fogPlantIn) objectToOperate.SetActive(false);
        else objectToOperate.SetActive(baseActiveState);
    }
}
