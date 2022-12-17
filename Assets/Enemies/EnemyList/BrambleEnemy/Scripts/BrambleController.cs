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
    private Vector3 currentColliderSize;
    private float retractTimer;
    [SerializeField] private UXEffect uxOnFullyRetracted;

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
        currentColliderSize = expandedBoxColliderSize;
    }

    private void Update()
    {
        if (isRetracted) RetractTimer();
        else if (currentColliderSize != expandedBoxColliderSize) RetractionUpdate(brambleData.unretractedGrowthRatePerSecond * Time.deltaTime);
    }
    public void OnSignalReceived(GameObject source)
    {
        if (!isRetracted) RetractionUpdate(-brambleData.onHitRetractReduction);
    }

    private void RetractSet(float timer)
    {
        if (!isRetracted)
        {
            spriteColliderObject.SetActive(false);
            retractTimer = timer;
            isRetracted = true;
            if (uxOnFullyRetracted.hasSound) uxOnFullyRetracted.sound.PlayAudio();
        }
    }

    private void RetractTimer()
    {
        if (retractTimer > 0) retractTimer -= Time.deltaTime;
        else
        {
            isRetracted = false;
            spriteColliderObject.SetActive(true);
        }
    }

    public void RetractionUpdate(float valueToAdd)
    {
        Vector3 vectorToClamp = currentColliderSize + new Vector3(valueToAdd, valueToAdd, valueToAdd);
        vectorToClamp.x = Mathf.Clamp(vectorToClamp.x, startBoxColliderSize.x, expandedBoxColliderSize.x);
        vectorToClamp.y = Mathf.Clamp(vectorToClamp.y, startBoxColliderSize.y, expandedBoxColliderSize.y);
        vectorToClamp.z = Mathf.Clamp(vectorToClamp.z, startBoxColliderSize.z, expandedBoxColliderSize.z);
        currentColliderSize = vectorToClamp;
        boxCollider.size = currentColliderSize;
        spriteColliderObject.transform.localScale = currentColliderSize;
        if (currentColliderSize == startBoxColliderSize) RetractSet(brambleData.onHitRetractTime);
    }
}
