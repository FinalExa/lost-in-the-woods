using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private string whoToDamage;
    [SerializeField] private Weapon enemyWeapon;
    public Rotation rotation;
    public bool isAlerted;
    public bool isInsideLight;
    public float defaultSpeed;
    public float lightUpSpeed;
    public float attackDistance;
    public float lightUpDistance;
    [HideInInspector] public GameObject playerTarget;
    [HideInInspector] public EnemyRotator enemyRotator;
    [HideInInspector] public EnemyCombo enemyCombo;
    [HideInInspector] public NavMeshAgent thisNavMeshAgent;
    private void Awake()
    {
        playerTarget = FindObjectOfType<PCController>().gameObject;
        enemyRotator = this.gameObject.GetComponent<EnemyRotator>();
        enemyCombo = this.gameObject.GetComponent<EnemyCombo>();
        thisNavMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        isAlerted = true;
        enemyWeapon.damageTag = whoToDamage;
        enemyCombo.SetWeapon(enemyWeapon);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerLight")) isInsideLight = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerLight")) isInsideLight = false;
    }
}
