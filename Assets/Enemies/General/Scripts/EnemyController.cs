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
    public float lightUpDistanceTolerance;
    [HideInInspector] public GameObject playerTarget;
    [HideInInspector] public EnemyRotator enemyRotator;
    [HideInInspector] public EnemyCombo enemyCombo;
    [HideInInspector] public NavMeshAgent thisNavMeshAgent;
    int lightLayerMask;
    private void Awake()
    {
        playerTarget = FindObjectOfType<PCController>().gameObject;
        enemyRotator = this.gameObject.GetComponent<EnemyRotator>();
        enemyCombo = this.gameObject.GetComponent<EnemyCombo>();
        thisNavMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        lightLayerMask = LayerMask.GetMask("PlayerLight");
        isAlerted = true;
        enemyWeapon.damageTag = whoToDamage;
        enemyCombo.SetWeapon(enemyWeapon);
    }
    private void Update()
    {
        CheckForLight();
    }

    private void CheckForLight()
    {
        Collider[] lightSearch = Physics.OverlapBox(this.transform.position, transform.localScale, transform.rotation, lightLayerMask);
        if (lightSearch.Length > 0) isInsideLight = true;
        else isInsideLight = false;
    }
}
