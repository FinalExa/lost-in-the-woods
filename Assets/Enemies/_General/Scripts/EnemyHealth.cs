using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private EnemyData enemyData;

    private void Start()
    {
        SetHPStartup(enemyData.maxHP);
    }
}
