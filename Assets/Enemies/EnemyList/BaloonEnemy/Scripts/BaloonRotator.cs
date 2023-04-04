using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonRotator : EnemyRotator
{
    [HideInInspector] public bool followPlayerRotation;
    private PCRotation pcRotation;

    override protected void Awake()
    {
        base.Awake();
        pcRotation = FindObjectOfType<PCRotation>();
    }

    override public void Rotate(Vector3 target)
    {
        Vector3 direction = Vector3.zero;
        if (!followPlayerRotation) direction = (this.gameObject.transform.position - target).normalized;
        else direction = (target - this.gameObject.transform.position).normalized;
        float xValue = 1f - Mathf.Abs(direction.x);
        float zValue = 1f - Mathf.Abs(direction.z);
        if (xValue < zValue) RotateHorizontally(direction);
        else RotateVertically(direction);
    }
}
