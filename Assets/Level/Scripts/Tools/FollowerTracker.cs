using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerTracker : MonoBehaviour
{
    private Follower followerRef;

    public void SetFollower(Follower receivedFollower)
    {
        followerRef = receivedFollower;
    }

    private void OnEnable()
    {
        if (followerRef != null) followerRef.SetObjectToModifyStatus(true);
    }

    private void OnDisable()
    {
        if (followerRef != null) followerRef.SetObjectToModifyStatus(false);
    }
}
