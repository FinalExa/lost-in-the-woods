using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack
{
    public bool IsAttacking { get; private set; }
    private Combo combo;
    private WeaponAttack currentAttack;
    private int attackCountdownFrame;
    private int attackCountFrame;
    private float movementSpeed;
    private Vector3 attackDirection;

    public ComboAttack(Combo comboRef)
    {
        combo = comboRef;
    }

    public void StartAttack(WeaponAttack attackRef, Vector3 receivedDirection)
    {
        if (!IsAttacking && attackRef != null)
        {
            currentAttack = attackRef;
            attackCountdownFrame = currentAttack.frameDuration;
            attackCountFrame = 0;
            movementSpeed = currentAttack.movementDistance / currentAttack.frameDuration;
            attackDirection = receivedDirection;
            currentAttack.attackObject.SetActive(true);
            if (currentAttack.uxOnWeaponAttack.hasSound) currentAttack.uxOnWeaponAttack.sound.PlayAudio();
            IsAttacking = true;
        }
    }

    public void Attacking()
    {
        if (IsAttacking)
        {
            if (attackCountdownFrame > 0) DuringAttack();
            else OnAttackEnd();
        }
    }

    private void DuringAttack()
    {
        attackCountdownFrame--;
        attackCountFrame++;
        AttackMovement();
        CheckActivatingHitboxes();
        if (currentAttack.weaponSpawnsObjectDuringThisAttack.Length > 0 && combo != null) combo.comboObjectSpawner.CheckObjectsToSpawn(currentAttack, attackCountFrame, attackDirection);
    }

    private void OnAttackEnd()
    {
        CheckActivatingHitboxes();
        if (currentAttack.weaponSpawnsObjectDuringThisAttack.Length > 0) combo.comboObjectSpawner.ResetObjectsToSpawn(currentAttack);
        IsAttacking = false;
        if (currentAttack.attackObject != null) currentAttack.attackObject.SetActive(false);
        EndComboHit();
    }

    private void AttackMovement()
    {
        if (currentAttack.movementDistance != 0 && combo!=null) combo.transform.position += (attackDirection * movementSpeed);
    }
    private void CheckActivatingHitboxes()
    {
        int count = 0;
        foreach (WeaponAttack.WeaponAttackHitboxSequence hitboxToCheck in currentAttack.weaponAttackHitboxSequence)
        {
            if (hitboxToCheck.attackRef != null)
            {
                if (attackCountFrame >= hitboxToCheck.activationFrame && attackCountFrame < hitboxToCheck.deactivationFrame) hitboxToCheck.attackRef.gameObject.SetActive(true);
                if (attackCountFrame >= hitboxToCheck.deactivationFrame) hitboxToCheck.attackRef.gameObject.SetActive(false);

            }
            count++;
        }
    }

    private void EndComboHit()
    {
        combo.currentWeapon.hitTargets.Clear();
        if (combo.currentWeapon.currentWeaponAttackIndex + 1 == combo.currentWeapon.weaponAttacks.Count)
        {
            combo.currentWeapon.currentWeaponAttackIndex = 0;
            combo.comboDelays.SetComboEnd();
        }
        else
        {
            combo.currentWeapon.currentWeaponAttackIndex++;
            combo.comboDelays.SetDelayForNextAttack();
        }
        combo.comboDelays.SetVariablesAfterAttack();
    }

    public void EndCombo()
    {
        if (IsAttacking)
        {
            attackCountFrame = currentAttack.frameDuration;
            foreach (WeaponAttack.WeaponAttackHitboxSequence weaponAttackHitbox in currentAttack.weaponAttackHitboxSequence) weaponAttackHitbox.attackRef.gameObject.SetActive(false);
            IsAttacking = false;
            currentAttack.attackObject.SetActive(false);
            combo.currentWeapon.currentWeaponAttackIndex = combo.currentWeapon.weaponAttacks.Count - 1;
            EndComboHit();
        }
    }
}
