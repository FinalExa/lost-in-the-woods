using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WitchLeap
{
    private WitchEnemyController witchEnemyController;

    public bool canLeap;
    public bool executingLeap;
    public GameObject backLeap;
    public GameObject rightLeap;
    public GameObject leftLeap;
    [HideInInspector] public Vector3 leapDestination;
    public float leapTolerance;
    public float leapSpeed;
    [SerializeField] private float maxLeapDuration;
    private float maxLeapDurationTimer;
    public void SetController(WitchEnemyController controller)
    {
        witchEnemyController = controller;
    }

    public void SetupLeap()
    {
        if (!canLeap)
        {
            DecideLeapObject();
            canLeap = true;
        }
    }
    public bool CheckIfLeapIsFinishedByDistance()
    {
        float distance = Vector3.Distance(witchEnemyController.transform.position, leapDestination);
        if (distance > leapTolerance)
        {
            if (!executingLeap) StartLeap();
            return false;
        }
        else
        {
            EndLeap();
            return true;
        }
    }

    private void DecideLeapObject()
    {
        if (witchEnemyController.affectedByLight.lightState == AffectedByLight.LightState.NORMAL || witchEnemyController.witchWeak.GetIfWitchIsWeak()) leapDestination = CalculateLeapPoint(backLeap);
        else if (witchEnemyController.affectedByLight.lightState == AffectedByLight.LightState.BERSERK) DistanceBetweenSideLeaps();
    }
    private void DistanceBetweenSideLeaps()
    {
        Vector3 rightPoint = CalculateLeapPoint(rightLeap);
        Vector3 leftPoint = CalculateLeapPoint(leftLeap);
        float rightDistance = Vector3.Distance(witchEnemyController.transform.position, rightPoint);
        float leftDistance = Vector3.Distance(witchEnemyController.transform.position, leftPoint);
        if (rightDistance > leftDistance) leapDestination = rightPoint;
        else if (rightDistance < leftDistance) leapDestination = leftPoint;
        else RandomizeSideLeapPoint(rightPoint, leftPoint);
    }

    private void RandomizeSideLeapPoint(Vector3 rightPoint, Vector3 leftPoint)
    {
        int randomNumber = Random.Range(0, 1);
        if (randomNumber == 0) leapDestination = rightPoint;
        else leapDestination = leftPoint;
    }

    private Vector3 CalculateLeapPoint(GameObject leapPoint)
    {
        Vector3 hitDirection = (leapPoint.transform.position - witchEnemyController.transform.position);
        if (Physics.Raycast(witchEnemyController.transform.position, hitDirection, out RaycastHit hit))
        {
            Vector3 hitPoint = new Vector3(hit.point.x, witchEnemyController.transform.position.y, hit.point.z);
            return hitPoint;
        }
        else return Vector3.zero;
    }

    public void StartLeap()
    {
        executingLeap = true;
        SetupLeapTimer();
        if (witchEnemyController.thisNavMeshAgent.speed != leapSpeed) witchEnemyController.thisNavMeshAgent.speed = leapSpeed;
        if (witchEnemyController.thisNavMeshAgent.isStopped) witchEnemyController.thisNavMeshAgent.isStopped = false;
        witchEnemyController.thisNavMeshAgent.SetDestination(leapDestination);
    }

    public void LeapFinishTimer()
    {
        if (executingLeap)
        {
            if (maxLeapDurationTimer > 0) maxLeapDurationTimer -= Time.deltaTime;
            else EndLeap();
        }
    }

    private void EndLeap()
    {
        witchEnemyController.AttackDone = false;
        canLeap = false;
        executingLeap = false;
    }

    private void SetupLeapTimer()
    {
        maxLeapDurationTimer = maxLeapDuration;
    }

    public bool GetIfWitchCanLeap()
    {
        return canLeap;
    }

    public bool GetIfWitchIsExecutingLeap()
    {
        return executingLeap;
    }
}
