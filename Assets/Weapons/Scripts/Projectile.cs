using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileLifetime;
    private float projectileTimer;
    private Rigidbody projectileRb;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public List<AttackReceivedData.GameTargets> possibleTargets;
    private Transform originalParent;

    private void Awake()
    {
        projectileRb = this.gameObject.GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        originalParent = this.transform.parent;
        this.transform.parent = null;
        projectileTimer = projectileLifetime;
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
        projectileRb.velocity = Vector3.zero;
        this.transform.parent = originalParent;
        this.transform.localPosition = Vector3.zero;
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        AttackReceived attackReceived = other.gameObject.GetComponent<AttackReceived>();
        if (attackReceived != null)
        {
            attackReceived.AttackReceivedOperation(possibleTargets, -projectileDamage);
            EndProjectile();
        }
    }
}
