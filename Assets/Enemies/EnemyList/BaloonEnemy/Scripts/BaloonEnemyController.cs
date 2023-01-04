using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonEnemyController : EnemyController, ISendSignalToSelf, ISendWeaponAttackType
{
    private EnemyWeaponSwitcher enemyWeaponSwitcher;
    [SerializeField] private SpriteRenderer spriteRef;
    private Color baseColor;
    public WeaponAttack.WeaponAttackType ReceivedWeaponAttackType { get; set; }
    [System.Serializable]
    public struct AbsorbableAttackTypes
    {
        public WeaponAttack.WeaponAttackType attackType;
        public Color feedbackColor;
    }
    [SerializeField] private List<AbsorbableAttackTypes> absorbableAttackTypes;
    [SerializeField] private List<WeaponAttack.WeaponAttackType> repelAttackTypes;
    private bool absorbed;
    [System.Serializable]
    public struct AttackTypeStorage
    {
        public List<WeaponAttack.WeaponAttackType> attackTypes;
    }
    private List<AttackTypeStorage> normalWeaponAttackTypes;
    private List<AttackTypeStorage> normalWeaponAttackAbsorbedTypes;
    private List<AttackTypeStorage> calmWeaponAttackTypes;
    private List<AttackTypeStorage> calmWeaponAttackAbsorbedTypes;
    private List<AttackTypeStorage> berserkWeaponAttackTypes;
    private List<AttackTypeStorage> berserkWeaponAttackAbsorbedTypes;


    protected override void Awake()
    {
        base.Awake();
        enemyWeaponSwitcher = this.gameObject.GetComponent<EnemyWeaponSwitcher>();
    }

    private void Start()
    {
        baseColor = spriteRef.color;
        PrepareLists();
    }

    public void OnSignalReceived(GameObject source)
    {
        int index = SearchAbsorbedAttack();
        if (index >= 0) Absorb(index);
        else if (repelAttackTypes.Contains(ReceivedWeaponAttackType) && !enemyCombo.isInCombo) Redirect();
    }

    private int SearchAbsorbedAttack()
    {
        for (int i = 0; i < absorbableAttackTypes.Count; i++)
        {
            if (absorbableAttackTypes[i].attackType == ReceivedWeaponAttackType) return i;
        }
        return -1;
    }

    private void PrepareLists()
    {
        normalWeaponAttackTypes = GetAttackTypesByList(enemyWeaponSwitcher.normalStateWeapon.weaponAttacks);
        normalWeaponAttackAbsorbedTypes = CreateAbsorbedAttackTypeList(enemyWeaponSwitcher.normalStateWeapon.weaponAttacks);
        calmWeaponAttackTypes = GetAttackTypesByList(enemyWeaponSwitcher.calmStateWeapon.weaponAttacks);
        calmWeaponAttackAbsorbedTypes = CreateAbsorbedAttackTypeList(enemyWeaponSwitcher.calmStateWeapon.weaponAttacks);
        berserkWeaponAttackTypes = GetAttackTypesByList(enemyWeaponSwitcher.berserkStateWeapon.weaponAttacks);
        berserkWeaponAttackAbsorbedTypes = CreateAbsorbedAttackTypeList(enemyWeaponSwitcher.berserkStateWeapon.weaponAttacks);
    }

    private List<AttackTypeStorage> GetAttackTypesByList(List<WeaponAttack> referenceList)
    {
        List<AttackTypeStorage> attackTypeStorage = new List<AttackTypeStorage>();
        for (int i = 0; i < referenceList.Count; i++)
        {
            AttackTypeStorage store = new AttackTypeStorage();
            store.attackTypes = referenceList[i].weaponAttackTypes;
            attackTypeStorage.Add(store);
        }
        return attackTypeStorage;
    }

    private List<AttackTypeStorage> CreateAbsorbedAttackTypeList(List<WeaponAttack> referenceList)
    {
        List<AttackTypeStorage> listToPass = new List<AttackTypeStorage>();
        for (int attackTypeIndex = 0; attackTypeIndex < absorbableAttackTypes.Count; attackTypeIndex++)
        {
            for (int referenceIndex = 0; referenceIndex < referenceList.Count; referenceIndex++)
            {
                AttackTypeStorage store = new AttackTypeStorage();
                List<WeaponAttack.WeaponAttackType> attackTypesToAdd = new List<WeaponAttack.WeaponAttackType>();
                attackTypesToAdd.Add(absorbableAttackTypes[attackTypeIndex].attackType);
                store.attackTypes = attackTypesToAdd;
                listToPass.Add(store);
            }
        }
        return listToPass;
    }

    private List<WeaponAttack> SetAbsorbWeaponAttacks(List<WeaponAttack> listToSet, int indexOfAttackType, int baseLenghtOfAttacks)
    {
        int startPoint = indexOfAttackType * baseLenghtOfAttacks;
        for (int i = startPoint; i < startPoint + baseLenghtOfAttacks; i++)
        {
            List<WeaponAttack.WeaponAttackType> resetElement = new List<WeaponAttack.WeaponAttackType>();
            resetElement.Add(absorbableAttackTypes[indexOfAttackType].attackType);
            listToSet[i - startPoint].weaponAttackTypes = resetElement;
        }
        return listToSet;
    }

    private List<WeaponAttack> ResetWeaponAttacks(List<WeaponAttack> listToSet, List<AttackTypeStorage> storage)
    {
        for (int i = 0; i < listToSet.Count; i++)
        {
            listToSet[i].weaponAttackTypes = storage[i].attackTypes;
        }
        return listToSet;
    }

    private void Absorb(int indexOfAttackType)
    {
        if (!absorbed && affectedByLight.lightState != AffectedByLight.LightState.BERSERK && enemyCombo.isInCombo)
        {
            enemyWeaponSwitcher.normalStateWeapon.weaponAttacks = SetAbsorbWeaponAttacks(enemyWeaponSwitcher.normalStateWeapon.weaponAttacks, indexOfAttackType, normalWeaponAttackTypes.Count);
            enemyWeaponSwitcher.calmStateWeapon.weaponAttacks = SetAbsorbWeaponAttacks(enemyWeaponSwitcher.calmStateWeapon.weaponAttacks, indexOfAttackType, calmWeaponAttackTypes.Count);
            enemyWeaponSwitcher.berserkStateWeapon.weaponAttacks = SetAbsorbWeaponAttacks(enemyWeaponSwitcher.berserkStateWeapon.weaponAttacks, indexOfAttackType, berserkWeaponAttackTypes.Count);
            spriteRef.color = absorbableAttackTypes[indexOfAttackType].feedbackColor;
            absorbed = true;
        }
    }

    public void StopAbsorb()
    {
        if (absorbed && affectedByLight.lightState != AffectedByLight.LightState.BERSERK && attackDone)
        {
            enemyWeaponSwitcher.normalStateWeapon.weaponAttacks = ResetWeaponAttacks(enemyWeaponSwitcher.normalStateWeapon.weaponAttacks, normalWeaponAttackTypes);
            enemyWeaponSwitcher.calmStateWeapon.weaponAttacks = ResetWeaponAttacks(enemyWeaponSwitcher.calmStateWeapon.weaponAttacks, calmWeaponAttackTypes);
            enemyWeaponSwitcher.berserkStateWeapon.weaponAttacks = ResetWeaponAttacks(enemyWeaponSwitcher.berserkStateWeapon.weaponAttacks, berserkWeaponAttackTypes);
            spriteRef.color = baseColor;
            absorbed = false;
            attackDone = false;
        }
    }

    private void Redirect()
    {
        Vector3 pointDirection = (this.transform.position - playerTarget.transform.position);
        Vector3 endPoint = this.transform.position + pointDirection * 5f;
        if (!enemyCombo.isInCombo) enemyCombo.ActivateEnemyCombo(endPoint);
    }
}
