using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomCollisions : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    public bool isVulnerable;

    private void OnTriggerEnter(Collider other)
    {
        if (isVulnerable && other.CompareTag("Hole"))
        {
            other.gameObject.GetComponent<AttackInteraction>().NamedInteractionExecute(enemyData.enemyName);
            this.gameObject.SetActive(false);
        }
    }
}
