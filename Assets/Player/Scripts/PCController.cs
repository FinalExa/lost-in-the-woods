using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : MonoBehaviour
{
    [HideInInspector] public string curState;
    [HideInInspector] public float actualSpeed;
    [HideInInspector] public PCReferences pcReferences;
    [HideInInspector] public Weapon thisWeapon;
    private Zone currentZone;

    private void Start()
    {
        SetupWeapon();
    }

    private void Awake()
    {
        pcReferences = this.gameObject.GetComponent<PCReferences>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private void SetupWeapon()
    {
        thisWeapon = this.gameObject.GetComponentInChildren<Weapon>();
        pcReferences.pcCombo.currentWeapon = thisWeapon;
    }

    public void ChangePlayerZone(Zone zoneToSet)
    {
        if (currentZone != null) currentZone.SetPlayerOutOfZone();
        currentZone = zoneToSet;
    }
}
