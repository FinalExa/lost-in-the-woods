using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PCCombo : MonoBehaviour
{
    private PCReferences pcReferences;
    [SerializeField] private string damagingTag;
    [SerializeField] private string notDamagingTag;
    [SerializeField] private PlayableDirector[] comboHits;
    [HideInInspector] public bool comboHitOver;
    [HideInInspector] public int currentComboProgress;
    [HideInInspector] public float comboBetweenHitsDelayTimer;
    [HideInInspector] public float comboCancelTimer;
    [HideInInspector] public float comboDelayAfterFinish;

    private void Awake()
    {
        pcReferences = this.gameObject.GetComponent<PCReferences>();
    }

    private void Start()
    {
        ComboSetup();
    }

    private void ComboSetup()
    {
        comboHitOver = false;
        currentComboProgress = 0;
        comboHits[currentComboProgress].gameObject.SetActive(true);
        comboBetweenHitsDelayTimer = pcReferences.pcData.comboDelayBetweenHits;
        comboCancelTimer = pcReferences.pcData.comboResetCooldown;
        comboDelayAfterFinish = pcReferences.pcData.comboEndCooldown;
    }

    public void StartComboHit()
    {
        comboHitOver = false;
        pcReferences.pcRotation.rotationEnabled = false;
        comboHits[currentComboProgress].gameObject.tag = damagingTag;
        comboHits[currentComboProgress].Play();
    }

    public void EndComboHit()
    {
        comboHits[currentComboProgress].gameObject.tag = notDamagingTag;
        comboHits[currentComboProgress].gameObject.SetActive(false);
        if (currentComboProgress + 1 == comboHits.Length) currentComboProgress = 0;
        else currentComboProgress++;
        comboHits[currentComboProgress].gameObject.SetActive(true);
        pcReferences.pcRotation.rotationEnabled = true;
        comboHitOver = true;
    }
}
