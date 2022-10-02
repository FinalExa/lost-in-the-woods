using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [HideInInspector] public float damageToDeal;
    [HideInInspector] public string whoToDamage;
    private bool lockDamage;

    private void OnEnable()
    {
        lockDamage = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(whoToDamage) && !lockDamage)
        {
            Controller controller = other.GetComponent<Controller>();
            controller.HealthAddValue(-damageToDeal);
            lockDamage = true;
        }
    }
}
