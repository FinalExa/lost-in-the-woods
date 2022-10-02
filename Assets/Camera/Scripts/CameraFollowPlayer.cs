using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    Transform pc;

    private void Awake()
    {
        pc = FindObjectOfType<PCController>().gameObject.transform;
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        this.transform.position = pc.position + offset;
    }
}
