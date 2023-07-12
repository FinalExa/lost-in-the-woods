using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WitchHidden
{
    private WitchEnemyController witchEnemyController;

    private PCGrabbing pcGrabbing;
    private Rigidbody pcRigidbody;
    private bool witchIsHidden;
    [HideInInspector] public List<Sprite> possibleHiddenSprites;
    [SerializeField] private float playerIdleDuration;
    private float playerIdleTimer;
    public SpriteRenderer witchSpriteRenderer;
    public SpriteRenderer coverSpriteRenderer;

    public void SetController(WitchEnemyController controller)
    {
        witchEnemyController = controller;
        pcGrabbing = witchEnemyController.playerRef.GetComponent<PCGrabbing>();
        pcRigidbody = witchEnemyController.playerRef.GetComponent<Rigidbody>();
    }

    public void UpdateWitchHiddenSpriteList(List<Sprite> newSprites)
    {
        possibleHiddenSprites = new List<Sprite>();
        possibleHiddenSprites = newSprites;
    }

    public void SetWitchHidden()
    {
        witchIsHidden = true;
        witchSpriteRenderer.enabled = true;
        coverSpriteRenderer.enabled = false;
        ResetPlayerIdleTimer();
        if (possibleHiddenSprites.Count > 0)
        {
            Sprite chosenSprite = possibleHiddenSprites[Random.Range(0, possibleHiddenSprites.Count - 1)];
            coverSpriteRenderer.sprite = chosenSprite;
            witchSpriteRenderer.enabled = false;
            coverSpriteRenderer.enabled = true;
        }
    }

    public void LookAtPlayer()
    {
        if (witchEnemyController.isAlerted && witchIsHidden)
        {
            if (pcGrabbing.GrabbedObjectExists()) LeaveHiddenState();
            PlayerIdleTimer();
        }

    }

    private void PlayerIdleTimer()
    {
        if (pcRigidbody.velocity == Vector3.zero)
        {
            if (playerIdleTimer > 0) playerIdleTimer -= Time.deltaTime;
            else LeaveHiddenState();
        }
        else ResetPlayerIdleTimer();
    }

    private void ResetPlayerIdleTimer()
    {
        playerIdleTimer = playerIdleDuration;
    }

    public void LeaveHiddenState()
    {
        if (witchIsHidden)
        {
            witchIsHidden = false;
            witchSpriteRenderer.enabled = true;
            coverSpriteRenderer.enabled = false;
        }
    }

    public bool GetIfWitchIsHidden()
    {
        return witchIsHidden;
    }
}
