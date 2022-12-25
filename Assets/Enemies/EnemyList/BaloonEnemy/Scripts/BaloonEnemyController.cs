using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonEnemyController : EnemyController, ISendSignalToSelf, ISendWeaponAttackType
{
    private EnemyWeaponSwitcher enemyWeaponSwitcher;
    public WeaponAttack.WeaponAttackType ReceivedWeaponAttackType { get; set; }
    [SerializeField] private List<WeaponAttack.WeaponAttackType> absorbableAttackTypes;
    [SerializeField] private List<WeaponAttack.WeaponAttackType> repelAttackTypes;
    private bool hasAbsorbed;
    private WeaponAttack.WeaponAttackType absorbedAttackType;
    [SerializeField] private List<WeaponAttack> normalWeaponAttacks;
    [SerializeField] private List<WeaponAttack> normalWeaponAttacksAbsorbed;
    private List<WeaponAttack> calmWeaponAttacks;
    private List<WeaponAttack> calmWeaponAttacksAbsorbed;
    private List<WeaponAttack> berserkWeaponAttacks;
    private List<WeaponAttack> berserkWeaponAttacksAbsorbed;

    protected override void Awake()
    {
        base.Awake();
        enemyWeaponSwitcher = this.gameObject.GetComponent<EnemyWeaponSwitcher>();
    }

    private void Start()
    {
        PrepareLists();
    }

    public void OnSignalReceived(GameObject source)
    {
        if (absorbableAttackTypes.Contains(ReceivedWeaponAttackType) && !hasAbsorbed) Absorb(absorbableAttackTypes.IndexOf(ReceivedWeaponAttackType));
        else if (repelAttackTypes.Contains(ReceivedWeaponAttackType)) ForcedAttack();
    }

    private void PrepareLists()
    {
        normalWeaponAttacks = enemyWeaponSwitcher.normalStateWeapon.weaponAttacks;
        normalWeaponAttacksAbsorbed = CreateAbsorbedAttackTypeList(absorbableAttackTypes, normalWeaponAttacks);
        calmWeaponAttacks = enemyWeaponSwitcher.calmStateWeapon.weaponAttacks;
        calmWeaponAttacksAbsorbed = CreateAbsorbedAttackTypeList(absorbableAttackTypes, calmWeaponAttacks);
        berserkWeaponAttacks = enemyWeaponSwitcher.berserkStateWeapon.weaponAttacks;
        berserkWeaponAttacksAbsorbed = CreateAbsorbedAttackTypeList(absorbableAttackTypes, berserkWeaponAttacks);
    }

    private List<WeaponAttack> CreateAbsorbedAttackTypeList(List<WeaponAttack.WeaponAttackType> absorbedAttackTypes, List<WeaponAttack> referenceList)
    {
        List<WeaponAttack> listToPass = new List<WeaponAttack>();
        for (int attackTypeIndex = 0; attackTypeIndex < absorbableAttackTypes.Count; attackTypeIndex++)
        {
            List<WeaponAttack.WeaponAttackType> attackTypeToSwitch = new List<WeaponAttack.WeaponAttackType>();
            attackTypeToSwitch.Add(absorbableAttackTypes[attackTypeIndex]);
            for (int referenceIndex = 0; referenceIndex < referenceList.Count; referenceIndex++)
            {
                WeaponAttack attackToAdd = new WeaponAttack();
                attackToAdd = referenceList[referenceIndex];
                attackToAdd.weaponAttackTypes = attackTypeToSwitch;
                listToPass.Add(attackToAdd);
            }
        }
        return listToPass;
    }

    private void Absorb(int indexOfAttackType)
    {
        absorbedAttackType = ReceivedWeaponAttackType;
        print("absorbed " + absorbedAttackType.ToString());
    }

    private void ForcedAttack()
    {
        print("repel");
    }
}
