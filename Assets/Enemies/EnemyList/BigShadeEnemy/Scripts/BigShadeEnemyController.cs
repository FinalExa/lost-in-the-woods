using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShadeEnemyController : EnemyController, ISendSignalToSelf
{
    [HideInInspector] public bool isVulnerable;
    [SerializeField] private int shadesNeededToPass;
    [SerializeField] private float bigShadeAbsorbRangeSize;
    [SerializeField] private SphereCollider bigShadeAbsorbRangeTrigger;
    private int shadesCount;

    private void OnEnable()
    {
        shadesCount = 0;
        bigShadeAbsorbRangeTrigger.radius = bigShadeAbsorbRangeSize;
        bigShadeAbsorbRangeTrigger.enabled = false;
    }

    public override void LightStateUpdate()
    {
        base.LightStateUpdate();
        if (affectedByLight.lightState == AffectedByLight.LightState.CALM)
        {
            isVulnerable = true;
            bigShadeAbsorbRangeTrigger.enabled = true;
        }
        else
        {
            isVulnerable = false;
            bigShadeAbsorbRangeTrigger.enabled = false;
        }
    }

    public void OnSignalReceived(GameObject source)
    {
        if (isVulnerable) IncreaseShadeCount(source);
    }

    private void IncreaseShadeCount(GameObject source)
    {
        shadesCount++;
        source.SetActive(false);
        if (shadesCount == shadesNeededToPass) this.gameObject.SetActive(false);
    }
}
