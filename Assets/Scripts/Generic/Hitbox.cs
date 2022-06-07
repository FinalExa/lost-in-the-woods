using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Controller controller;
    [SerializeField] private string damagingTag;
    [SerializeField] private string notDamagingTag;
    private bool lockDamage;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(damagingTag) && !lockDamage)
        {
            Attack atk = other.GetComponent<Attack>();
            controller.HealthAddValue(-atk.damageToDeal);
            lockDamage = true;
        }
        if (other.CompareTag(notDamagingTag) && lockDamage)
        {
            lockDamage = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((other.CompareTag(damagingTag) || other.CompareTag(notDamagingTag)) && lockDamage)
        {
            lockDamage = false;
        }
    }
}
