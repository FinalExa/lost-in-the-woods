using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdaptiveTextTrigger : MonoBehaviour
{
    private bool playerIsInside;
    private bool currentlyActive;
    private PCStateMachine pcStateMachine;
    private SphereCollider thisCollider;
    [SerializeField] private TMP_Text textRef;
    [SerializeField] private bool needsStates;
    private enum AdaptiveTextPlayerStates { PCAttack, PCDodge, PCEnterLanternUp, PCExitLanternUp, PCIdle, PCIdleGrab, PCIdleLanternUp, PCMoving, PCMovingGrab, PCMovingLanternUp }
    [SerializeField] private AdaptiveTextPlayerStates[] adaptiveTextPlayerStates;

    private void Awake()
    {
        PCStateMachine.onPlayerStateChange += CheckForState;
        thisCollider = this.gameObject.GetComponent<SphereCollider>();
        pcStateMachine = FindObjectOfType<PCStateMachine>();
    }

    private void OnEnable()
    {
        DectivateText();
    }

    private void Update()
    {
        CheckForReactivation();
        CheckForPlayerDodge();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsInside = true;
            if (CheckIfStateMatches(pcStateMachine.thisStateName)) ActivateText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsInside = false;
            DectivateText();
        }
    }

    private void CheckForReactivation()
    {
        if (playerIsInside && !currentlyActive && CheckIfStateMatches(pcStateMachine.thisStateName)) ActivateText();
    }

    private void ActivateText()
    {
        currentlyActive = true;
        if (!textRef.gameObject.activeSelf) textRef.gameObject.SetActive(true);
    }

    private void DectivateText()
    {
        currentlyActive = false;
        if (textRef.gameObject.activeSelf) textRef.gameObject.SetActive(false);
    }

    private void CheckForState(string receivedState)
    {
        if (currentlyActive && needsStates)
        {
            if (!CheckIfStateMatches(receivedState)) DectivateText();
        }
    }

    private bool CheckIfStateMatches(string receivedState)
    {
        bool match = false;
        if (needsStates)
        {
            foreach (AdaptiveTextPlayerStates adaptiveTextPlayerState in adaptiveTextPlayerStates)
            {
                if (adaptiveTextPlayerState.ToString() == receivedState)
                {
                    match = true;
                    break;
                }
            }
        }
        else match = true;
        return match;
    }

    private void CheckForPlayerDodge()
    {
        if (playerIsInside && pcStateMachine.thisStateName == AdaptiveTextPlayerStates.PCDodge.ToString())
        {
            if (!CheckForPlayerInside())
            {
                playerIsInside = false;
                DectivateText();
            }
        }
        else if (!playerIsInside && pcStateMachine.thisStateName == AdaptiveTextPlayerStates.PCDodge.ToString())
        {
            if (!needsStates || (needsStates && CheckIfStateMatches(pcStateMachine.thisStateName)))
            {
                if (CheckForPlayerInside())
                {
                    playerIsInside = true;
                    ActivateText();
                }
            }
        }
    }

    private bool CheckForPlayerInside()
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, thisCollider.radius);
        bool check = false;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.name == pcStateMachine.gameObject.name)
            {
                check = true;
                break;
            }
        }
        return check;
    }
}
