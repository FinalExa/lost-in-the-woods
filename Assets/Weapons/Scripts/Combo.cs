using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    [HideInInspector] public Weapon currentWeapon;
    [HideInInspector] public int currentComboProgress;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool comboHitOver;
    [HideInInspector] public bool comboDelay;
    [HideInInspector] public bool comboEndDelay;
    [HideInInspector] public float attackTimer;
    [HideInInspector] public float attackCountTime;
    [HideInInspector] public float comboDelayTimer;
    [HideInInspector] public float comboCancelTimer;
    [HideInInspector] public Vector3 lastDirection { get; set; }
    protected bool comboCancelDelay;
    protected float movementSpeed;

    protected virtual void Start()
    {
        ComboSetup();
    }
    public virtual void Update()
    {
        if (isAttacking) Attacking();
        if (comboDelay) ComboDelay();
        if (comboCancelDelay) CountToCancelCombo();
    }

    public void SetWeapon(Weapon weaponToSet)
    {
        if (currentWeapon != weaponToSet) currentWeapon = weaponToSet;
    }
    protected void ComboSetup()
    {
        comboHitOver = true;
        comboDelay = false;
        lastDirection = new Vector3(0f, 0f, 1f);
        currentComboProgress = 0;
    }
    public void StartComboHitCheck()
    {
        if (!comboDelay) StartComboHit();
    }

    protected void ComboDelay()
    {
        if (comboDelayTimer > 0) comboDelayTimer -= Time.deltaTime;
        else
        {
            comboDelay = false;
            if (comboEndDelay)
            {
                comboEndDelay = false;
                OnComboEnd();
            }
        }
    }

    protected void StartComboHit()
    {
        comboHitOver = false;
        comboCancelDelay = false;
        attackTimer = currentWeapon.weaponAttacks[currentComboProgress].duration;
        currentWeapon.weaponAttacks[currentComboProgress].attackObject.SetActive(true);
        currentWeapon.currentDamage = currentWeapon.weaponAttacks[currentComboProgress].damage;
        if (currentWeapon.weaponAttacks[currentComboProgress].movementDistance != 0) movementSpeed = currentWeapon.weaponAttacks[currentComboProgress].movementDistance / currentWeapon.weaponAttacks[currentComboProgress].duration;
        attackCountTime = 0;
        isAttacking = true;
    }

    private void Attacking()
    {
        WeaponAttack currentAttack = currentWeapon.weaponAttacks[currentComboProgress];
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
            attackCountTime += Time.deltaTime;
            AttackMovement(currentAttack);
            CheckActivatingHitboxes(currentAttack);
        }
        else
        {
            CheckActivatingHitboxes(currentAttack);
            isAttacking = false;
            currentAttack.attackObject.SetActive(false);
            EndComboHit();
        }
    }
    private void AttackMovement(WeaponAttack currentAttack)
    {
        if (currentAttack.movementDistance != 0)
        {
            float relativeSpeed = movementSpeed * Time.deltaTime;
            this.transform.position += (lastDirection * relativeSpeed);
        }
    }
    private void CheckActivatingHitboxes(WeaponAttack currentAttack)
    {
        int count = 0;
        foreach (WeaponAttack.WeaponAttackHitboxSequence hitboxToCheck in currentAttack.weaponAttackHitboxSequence)
        {
            if (attackCountTime >= hitboxToCheck.activationDelayAfterStart && attackCountTime < hitboxToCheck.deactivationDelayAfterStart) hitboxToCheck.attackRef.gameObject.SetActive(true);
            if (attackCountTime >= hitboxToCheck.deactivationDelayAfterStart)
            {
                hitboxToCheck.attackRef.gameObject.SetActive(false);
                currentAttack.ProjectileSetSpawnedStatus(false, count);
            }
            if (hitboxToCheck.spawnsProjectile && attackCountTime >= hitboxToCheck.projectileLaunchTimeAfterStart && !hitboxToCheck.spawnedProjectile)
            {
                hitboxToCheck.projectile.direction = lastDirection.normalized;
                hitboxToCheck.projectile.gameObject.SetActive(true);
                currentAttack.ProjectileSetSpawnedStatus(true, count);
            }
            count++;
        }
    }

    private void CountToCancelCombo()
    {
        if (comboCancelTimer > 0) comboCancelTimer -= Time.deltaTime;
        else
        {
            currentWeapon.weaponAttacks[currentComboProgress].attackObject.SetActive(false);
            currentComboProgress = 0;
            currentWeapon.weaponAttacks[currentComboProgress].attackObject.SetActive(true);
            comboCancelDelay = false;
        }
    }

    private void EndComboHit()
    {
        currentWeapon.hitTargets.Clear();
        if (currentComboProgress + 1 == currentWeapon.weaponAttacks.Count)
        {
            currentComboProgress = 0;
            comboDelayTimer = currentWeapon.comboEndDelay;
            comboEndDelay = true;
        }
        else
        {
            comboDelayTimer = currentWeapon.weaponAttacks[currentComboProgress].afterDelay;
            currentComboProgress++;
            comboCancelTimer = currentWeapon.comboCancelTime;
            comboCancelDelay = true;
        }
        comboDelay = true;
        comboHitOver = true;
    }
    protected virtual void OnComboEnd()
    {
        return;
    }
}
