using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [HideInInspector] public float damageToDeal;
    private bool lockDamage;

    private void OnEnable()
    {
        lockDamage = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !lockDamage)
        {
            PCController controller = other.GetComponent<PCController>();
            controller.HealthAddValue(-damageToDeal);
            lockDamage = true;
        }
    }
}
