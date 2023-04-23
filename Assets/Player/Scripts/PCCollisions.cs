using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAggroRange")) other.GetComponent<EnemyAggro>().PlayerAggroInteraction(true, this.transform);
        if (other.CompareTag("DestroyWhenPlayerEnters")) GameObject.Destroy(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyAggroRange")) other.GetComponent<EnemyAggro>().PlayerAggroInteraction(false, this.transform);
    }
}
