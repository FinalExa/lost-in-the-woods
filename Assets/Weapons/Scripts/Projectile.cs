using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IHaveSettableDirection
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileLifetime;
    public List<AttackReceived.GameTargets> possibleTargets;
    public List<WeaponAttack.WeaponAttackType> attackTypes;
    private float projectileTimer;
    private Rigidbody projectileRb;
    [HideInInspector] public Vector3 direction;
    [SerializeField] private UXEffect uxProjectile;

    private void Awake()
    {
        projectileRb = this.gameObject.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        projectileTimer = projectileLifetime;
        uxProjectile.UXEffectStartup();
        if (uxProjectile.hasSound) uxProjectile.sound.PlayAudio();
    }

    private void FixedUpdate()
    {
        ProjectileMovement();
    }

    public void SetDirection(Vector3 receivedDirection)
    {
        direction = receivedDirection;
    }
    private void ProjectileMovement()
    {
        if (projectileTimer > 0)
        {
            projectileTimer -= Time.fixedDeltaTime;
            projectileRb.velocity = direction * projectileSpeed;
        }
        else EndProjectile();
    }

    private void EndProjectile()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        bool invulnerable = false;
        if (other.CompareTag("Invulnerable")) invulnerable = true;
        AttackReceived attackReceived = other.gameObject.GetComponent<AttackReceived>();
        if (attackReceived != null)
        {
            attackReceived.AttackReceivedOperation(possibleTargets, projectileDamage, attackTypes, invulnerable, this.gameObject);
            EndProjectile();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground")) EndProjectile();
    }
}
