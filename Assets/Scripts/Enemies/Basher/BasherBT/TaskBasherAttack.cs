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

    public TaskBasherAttack(float attackChargeMaxTime, GameObject damageHitBox, string notDamagingTag, string damagingTag, GameObject playerRef)
    {
        _attackChargeMaxTime = attackChargeMaxTime;
        _attackChargeTimer = _attackChargeMaxTime;
        _damageHitBox = damageHitBox;
        _playerRef = playerRef;
        _notDamagingTag = notDamagingTag;
        _damagingTag = damagingTag;
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
        _damageHitBox.tag = _damagingTag;
        RotateParent();
        _damageHitBox.SetActive(true);
    }

    private void SetAttackDuringCharge()
    {
        if (_damageHitBox.tag == _damagingTag)
        {
            _damageHitBox.SetActive(false);
            _damageHitBox.tag = _notDamagingTag;
        }
    }

    private void RotateParent()
    {
        Transform parent = _damageHitBox.transform.parent.transform;
        float angle = Vector3.Angle(parent.parent.position, _playerRef.transform.position);
        parent.rotation = new Quaternion(parent.rotation.x, angle, parent.rotation.z, parent.rotation.w);
    }
}
