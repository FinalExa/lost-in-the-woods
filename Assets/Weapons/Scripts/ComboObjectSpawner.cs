using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboObjectSpawner
{
    private Combo combo;

    public ComboObjectSpawner(Combo _combo)
    {
        combo = _combo;
    }

    public void CheckObjectsToSpawn(WeaponAttack currentAttack, int attackFrame, Vector3 lastDirection)
    {
        for (int i = 0; i < currentAttack.weaponSpawnsObjectDuringThisAttack.Length; i++)
        {
            if (!currentAttack.weaponSpawnsObjectDuringThisAttack[i].spawned && attackFrame >= currentAttack.weaponSpawnsObjectDuringThisAttack[i].launchFrame)
            {
                SpawnObject(currentAttack, i, lastDirection);
            }
        }
    }

    private void SpawnObject(WeaponAttack currentAttack, int currentIndex, Vector3 lastDirection)
    {
        WeaponAttack.WeaponSpawnsObjectDuringThisAttack currentObjectToSpawn = currentAttack.weaponSpawnsObjectDuringThisAttack[currentIndex];
        currentObjectToSpawn.spawned = true;
        GameObject objectToLaunch = GameObject.Instantiate(currentObjectToSpawn.objectRef, currentObjectToSpawn.objectStartPosition.transform.position, Quaternion.identity, combo.gameObject.transform.parent);
        CheckForGrabbable(objectToLaunch);
        IHaveSettableDirection haveSettableDirection = objectToLaunch.GetComponent<IHaveSettableDirection>();
        Rigidbody objectToLaunchRb = objectToLaunch.GetComponent<Rigidbody>();
        if (haveSettableDirection != null) haveSettableDirection.SetDirection(lastDirection);
        if (objectToLaunchRb != null) objectToLaunchRb.velocity = lastDirection * currentObjectToSpawn.launchSpeed;
        currentAttack.weaponSpawnsObjectDuringThisAttack[currentIndex] = currentObjectToSpawn;
    }

    private void CheckForGrabbable(GameObject objectToLaunch)
    {
        GrabbableByPlayer grabbableByPlayer = objectToLaunch.gameObject.GetComponent<GrabbableByPlayer>();
        if (grabbableByPlayer != null)
        {
            grabbableByPlayer.ManualStartup();
            grabbableByPlayer.SetStartParent(objectToLaunch.transform.parent);
            grabbableByPlayer.ReleaseFromBeingGrabbed();
        }
    }

    public void ResetObjectsToSpawn(WeaponAttack currentAttack)
    {
        for (int i = 0; i < currentAttack.weaponSpawnsObjectDuringThisAttack.Length; i++)
        {
            currentAttack.weaponSpawnsObjectDuringThisAttack[i].spawned = false;
        }
    }
}
