using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : MonoBehaviour
{
    [SerializeField] private string whoToDamage;

    [HideInInspector] public string curState;
    [HideInInspector] public float actualSpeed;
    [HideInInspector] public PCReferences pcReferences;
    [HideInInspector] public Weapon thisWeapon;

    private void Start()
    {
        SetupWeapon();
    }

    private void Awake()
    {
        pcReferences = this.gameObject.GetComponent<PCReferences>();
    }

    private void SetupWeapon()
    {
        thisWeapon = this.gameObject.GetComponentInChildren<Weapon>();
        thisWeapon.damageTag = whoToDamage;
        pcReferences.pcCombo.currentWeapon = thisWeapon;
    }
}
