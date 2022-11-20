using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomController : EnemyController
{
    public bool isVulnerable;

    private void OnTriggerEnter(Collider other)
    {
        if (isVulnerable && other.CompareTag("Hole"))
        {
            other.gameObject.GetComponent<AttackInteraction>().CheckIfEnemyIsTheSame(enemyData.enemyName);
            this.gameObject.SetActive(false);
        }
    }
}
