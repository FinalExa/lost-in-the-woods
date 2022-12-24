using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootEnemyController : EnemyController
{
    [HideInInspector] public bool rootUnderground;
    [SerializeField] private SpriteRenderer spriteRef;
    [SerializeField] private Sprite rootUndergroundSprite;
    [SerializeField] private Sprite rootOutsideSprite;
    [SerializeField] private float rootOutsideTime;
    private float rootOutsideTimer;

    private void OnEnable()
    {
        SetRootUnderground();
    }

    private void Update()
    {
        if (!rootUnderground) RootOutsideTime();
    }

    private void SetRootUnderground()
    {
        rootUnderground = true;
        spriteRef.sprite = rootUndergroundSprite;
        attackReceived.SetInvincibility(true);
    }

    public void SetRootOutside()
    {
        rootUnderground = false;
        spriteRef.sprite = rootOutsideSprite;
        rootOutsideTimer = rootOutsideTime;
        attackReceived.SetInvincibility(false);
    }

    private void RootOutsideTime()
    {
        if (rootOutsideTimer > 0 && affectedByLight.lightState != AffectedByLight.LightState.BERSERK) rootOutsideTimer -= Time.deltaTime;
        else SetRootUnderground();
    }
}
