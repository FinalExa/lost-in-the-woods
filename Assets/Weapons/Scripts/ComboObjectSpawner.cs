using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboObjectSpawner
{
    public void CheckObjectsToSpawn(WeaponAttack currentAttack, float attackCountTime, Vector3 lastDirection)
    {
        for (int i = 0; i < currentAttack.weaponSpawnsObjectDuringThisAttack.Length; i++)
        {
            if (!currentAttack.weaponSpawnsObjectDuringThisAttack[i].spawned && attackCountTime >= currentAttack.weaponSpawnsObjectDuringThisAttack[i].launchTimeAfterStart)
            {
                SpawnObject(currentAttack, i, lastDirection);
            }
        }
    }

    private void SpawnObject(WeaponAttack currentAttack, int currentIndex, Vector3 lastDirection)
    {
        WeaponAttack.WeaponSpawnsObjectDuringThisAttack currentObjectToSpawn = currentAttack.weaponSpawnsObjectDuringThisAttack[currentIndex];
        currentObjectToSpawn.spawned = true;
        GameObject objectToLaunch = GameObject.Instantiate(currentObjectToSpawn.objectRef, currentObjectToSpawn.objectStartPosition.transform.position, Quaternion.identity);
        IHaveSettableDirection haveSettableDirection = objectToLaunch.GetComponent<IHaveSettableDirection>();
        if (haveSettableDirection != null) haveSettableDirection.SetDirection(lastDirection);
        currentAttack.weaponSpawnsObjectDuringThisAttack[currentIndex] = currentObjectToSpawn;
    }

    public void ResetObjectsToSpawn(WeaponAttack currentAttack)
    {
        for (int i = 0; i < currentAttack.weaponSpawnsObjectDuringThisAttack.Length; i++)
        {
            currentAttack.weaponSpawnsObjectDuringThisAttack[i].spawned = false;
        }
    }
}
