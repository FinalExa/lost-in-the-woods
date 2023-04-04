using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepSpriteRotation : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    private Quaternion startRotation;
    private void Start()
    {
        if (targetObject != null) startRotation = targetObject.transform.rotation;
    }

    private void Update()
    {
        ResetRotation();
    }

    private void ResetRotation()
    {
        if (targetObject != null && targetObject.transform.rotation != startRotation) targetObject.transform.rotation = startRotation;
    }
}
