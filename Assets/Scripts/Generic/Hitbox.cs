using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Controller controller;
    [SerializeField] private string damagingTag;
    [SerializeField] private string notDamagingTag;
    [HideInInspector] public float damageToDeal;
    private bool lockDamage;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(damagingTag) && !lockDamage)
        {
            controller.HealthAddValue(-damageToDeal);
            lockDamage = true;
        }
        if (other.CompareTag(notDamagingTag) && lockDamage)
        {
            lockDamage = false;
        }
    }
}
