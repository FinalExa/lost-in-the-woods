using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RootShieldWallManagement
{
    [SerializeField] private int normalCalmMaxWalls;
    [SerializeField] private int berserkMaxWalls;
    [SerializeField] private int currentMaxWalls;
    private RootShieldEnemyController rootShieldEnemyController;
    [SerializeField] private List<RootShieldAttackWall> activeWalls;

    public void Startup(RootShieldEnemyController _rootShieldEnemyController)
    {
        rootShieldEnemyController = _rootShieldEnemyController;
        activeWalls = new List<RootShieldAttackWall>();
    }

    public void UpdateMaxWallsValue()
    {
        if (rootShieldEnemyController != null)
        {
            if (rootShieldEnemyController.affectedByLight.lightState == AffectedByLight.LightState.BERSERK) currentMaxWalls = berserkMaxWalls;
            else if (rootShieldEnemyController.affectedByLight.lightState != AffectedByLight.LightState.BERSERK)
            {
                currentMaxWalls = normalCalmMaxWalls;
                CheckForWallOverflow();
            }
        }
    }

    public void AddWall(RootShieldAttackWall receivedWall)
    {
        activeWalls.Add(receivedWall);
        CheckForWallOverflow();
    }

    private void CheckForWallOverflow()
    {
        if (activeWalls.Count > currentMaxWalls) RemoveExtraWalls();
    }

    private void RemoveExtraWalls()
    {
        RootShieldAttackWall tempWallRef = activeWalls[0];
        activeWalls.RemoveAt(0);
        tempWallRef.SelfDestruct();
        CheckForWallOverflow();
    }

    public void RemoveWall(RootShieldAttackWall receivedWall)
    {
        activeWalls.Remove(receivedWall);
        receivedWall.SelfDestruct();
    }
}
