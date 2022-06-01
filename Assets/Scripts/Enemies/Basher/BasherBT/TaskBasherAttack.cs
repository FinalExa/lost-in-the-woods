using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskBasherAttack : Node
{
    private float _attackChargeMaxTime;
    private float _attackChargeTimer;
    private GameObject _damageHitBox;
    private GameObject _playerRef;
    private string _notDamagingTag;
    private string _damagingTag;
    private float _offset;

    public TaskBasherAttack(float attackChargeMaxTime, GameObject damageHitBox, string notDamagingTag, string damagingTag, GameObject playerRef, float offset)
    {
        _attackChargeMaxTime = attackChargeMaxTime;
        _attackChargeTimer = _attackChargeMaxTime;
        _damageHitBox = damageHitBox;
        _playerRef = playerRef;
        _notDamagingTag = notDamagingTag;
        _damagingTag = damagingTag;
        _offset = offset;
    }

    public override NodeState Evaluate()
    {
        if (_attackChargeTimer > 0)
        {
            _attackChargeTimer -= Time.deltaTime;
            SetAttackDuringCharge();
        }
        else
        {
            Attack();
            _attackChargeTimer = _attackChargeMaxTime;
            state = NodeState.SUCCESS;
            return state;
        }
        state = NodeState.RUNNING;
        return state;
    }

    private void Attack()
    {
        RotateParent();
        _damageHitBox.tag = _damagingTag;
    }

    private void SetAttackDuringCharge()
    {
        if (_damageHitBox.tag == _damagingTag)
        {
            _damageHitBox.tag = _notDamagingTag;
        }
    }

    private void RotateParent()
    {
        Transform parent = _damageHitBox.transform.parent.transform;
        float angle = CalculateAngle(parent.position, _playerRef.transform.position);
        parent.transform.rotation = Quaternion.Euler(new Vector3(parent.rotation.x, angle + _offset, parent.rotation.z));
    }

    private float CalculateAngle(Vector3 start, Vector3 end)
    {
        return Mathf.Atan2(end.x - start.x, end.z - start.z) * Mathf.Rad2Deg;
    }
}
