using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private GameObject objectToTrack;
    [SerializeField] private GameObject objectToModify;
    [SerializeField] private bool inverter;
    private FollowerTracker followerTracker;

    private void Awake()
    {
        if (objectToTrack != null)
        {
            followerTracker = objectToTrack.AddComponent<FollowerTracker>();
        }
    }

    public void SetObjectToModifyStatus(bool status)
    {
        if (inverter) status = !status;
        objectToModify.SetActive(status);
    }
}
