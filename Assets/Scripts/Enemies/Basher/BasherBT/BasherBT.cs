using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class BasherBT : BT_Tree
{
    [HideInInspector] public BasherController basherController;

    private void Awake()
    {
        basherController = this.gameObject.GetComponent<BasherController>();
    }

    protected override Node SetupTree()
    {

        Node root = new Sequence(new List<Node>
        {
            new Selector(new List<Node>
            {
                new TaskIsCloseToPlayer(basherController,this.gameObject,basherController.basherReferences.basherNavMesh, basherController.basherReferences.playerRef, basherController.basherReferences.basherData.distanceFromPlayer),
                new TaskGetCloseToPlayer(basherController.basherReferences.basherNavMesh, basherController.basherReferences.playerRef),
            }),
            new Selector(new List<Node>
            {
                new TaskBasherAttack(basherController)
            })
        });
        return root;
    }
}
