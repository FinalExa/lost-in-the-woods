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
    private bool isInPermanentHoleTerrain;
    private GameObject currentHoleToSpawn;
    [SerializeField] private GameObject permanentHoleRef;
    [SerializeField] private GameObject temporaryHoleRef;

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
    }

    public void SetRootOutside()
    {
        rootUnderground = false;
        spriteRef.sprite = rootOutsideSprite;
        rootOutsideTimer = rootOutsideTime;
    }

    private void SetPermanentHoleOff()
    {
        isInPermanentHoleTerrain = false;
        currentHoleToSpawn = temporaryHoleRef;
    }

    private void SetPermanentHoleOn()
    {
        isInPermanentHoleTerrain = true;
        currentHoleToSpawn = permanentHoleRef;
    }

    private void RootOutsideTime()
    {
        if (rootOutsideTimer > 0 && affectedByLight.lightState != AffectedByLight.LightState.BERSERK) rootOutsideTimer -= Time.deltaTime;
        else SetRootUnderground();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PermanentHoleTerrain")) SetPermanentHoleOn();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PermanentHoleTerrain")) SetPermanentHoleOff();
    }
}
