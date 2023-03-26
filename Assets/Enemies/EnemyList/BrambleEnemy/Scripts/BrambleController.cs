using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrambleController : EnemyController, ISendSignalToSelf
{
    [HideInInspector] public bool isRetracted;
    [HideInInspector] public BrambleData brambleData;
    [SerializeField] private GameObject brambleBallRef;
    [SerializeField] private float brambleBallStartSpeed;
    [SerializeField] GameObject spriteColliderObject;
    private SphereCollider sphereCollider;
    private float startRadius;
    private float expandedRadius;
    private float currentRadius;
    private float retractTimer;
    [SerializeField] private UXEffect uxOnFullyRetracted;

    protected override void Awake()
    {
        base.Awake();
        sphereCollider = this.gameObject.GetComponent<SphereCollider>();
    }
    private void Start()
    {
        brambleData = (BrambleData)enemyData;
        startRadius = sphereCollider.radius;
        expandedRadius = 1 * brambleData.unretractedScaleSize;
        spriteColliderObject.SetActive(true);
        spriteColliderObject.transform.localScale = Vector3.one * brambleData.unretractedScaleSize;
        currentRadius = expandedRadius;
        sphereCollider.radius = currentRadius / 2f;
    }

    private void Update()
    {
        if (isRetracted) RetractTimer();
        else if (currentRadius != expandedRadius) RetractionUpdate(brambleData.unretractedGrowthRatePerSecond * Time.deltaTime);
    }
    public void OnSignalReceived(GameObject source)
    {
        if (!isRetracted && affectedByLight.lightState != AffectedByLight.LightState.BERSERK)
        {
            RetractionUpdate(-brambleData.onHitRetractReduction);
            Vector3 direction = source.transform.position - this.gameObject.transform.position;
            direction = new Vector3(direction.x, 0f, direction.z);
            ShootBrambleBall(direction);
            if (isRetracted) enemyCombo.EndCombo();
        }
    }

    public override void LightStateUpdate()
    {
        base.LightStateUpdate();
        if (affectedByLight.lightState == AffectedByLight.LightState.BERSERK) RetractOff();
    }

    public void OnLightReceived(float unretractedScaleSize)
    {
        RetractionUpdate(unretractedScaleSize);
        if (isRetracted) enemyCombo.EndCombo();
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
        else RetractOff();
    }

    private void RetractOff()
    {
        isRetracted = false;
        spriteColliderObject.SetActive(true);
    }

    private void RetractionUpdate(float valueToAdd)
    {
        float newRadius = Mathf.Clamp(currentRadius + valueToAdd, startRadius, expandedRadius);
        currentRadius = newRadius;
        sphereCollider.radius = currentRadius / 2f;
        spriteColliderObject.transform.localScale = Vector3.one * currentRadius;
        if (currentRadius == startRadius) RetractSet(brambleData.onHitRetractTime);
    }

    private void ShootBrambleBall(Vector3 direction)
    {
        GameObject brambleBall = Instantiate(brambleBallRef, this.transform.position, this.transform.rotation);
        brambleBall.transform.position = this.transform.position + Vector3.Scale(direction, (Vector3.one * currentRadius / 2f));
        Interaction breambleInteraction = brambleBall.GetComponent<Interaction>();
        if (breambleInteraction != null) breambleInteraction.SetLocked(1f);
    }
}
