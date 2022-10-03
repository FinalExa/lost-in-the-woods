using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TestEnemyBT : BT_Tree
{
    [HideInInspector] public TestEnemyController testEnemyController;

    private void Awake()
    {
        testEnemyController = this.gameObject.GetComponent<TestEnemyController>();
    }

    protected override Node SetupTree()
    {

        Node root = new Sequence(new List<Node>
        {
            new Selector(new List<Node>
            {
                new TaskIsCloseToPlayer(testEnemyController,this.gameObject,testEnemyController.testEnemyReferences.basherNavMesh, testEnemyController.testEnemyReferences.playerRef, testEnemyController.testEnemyReferences.basherData.distanceFromPlayer),
                new TaskGetCloseToPlayer(testEnemyController.testEnemyReferences.basherNavMesh, testEnemyController.testEnemyReferences.playerRef),
            }),
            new Selector(new List<Node>
            {
                new TaskEnemyAttack(testEnemyController)
            })
        });
        return root;
    }
}
