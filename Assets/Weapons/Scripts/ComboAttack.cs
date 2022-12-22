using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack
{
    public bool isAttacking;
    private Combo combo;
    private Weapon currentWeapon;
    private WeaponAttack currentAttack;
    private float attackTimer;
    private float attackCountTime;
    private float movementSpeed;
    private Vector3 attackDirection;

    public ComboAttack(Combo comboRef)
    {
        combo = comboRef;
    }

    public void StartAttack(Weapon weaponRef, WeaponAttack attackRef, Vector3 receivedDirection)
    {
        if (!isAttacking)
        {
            if (currentWeapon != weaponRef) currentWeapon = weaponRef;
            currentAttack = attackRef;
            attackTimer = currentAttack.duration;
            attackCountTime = 0;
            movementSpeed = currentAttack.movementDistance / currentAttack.duration;
            attackDirection = receivedDirection;
            currentAttack.attackObject.SetActive(true);
            if (currentAttack.uxOnWeaponAttack.hasSound) currentAttack.uxOnWeaponAttack.sound.PlayAudio();
            isAttacking = true;
        }
    }

    public void Attacking()
    {
        if (isAttacking)
        {
            if (attackTimer > 0) DuringAttack();
            else OnAttackEnd();
        }
    }

    private void DuringAttack()
    {
        attackTimer -= Time.deltaTime;
        attackCountTime += Time.deltaTime;
        AttackMovement();
        CheckActivatingHitboxes();
        if (currentAttack.weaponSpawnsObjectDuringThisAttack.Length > 0) combo.comboObjectSpawner.CheckObjectsToSpawn(currentAttack, attackCountTime, attackDirection);
    }

    private void OnAttackEnd()
    {
        CheckActivatingHitboxes();
        if (currentAttack.weaponSpawnsObjectDuringThisAttack.Length > 0) combo.comboObjectSpawner.ResetObjectsToSpawn(currentAttack);
        isAttacking = false;
        currentAttack.attackObject.SetActive(false);
        combo.EndComboHit();
    }

    private void AttackMovement()
    {
        if (currentAttack.movementDistance != 0)
        {
            float relativeSpeed = movementSpeed * Time.deltaTime;
            combo.transform.position += (attackDirection * relativeSpeed);
        }
    }
    private void CheckActivatingHitboxes()
    {
        int count = 0;
        foreach (WeaponAttack.WeaponAttackHitboxSequence hitboxToCheck in currentAttack.weaponAttackHitboxSequence)
        {
            if (attackCountTime >= hitboxToCheck.activationDelayAfterStart && attackCountTime < hitboxToCheck.deactivationDelayAfterStart) hitboxToCheck.attackRef.gameObject.SetActive(true);
            if (attackCountTime >= hitboxToCheck.deactivationDelayAfterStart) hitboxToCheck.attackRef.gameObject.SetActive(false);
            count++;
        }
    }

    public void EndCombo()
    {
        attackTimer = currentAttack.duration;
        foreach (WeaponAttack.WeaponAttackHitboxSequence weaponAttackHitbox in currentAttack.weaponAttackHitboxSequence) weaponAttackHitbox.attackRef.gameObject.SetActive(false);
        isAttacking = false;
        currentAttack.attackObject.SetActive(false);
        currentWeapon.currentWeaponAttackIndex = currentWeapon.weaponAttacks.Count - 1;
        combo.EndComboHit();
    }
}
