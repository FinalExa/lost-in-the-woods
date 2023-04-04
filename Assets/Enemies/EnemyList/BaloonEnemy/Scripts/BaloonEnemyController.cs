using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonEnemyController : EnemyController, ISendSignalToSelf, ISendWeaponAttackType
{
    [HideInInspector] public BaloonWeaponSwitcher baloonWeaponSwitcher;
    [HideInInspector] public GrabbableByPlayer grabbableByPlayer;
    [HideInInspector] public BaloonRotator baloonRotator;
    private Color baseColor;
    [HideInInspector] public EnemyHealth enemyHealth;
    [HideInInspector] public bool vulnerable;
    [HideInInspector] public bool absorbed;
    private bool executedFreeFromAbsorbAttack;
    [SerializeField] private SpriteRenderer spriteRef;
    public WeaponAttack.WeaponAttackType ReceivedWeaponAttackType { get; set; }
    [System.Serializable]
    public struct AbsorbableTypes
    {
        public WeaponAttack.WeaponAttackType absorbableAttackType;
        public string absorbableName;
        public Weapon thisTypeNormalWeapon;
        public Weapon thisTypeCalmWeapon;
        public Weapon thisTypeBerserkWeapon;
        public Color feedbackColor;
    }
    public List<AbsorbableTypes> absorbableTypes;


    protected override void Awake()
    {
        base.Awake();
        baloonWeaponSwitcher = this.gameObject.GetComponent<BaloonWeaponSwitcher>();
        grabbableByPlayer = this.gameObject.GetComponent<GrabbableByPlayer>();
        enemyHealth = this.gameObject.GetComponent<EnemyHealth>();
        baloonRotator = this.gameObject.GetComponent<BaloonRotator>();
    }

    private void Start()
    {
        baseColor = spriteRef.color;
        executedFreeFromAbsorbAttack = false;
    }

    public void OnSignalReceived(GameObject source)
    {
        Absorb();
    }

    private void Absorb()
    {
        if (!absorbed && !enemyCombo.isInCombo)
        {
            foreach (AbsorbableTypes absorbableType in absorbableTypes)
            {
                if ((ReceivedWeaponAttackType != WeaponAttack.WeaponAttackType.GENERIC && ReceivedWeaponAttackType == absorbableType.absorbableAttackType) || interaction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(absorbableType.absorbableName))
                {
                    spriteRef.color = absorbableType.feedbackColor;
                    baloonWeaponSwitcher.SetWeapons(absorbableType.absorbableAttackType);
                    absorbed = true;
                    executedFreeFromAbsorbAttack = false;
                    break;
                }
            }
        }
    }

    public void EndAbsorb()
    {
        if (!executedFreeFromAbsorbAttack) executedFreeFromAbsorbAttack = true;
        else
        {
            spriteRef.color = baseColor;
            baloonWeaponSwitcher.SetWeapons(WeaponAttack.WeaponAttackType.GENERIC);
            absorbed = false;
        }
    }

    public void HPDeplete()
    {
        thisNavMeshAgent.enabled = false;
        baloonRotator.followPlayerRotation = true;
        grabbableByPlayer.lockedGrabbable = false;
        vulnerable = true;
    }

    public void HPReset()
    {
        enemyHealth.HealthAddValue(enemyData.maxHP);
        thisNavMeshAgent.enabled = true;
        baloonRotator.followPlayerRotation = false;
        grabbableByPlayer.lockedGrabbable = true;
        vulnerable = false;
    }
}
