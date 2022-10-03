using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskEnemyAttack : Node
{
    private TestEnemyController _testEnemyController;
    private bool postAttack;

    public TaskEnemyAttack(TestEnemyController testEnemyController)
    {
        _testEnemyController = testEnemyController;
    }

    public override NodeState Evaluate()
    {
        if (!postAttack) ChargeAndAttack();
        else StayStill();
        state = NodeState.RUNNING;
        return state;
    }

    private void ChargeAndAttack()
    {
        if (_testEnemyController.attackTimer > 0)
        {
            if (_testEnemyController.testEnemyReferences.damageHitBox.activeSelf) _testEnemyController.testEnemyReferences.damageHitBox.SetActive(false);
            _testEnemyController.attackTimer -= Time.deltaTime;
        }
        else
        {
            _testEnemyController.ResetAttackTimer();
            _testEnemyController.ResetPostAttackTimer();
            Attack();
        }
    }

    private void StayStill()
    {
        if (_testEnemyController.postAttackTimer > 0) _testEnemyController.postAttackTimer -= Time.deltaTime;
        else postAttack = false;
    }

    private void Attack()
    {
        postAttack = true;
        RotateParent();
        _testEnemyController.testEnemyReferences.damageHitBox.SetActive(true);
    }

    private void RotateParent()
    {
        Transform parent = _testEnemyController.testEnemyReferences.damageHitBox.transform.parent.transform;
        float angle = CalculateAngle(parent.position, _testEnemyController.testEnemyReferences.playerRef.transform.position);
        parent.transform.rotation = Quaternion.Euler(new Vector3(parent.rotation.x, angle + _testEnemyController.attackOffset, parent.rotation.z));
    }

    private float CalculateAngle(Vector3 start, Vector3 end)
    {
        return Mathf.Atan2(end.x - start.x, end.z - start.z) * Mathf.Rad2Deg;
    }
}
