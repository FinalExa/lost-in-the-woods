using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WitchCrying
{
    private WitchEnemyController witchEnemyController;
    public bool witchIsCrying;
    [SerializeField] private float witchCryingDuration;
    [SerializeField] private GameObject witchCryingSprite;
    private float witchCryingTimer;
    public void SetController(WitchEnemyController controller)
    {
        witchEnemyController = controller;
    }
    public void WitchCryingAction()
    {
        if (witchIsCrying)
        {
            if (witchCryingTimer > 0) witchCryingTimer -= Time.deltaTime;
            else WitchStopCryingAction();
        }
    }

    public void WitchStartCryingAction()
    {
        witchIsCrying = true;
        witchCryingSprite.SetActive(true);
        witchCryingTimer = witchCryingDuration;
    }

    public void WitchStopCryingAction()
    {
        witchCryingSprite.SetActive(false);
        witchIsCrying = false;
    }

    public bool GetIfWitchIsCrying()
    {
        return witchIsCrying;
    }
}
