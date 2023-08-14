using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootShieldAttackWall : MonoBehaviour, INeedRefFromCreator, ISendSignalToSelf, ISendWeaponAttackType
{
    private RootShieldEnemyController rootShieldRef;
    private Interaction interaction;
    [SerializeField] private int chanceToSpawnWall;
    [SerializeField] private SpriteRenderer wallSprite;
    [SerializeField] private SpriteRenderer wallSecondSprite;
    [SerializeField] private BoxCollider colliderRef;
    [SerializeField] private int hitPoints;
    [SerializeField] private string positiveWeakness;
    [SerializeField] private Color positiveColor;
    [SerializeField] private string negativeWeakness;
    [SerializeField] private Color negativeColor;
    private string currentWeakness;
    private int currentHitPoints;
    public WeaponAttack.WeaponAttackType ReceivedWeaponAttackType { get; set; }

    private void Awake()
    {
        interaction = this.gameObject.GetComponent<Interaction>();
    }
    private void OnEnable()
    {
        wallSprite.gameObject.SetActive(false);
    }

    public void SetRef(GameObject source)
    {
        rootShieldRef = source.gameObject.GetComponent<RootShieldEnemyController>();
        this.transform.rotation = source.transform.GetChild(1).transform.rotation;
        this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x - 90, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
        AttemptToSpawnWall();
    }

    public void OnSignalReceived(GameObject source)
    {
        ReceiveAttacks();
    }

    private void AttemptToSpawnWall()
    {
        int randomValue = Random.Range(0, 100);
        if (randomValue < chanceToSpawnWall) CheckForPossiblePosition();
        else SelfDestruct();
    }

    private void ReceiveAttacks()
    {
        if (interaction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(currentWeakness) && currentWeakness != string.Empty) RemoveSelfFromList();
        if (ReceivedWeaponAttackType == WeaponAttack.WeaponAttackType.PLAYER) RemoveHitPoint();
    }

    private void RemoveHitPoint()
    {
        currentHitPoints--;
        if (currentHitPoints <= 0) RemoveSelfFromList();
    }

    private void CheckForPossiblePosition()
    {
        Collider[] colliders = Physics.OverlapBox(this.transform.position, new Vector3(colliderRef.size.x / 2, colliderRef.size.y, colliderRef.size.z / 2));
        if (colliders.Length > 0)
        {
            bool checkForBadCollisions = false;
            foreach (Collider collider in colliders)
            {
                if (!collider.gameObject.CompareTag("Ground") && !collider.gameObject.CompareTag("Player") && !collider.isTrigger)
                {
                    checkForBadCollisions = true;
                    break;
                }
            }
            if (checkForBadCollisions) SelfDestruct();
            else SetWallActive();
        }
        else SelfDestruct();
    }

    private void SetWallActive()
    {
        wallSprite.gameObject.SetActive(true);
        colliderRef.enabled = true;
        currentHitPoints = hitPoints;
        SetWeakness();
        rootShieldRef.rootShieldWallManagement.AddWall(this);
    }

    private void SetWeakness()
    {
        if (rootShieldRef.randomizeShieldTypeEverytime)
        {
            int randomNumber = Random.Range(0, 2);
            if (randomNumber == 0) SetWeaknessType(true);
            else SetWeaknessType(false);
        }
        else
        {
            if (!rootShieldRef.isFog) SetWeaknessType(true);
            else SetWeaknessType(false);
        }
    }

    private void SetWeaknessType(bool isPositive)
    {
        if (isPositive)
        {
            currentWeakness = positiveWeakness;
            wallSprite.color = positiveColor;
            wallSecondSprite.color = positiveColor;
        }
        else
        {
            currentWeakness = negativeWeakness;
            wallSprite.color = negativeColor;
            wallSecondSprite.color = negativeColor;
        }
    }

    private void RemoveSelfFromList()
    {
        rootShieldRef.rootShieldWallManagement.RemoveWall(this);
    }

    public void SelfDestruct()
    {
        GameObject.Destroy(this.gameObject);
    }
}
