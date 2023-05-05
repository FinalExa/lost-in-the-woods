using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveLightInstance : MonoBehaviour
{
    [SerializeField] private string adaptiveLightName;
    private AdaptiveLightFeedback adaptiveLightFeedback;
    private Light thisLight;

    private void Awake()
    {
        adaptiveLightFeedback = FindObjectOfType<AdaptiveLightFeedback>();
        thisLight = this.gameObject.GetComponent<Light>();
        AdaptiveLightFeedback.adaptiveLightCheck += QueryForActivation;
    }

    private void OnEnable()
    {
        QueryForActivation();
    }

    private void QueryForActivation()
    {
        thisLight.enabled = adaptiveLightFeedback.QueryCurrentAdaptiveLight(adaptiveLightName);
    }
}
