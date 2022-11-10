using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;

    private void Awake()
    {
        if (target == null)
        {
            target = FindObjectOfType<PCController>().gameObject.transform;
        }
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        this.transform.position = target.position + offset;
    }
}
