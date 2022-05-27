using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class EnemyBT : BT_Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;

    protected override Node SetupTree()
    {
        Node root = new TaskPatrol(transform, waypoints);
        return root;
    }
}
