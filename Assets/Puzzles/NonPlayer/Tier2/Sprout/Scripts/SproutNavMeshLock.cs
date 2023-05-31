using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SproutNavMeshLock : MonoBehaviour
{
    private NavMeshObstacle obstacle;
    private BoxCollider boxCollider;
    [SerializeField] private float globalInterval;

    private void Awake()
    {
        obstacle = this.gameObject.GetComponent<NavMeshObstacle>();
        boxCollider = this.gameObject.GetComponent<BoxCollider>();
    }

    private void Start()
    {
        StartCoroutine(CheckForFreeSpace(globalInterval));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground")) obstacle.enabled = false;
    }

    private IEnumerator CheckForFreeSpace(float interval)
    {
        yield return new WaitForSeconds(interval);
        if (!obstacle.enabled) CheckForColliders();
        LaunchCheck();
    }

    private void CheckForColliders()
    {
        Collider[] collidersInLock = Physics.OverlapBox(this.transform.position, boxCollider.size / 2);
        bool check = true;
        foreach (Collider collider in collidersInLock)
        {
            if (collider.gameObject.CompareTag("Ground"))
            {
                check = false;
                break;
            }
        }
        if (check) obstacle.enabled = true;
    }
    private void LaunchCheck()
    {
        StartCoroutine(CheckForFreeSpace(globalInterval));
    }
}
