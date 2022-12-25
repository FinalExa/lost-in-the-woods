using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISendWeaponAttackType
{
    WeaponAttack.WeaponAttackType ReceivedWeaponAttackType { get; set; }
}
