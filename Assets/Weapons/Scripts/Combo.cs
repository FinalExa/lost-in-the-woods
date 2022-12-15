using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    [HideInInspector] public Weapon currentWeapon;
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
    protected bool enteredProjectileSpawn;
    protected bool colliding;

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
        if (currentWeapon != weaponToSet)
        {
            currentWeapon = weaponToSet;
            currentWeapon.currentWeaponAttackIndex = 0;
        }
    }
    protected void ComboSetup()
    {
        comboHitOver = true;
        comboDelay = false;
        lastDirection = new Vector3(0f, 0f, 1f);
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
        attackTimer = currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].duration;
        currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].attackObject.SetActive(true);
        currentWeapon.currentDamage = currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].damage;
        if (currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].movementDistance != 0) movementSpeed = currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].movementDistance / currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].duration;
        attackCountTime = 0;
        isAttacking = true;
        if (currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].playsSound) AudioManager.Instance.PlaySound(currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].soundToPlay);
    }

    private void Attacking()
    {
        WeaponAttack currentAttack = currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex];
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
        if (currentAttack.movementDistance != 0 && !colliding)
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
            if (hitboxToCheck.weaponAttackHitboxProjectile.spawnsProjectile && attackCountTime >= hitboxToCheck.weaponAttackHitboxProjectile.projectileLaunchTimeAfterStart && attackCountTime < hitboxToCheck.deactivationDelayAfterStart && !enteredProjectileSpawn)
            {
                Projectile projectile = Instantiate(hitboxToCheck.weaponAttackHitboxProjectile.projectile, hitboxToCheck.attackRef.transform.position, hitboxToCheck.attackRef.transform.rotation);
                projectile.direction = lastDirection;
                enteredProjectileSpawn = true;
            }
            if (attackCountTime >= hitboxToCheck.deactivationDelayAfterStart)
            {
                hitboxToCheck.attackRef.gameObject.SetActive(false);
                enteredProjectileSpawn = false;
            }
            count++;
        }
    }

    private void CountToCancelCombo()
    {
        if (comboCancelTimer > 0) comboCancelTimer -= Time.deltaTime;
        else
        {
            currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].attackObject.SetActive(false);
            currentWeapon.currentWeaponAttackIndex = 0;
            currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].attackObject.SetActive(true);
            comboCancelDelay = false;
        }
    }

    private void EndComboHit()
    {
        currentWeapon.hitTargets.Clear();
        if (currentWeapon.currentWeaponAttackIndex + 1 == currentWeapon.weaponAttacks.Count)
        {
            currentWeapon.currentWeaponAttackIndex = 0;
            comboDelayTimer = currentWeapon.comboEndDelay;
            comboEndDelay = true;
        }
        else
        {
            comboDelayTimer = currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].afterDelay;
            currentWeapon.currentWeaponAttackIndex++;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) colliding = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) colliding = false;
    }
}
