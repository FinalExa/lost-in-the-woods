using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGroundPositionHere : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) other.gameObject.GetComponent<PCFall>().SetLastGround(this.transform.position);
    }
}
