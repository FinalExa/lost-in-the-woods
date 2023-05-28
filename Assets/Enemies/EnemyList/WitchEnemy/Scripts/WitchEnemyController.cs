using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WitchEnemyController : EnemyController, ISendSignalToSelf
{
    [SerializeField] private string[] weakNames;
    [SerializeField] private float witchCryingDuration;
    [SerializeField] private float pathInvalidDuration;
    private float witchCryingTimer;
    private float pathInvalidTimer;
    private bool pathInvalid;
    [HideInInspector] public bool witchWeak;
    [HideInInspector] public bool witchCrying;
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

    private void Update()
    {
        WitchCrying();
        PathInvalidTimer();
    }

    public override void LightStateUpdate()
    {
        WitchWeakOperations();
    }

    public void OnSignalReceived(GameObject source)
    {
        WitchWeakOperations();
    }

    private void WitchWeakOperations()
    {
        bool status = CheckSetWeak();
        if (status && !witchWeak)
        {
            witchWeak = true;
            if (witchCrying) WitchStopCrying();
        }
        else if (!status && witchWeak)
        {
            WitchStartCrying();
            witchWeak = false;
        }
    }

    private void WitchCrying()
    {
        if (witchCrying)
        {
            if (witchCryingTimer > 0) witchCryingTimer -= Time.deltaTime;
            else WitchStopCrying();
        }
    }

    private void WitchStartCrying()
    {
        witchCrying = true;
        witchCryingTimer = witchCryingDuration;
    }

    private void WitchStopCrying()
    {
        witchCrying = false;
    }

    private bool CheckSetWeak()
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
        if (thisNavMeshAgent.path.status == NavMeshPathStatus.PathInvalid || thisNavMeshAgent.path.status == NavMeshPathStatus.PathPartial) SetPathInvalidTimer();
    }

    private void SetPathInvalidTimer()
    {
        pathInvalid = true;
        pathInvalidTimer = pathInvalidDuration;
    }

    private void PathInvalidTimer()
    {
        if (pathInvalid)
        {
            if (pathInvalidTimer > 0) pathInvalidTimer -= Time.deltaTime;
            else
            {
                Vector3 newPos = this.transform.position + (this.transform.position - backLeap.transform.position) * 2;
                leapDestination = newPos;
                thisNavMeshAgent.SetDestination(newPos);
                pathInvalid = false;
            }
        }
    }

    public void EndLeap()
    {
        AttackDone = false;
        canLeap = false;
    }
}
