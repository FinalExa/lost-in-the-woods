using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileLifetime;
    public List<AttackReceived.GameTargets> possibleTargets;
    public List<WeaponAttack.WeaponAttackType> attackTypes;
    private float projectileTimer;
    private Rigidbody projectileRb;
    [HideInInspector] public Vector3 direction;
    [SerializeField] private bool playsSound;
    [SerializeField] private string soundToPlay;

    private void Awake()
    {
        projectileRb = this.gameObject.GetComponent<Rigidbody>();
    }
    private void Start()
    {
        projectileTimer = projectileLifetime;
        if (playsSound) AudioManager.Instance.PlaySound(soundToPlay);
    }

    private void FixedUpdate()
    {
        ProjectileMovement();
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
            attackReceived.AttackReceivedOperation(possibleTargets, projectileDamage, attackTypes, invulnerable);
            EndProjectile();
        }
    }
}
