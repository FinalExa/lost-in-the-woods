using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootShieldEnemyController : EnemyController
{
    [HideInInspector] public bool shieldUp;

    private void OnEnable()
    {
        shieldUp = true;
    }
}
