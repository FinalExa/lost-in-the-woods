using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WitchEnemyController : EnemyController, ISendSignalToSelf
{
    [SerializeField] private string[] weakNames;
    [HideInInspector] public bool witchWeak;
    [HideInInspector] public EnemyData witchEnemyData;
    [HideInInspector] public bool canLeap;
    [HideInInspector] public Vector3 leapDestination;
    public float leapTolerance;
    public float leapSpeed;
    public GameObject backLeap;
    public GameObject rightLeap;
    public GameObject leftLeap;

    protected override void Awake()
    {
        base.Awake();
        interaction = this.gameObject.GetComponent<Interaction>();
    }

    public override void LightStateUpdate()
    {
        CheckSetWeak();
    }

    public void OnSignalReceived(GameObject source)
    {
        witchWeak = CheckSetWeak();
    }

    public bool CheckSetWeak()
    {
        if (affectedByLight.lightState == AffectedByLight.LightState.CALM) return SetWeak();
        else foreach (string name in weakNames) if (interaction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(name)) return SetWeak();
        return SetNotWeak();
    }

    private bool SetWeak()
    {
        thisNavMeshAgent.isStopped = true;
        return true;
    }

    private bool SetNotWeak()
    {
        thisNavMeshAgent.isStopped = false;
        return false;
    }

    public void DecideLeapObject()
    {
        if (affectedByLight.lightState == AffectedByLight.LightState.NORMAL || witchWeak) leapDestination = CalculateLeapPoint(backLeap);
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

    public void StartLeap()
    {
        if (thisNavMeshAgent.speed != leapSpeed) thisNavMeshAgent.speed = leapSpeed;
        if (thisNavMeshAgent.isStopped) thisNavMeshAgent.isStopped = false;
        thisNavMeshAgent.SetDestination(leapDestination);
    }

    public void EndLeap()
    {
        attackDone = false;
        canLeap = false;
    }
}
