using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrambleController : EnemyController, ISendSignalToSelf
{
    [HideInInspector] public bool isRetracted;
    [HideInInspector] public BrambleData brambleData;
    private BoxCollider boxCollider;
    [SerializeField] GameObject spriteColliderObject;
    private Vector3 startBoxColliderSize;
    private Vector3 expandedBoxColliderSize;
    private float retractTimer;

    protected override void Awake()
    {
        base.Awake();
        boxCollider = this.gameObject.GetComponent<BoxCollider>();
    }
    private void Start()
    {
        brambleData = (BrambleData)enemyData;
        startBoxColliderSize = boxCollider.size;
        expandedBoxColliderSize = Vector3.one * brambleData.unretractedScaleSize;
        spriteColliderObject.SetActive(true);
        spriteColliderObject.transform.localScale = Vector3.one * brambleData.unretractedScaleSize;
        boxCollider.size = expandedBoxColliderSize;
    }

    private void Update()
    {
        if (isRetracted) RetractTimer();
    }
    public void OnSignalReceived()
    {
        RetractSet(brambleData.onHitRetractTime);
    }

    public void RetractSet(float timer)
    {
        if (!isRetracted)
        {
            spriteColliderObject.SetActive(false);
            boxCollider.size = startBoxColliderSize;
            retractTimer = timer;
            isRetracted = true;
        }
    }

    private void RetractTimer()
    {
        if (retractTimer > 0) retractTimer -= Time.deltaTime;
        else
        {
            isRetracted = false;
            boxCollider.size = expandedBoxColliderSize;
            spriteColliderObject.SetActive(true);
        }
    }
}
