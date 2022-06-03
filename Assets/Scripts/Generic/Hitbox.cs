using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Controller controller;
    [SerializeField] private string damagingTag;
    [SerializeField] private string notDamagingTag;
    private bool lockDamage;
    private Collider lastHit;

    private void Update()
    {
        TrackOther();
    }

    private void TrackOther()
    {
        if (lockDamage)
        {
            if (lastHit.CompareTag(notDamagingTag)) lockDamage = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(damagingTag) && !lockDamage)
        {
            Attack atk = other.GetComponent<Attack>();
            controller.HealthAddValue(-atk.damageToDeal);
            print(controller.actualHealth);
            lockDamage = true;
            lastHit = other;
        }
    }
}
