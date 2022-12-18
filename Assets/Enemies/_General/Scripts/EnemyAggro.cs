using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    private EnemyController enemyController;
    private SphereCollider aggroCollider;
    private bool playerInAggroCollider;
    private Transform playerTransform;
    [SerializeField] private LayerMask aggroLayer;
    private int playerLayer;
    [SerializeField] private UXEffect uxOnAggro;

    private void Awake()
    {
        enemyController = this.gameObject.GetComponentInParent<EnemyController>();
        aggroCollider = this.gameObject.GetComponent<SphereCollider>();
    }

    private void Start()
    {
        SetupAggro();
        uxOnAggro.UXEffectStartup();
    }

    private void Update()
    {
        if (!enemyController.isAlerted && !aggroCollider.enabled) aggroCollider.enabled = true;
        if (!enemyController.isAlerted && playerInAggroCollider) CheckIfPlayerIsSeen();
        if (enemyController.isAlerted) CountDistanceFromPlayer();
    }

    private void SetupAggro()
    {
        aggroCollider.radius = enemyController.enemyData.firstAggroDistance;
        playerLayer = LayerMask.GetMask("Player");
    }

    public void PlayerAggroInteraction(bool status, Transform pcTransform)
    {
        playerInAggroCollider = status;
        playerTransform = pcTransform;
    }

    private void CheckIfPlayerIsSeen()
    {
        RaycastHit hit;
        Vector3 direction = playerTransform.position - enemyController.transform.position;
        if (Physics.Raycast(enemyController.transform.position, direction, out hit, Mathf.Infinity, aggroLayer) && hit.collider.CompareTag("Player"))
        {
            enemyController.isAlerted = true;
            playerInAggroCollider = false;
            aggroCollider.enabled = false;
            PlayAggroSound();
        }
    }

    private void PlayAggroSound()
    {
        if (uxOnAggro.hasSound) uxOnAggro.sound.PlayAudio();
    }

    private void CountDistanceFromPlayer()
    {
        RaycastHit hit;
        Vector3 direction = playerTransform.position - enemyController.transform.position;
        if (Physics.Raycast(enemyController.transform.position, direction, out hit, Mathf.Infinity, playerLayer) && hit.distance > enemyController.enemyData.stopAggroDistance) enemyController.isAlerted = false;
    }
}
