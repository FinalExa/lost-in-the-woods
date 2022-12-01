using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchEnemyController : EnemyController
{
    [HideInInspector] public WitchEnemyData witchEnemyData;
    [HideInInspector] public bool canLeap;
    [HideInInspector] public Vector3 leapDestination;
    public GameObject backLeap;
    public GameObject rightLeap;
    public GameObject leftLeap;

    protected override void Start()
    {
        base.Start();
        witchEnemyData = (WitchEnemyData)enemyData;
    }

    public void DecideLeapObject()
    {
        if (affectedByLight.lightState == AffectedByLight.LightState.NORMAL) leapDestination = CalculateLeapPoint(backLeap);
        else if (affectedByLight.lightState == AffectedByLight.LightState.BERSERK) DistanceBetweenSideLeaps();
    }

    private void DistanceBetweenSideLeaps()
    {
        Vector3 rightPoint = CalculateLeapPoint(rightLeap);
        Vector3 leftPoint = CalculateLeapPoint(leftLeap);
        float rightDistance = Vector3.Distance(this.transform.position, rightPoint);
        float leftDistance = Vector3.Distance(this.transform.position, leftPoint);
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
        Vector3 hitDirection = (leapPoint.transform.position - this.gameObject.transform.position);
        if (Physics.Raycast(this.gameObject.transform.position, hitDirection, out RaycastHit hit))
        {
            Vector3 hitPoint = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);
            return hitPoint;
        }
        else return Vector3.zero;
    }
}
