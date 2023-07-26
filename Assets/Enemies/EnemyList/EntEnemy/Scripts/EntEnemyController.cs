using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntEnemyController : EnemyController, ISendSignalToSelf
{
    private EntEnemyWeaponSwitcher entEnemyWeaponSwitcher;
    public EnemyData vigorousData;
    [SerializeField] private string swapToVigorousName;
    public EnemyData depletedData;
    [SerializeField] private string swapToDepletedName;
    [HideInInspector] public bool stunned;
    [SerializeField] private float stunTotalTime;
    private float stunTimer;
    [SerializeField] private SpriteRenderer entModeSwitchFeedback;
    [SerializeField] private int maxAlpha;
    private float alphaPercentage;
    private float switchFeedbackValueToAdd;
    private float finalFeedbackValue;

    protected override void Awake()
    {
        base.Awake();
        entEnemyWeaponSwitcher = this.gameObject.GetComponent<EntEnemyWeaponSwitcher>();
    }

    private void Start()
    {
        alphaPercentage = (float)maxAlpha / 255f;
        if (enemyData == vigorousData) entModeSwitchFeedback.color = new Color(entModeSwitchFeedback.color.r, entModeSwitchFeedback.color.g, entModeSwitchFeedback.color.b, 0);
        else entModeSwitchFeedback.color = new Color(entModeSwitchFeedback.color.r, entModeSwitchFeedback.color.g, entModeSwitchFeedback.color.b, alphaPercentage);
    }

    private void Update()
    {
        StunTimer();
    }

    public void OnSignalReceived(GameObject source)
    {
        EntCheckForSwap();
    }

    private void StunTimer()
    {
        if (stunned)
        {
            if (stunTimer > 0)
            {
                stunTimer -= Time.deltaTime;
                finalFeedbackValue = Mathf.Clamp(finalFeedbackValue + (switchFeedbackValueToAdd * Time.deltaTime), 0f, alphaPercentage);
                entModeSwitchFeedback.color = new Color(entModeSwitchFeedback.color.r, entModeSwitchFeedback.color.g, entModeSwitchFeedback.color.b, finalFeedbackValue);
            }
            else stunned = false;
        }
    }

    private void SetupFeedbackSpriteSwap()
    {
        float multiplier = 1f;
        float startValue = 0;
        if (enemyData == vigorousData)
        {
            multiplier = -1f;
            startValue = alphaPercentage;
        }
        entModeSwitchFeedback.color = new Color(entModeSwitchFeedback.color.r, entModeSwitchFeedback.color.g, entModeSwitchFeedback.color.b, startValue);
        finalFeedbackValue = startValue;
        switchFeedbackValueToAdd = ((float)alphaPercentage / stunTotalTime) * multiplier;
    }

    private void EntCheckForSwap()
    {
        if (InteractionContainsName(swapToVigorousName) && enemyData != vigorousData) SetStunned(vigorousData);
        else if (InteractionContainsName(swapToDepletedName) && enemyData != depletedData) SetStunned(depletedData);
    }

    private void SetStunned(EnemyData newData)
    {
        enemyCombo.EndCombo();
        stunned = true;
        stunTimer = stunTotalTime;
        enemyData = newData;
        SetupFeedbackSpriteSwap();
        entEnemyWeaponSwitcher.SwitchEntWeapons();
    }
}
