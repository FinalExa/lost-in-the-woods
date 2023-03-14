using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : MonoBehaviour
{
    [HideInInspector] public string curState;
    [HideInInspector] public float actualSpeed;
    [HideInInspector] public PCReferences pcReferences;
    [HideInInspector] public Weapon thisWeapon;
    [SerializeField] private GameObject grabPosition;
    private GrabbableByPlayer grabbedObject;
    private Zone currentZone;

    private void Awake()
    {
        pcReferences = this.gameObject.GetComponent<PCReferences>();
    }

    private void Start()
    {
        CheckStartingZone();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void ChangePlayerZone(Zone zoneToSet)
    {
        if (currentZone != null) currentZone.SetPlayerOutOfZone();
        currentZone = zoneToSet;
    }

    public Zone GetCurrentZone()
    {
        return currentZone;
    }

    public void SetGrabbedObject(GrabbableByPlayer objectToSet)
    {
        if (grabbedObject != null)
        {
            grabbedObject = objectToSet;
        }
    }

    public void RemoveGrabbedObject()
    {
        grabbedObject = null;
    }

    private void CheckStartingZone()
    {
        Collider[] collidersTouchingPlayerAtStart = Physics.OverlapBox(this.transform.position, new Vector3(0.1f, 2f, 0.1f));
        foreach (Collider collider in collidersTouchingPlayerAtStart)
        {
            ZoneGround zoneGround = collider.GetComponent<ZoneGround>();
            if (zoneGround != null) zoneGround.SetPlayerInZone(this.gameObject.GetComponent<Collider>());
        }
    }
}
