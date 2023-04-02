using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotator : MonoBehaviour
{
    [HideInInspector] public bool invertRotationLook;
    [SerializeField] private GameObject rotator;
    private EnemyController enemyController;

    private void Awake()
    {
        enemyController = this.gameObject.GetComponent<EnemyController>();
    }

    private void Update()
    {
        ContinouslyRotate();
    }

    private void ContinouslyRotate()
    {
        if (!enemyController.enemyCombo.isInCombo) Rotate(enemyController.playerTarget.transform.position);
    }

    public void Rotate(Vector3 target)
    {
        Vector3 direction = Vector3.zero;
        if (!invertRotationLook) direction = (this.gameObject.transform.position - target).normalized;
        else direction = (target - this.gameObject.transform.position).normalized;
        float xValue = 1f - Mathf.Abs(direction.x);
        float zValue = 1f - Mathf.Abs(direction.z);
        if (xValue < zValue) RotateHorizontally(direction);
        else RotateVertically(direction);
    }

    private void RotateHorizontally(Vector3 direction)
    {
        if (direction.x < 0 && rotator.transform.eulerAngles != enemyController.rotation.left) rotator.transform.eulerAngles = enemyController.rotation.left;
        else if (direction.x > 0 && rotator.transform.eulerAngles != enemyController.rotation.right) rotator.transform.eulerAngles = enemyController.rotation.right;
    }

    private void RotateVertically(Vector3 direction)
    {
        if (direction.z < 0 && rotator.transform.eulerAngles != enemyController.rotation.forward) rotator.transform.eulerAngles = enemyController.rotation.forward;
        else if (direction.z > 0 && rotator.transform.eulerAngles != enemyController.rotation.back) rotator.transform.eulerAngles = enemyController.rotation.back;
    }
}
