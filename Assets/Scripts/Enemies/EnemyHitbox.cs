using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private BasherController basherController;
    private PCReferences pcReferences;
    private bool lockDamage;
    private void Awake()
    {
        basherController = this.gameObject.GetComponent<BasherController>();
        pcReferences = FindObjectOfType<PCReferences>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(pcReferences.pcCombo.damagingTag) && !lockDamage)
        {
            basherController.TakeDamage(pcReferences.pcData.comboDamage);
            lockDamage = true;
        }
        if (other.CompareTag(pcReferences.pcCombo.notDamagingTag) && lockDamage)
        {
            lockDamage = false;
        }
    }
}
