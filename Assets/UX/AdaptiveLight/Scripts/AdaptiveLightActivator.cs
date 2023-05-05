using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdaptiveLightActivator : MonoBehaviour
{
    [SerializeField] private List<string> namesToActivate;
    private AdaptiveLightFeedback adaptiveLightFeedback;

    private void Awake()
    {
        adaptiveLightFeedback = FindObjectOfType<AdaptiveLightFeedback>();
    }

    private void Update()
    {
        CheckForGrab();
    }

    private void CheckForGrab()
    {
        if (adaptiveLightFeedback != null)
        {
            if (this.gameObject.transform.parent != null && this.gameObject.transform.parent.gameObject.CompareTag("PlayerGrab") && adaptiveLightFeedback.currentRef != this) adaptiveLightFeedback.ChangeAdaptiveLightList(this, namesToActivate);
            else if ((this.gameObject.transform.parent == null || !this.gameObject.transform.parent.gameObject.CompareTag("PlayerGrab")) && adaptiveLightFeedback.currentRef == this) adaptiveLightFeedback.ClearAdaptiveLightList();
        }
    }
}
