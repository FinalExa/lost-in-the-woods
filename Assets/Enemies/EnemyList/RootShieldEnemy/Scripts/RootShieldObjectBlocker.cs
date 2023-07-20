using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootShieldObjectBlocker : MonoBehaviour
{
    private Interaction objectToBlock;

    public void SetupObjectToBlock(Interaction receivedObj)
    {
        objectToBlock = receivedObj;
        this.transform.position = objectToBlock.transform.position;
    }

    private void Update()
    {
        if (!objectToBlock.locked) objectToBlock.locked = true;
    }

    public void SelfDestruct()
    {
        objectToBlock.locked = false;
        GameObject.Destroy(this.gameObject);
    }
}
