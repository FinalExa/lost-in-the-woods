using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskBasherAttack : Node
{
    private BasherController _basherController;

    public TaskBasherAttack(BasherController basherController)
    {
        _basherController = basherController;
    }

    public override NodeState Evaluate()
    {
        ChargeAndAttack();
        state = NodeState.RUNNING;
        return state;
    }

    private void ChargeAndAttack()
    {
        if (_basherController.attackTimer > 0)
        {
            if (_basherController.basherReferences.damageHitBox.activeSelf) _basherController.basherReferences.damageHitBox.SetActive(false);
            _basherController.attackTimer -= Time.deltaTime;
            //SetAttackDuringCharge();
        }
        else
        {
            Attack();
            _basherController.ResetAttackTimer();
        }
    }

    private void Attack()
    {
        _basherController.basherReferences.damageHitBox.tag = _basherController.damagingTag;
        RotateParent();
        _basherController.basherReferences.damageHitBox.SetActive(true);
    }

    private void SetAttackDuringCharge()
    {
        if (_basherController.basherReferences.damageHitBox.tag == _basherController.damagingTag) _basherController.basherReferences.damageHitBox.tag = _basherController.notDamagingTag;
    }

    private void RotateParent()
    {
        Transform parent = _basherController.basherReferences.damageHitBox.transform.parent.transform;
        float angle = CalculateAngle(parent.position, _basherController.basherReferences.playerRef.transform.position);
        parent.transform.rotation = Quaternion.Euler(new Vector3(parent.rotation.x, angle + _basherController.attackOffset, parent.rotation.z));
    }

    private float CalculateAngle(Vector3 start, Vector3 end)
    {
        return Mathf.Atan2(end.x - start.x, end.z - start.z) * Mathf.Rad2Deg;
    }
}
