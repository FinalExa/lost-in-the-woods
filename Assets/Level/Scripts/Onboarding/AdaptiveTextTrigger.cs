using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdaptiveTextTrigger : MonoBehaviour
{
    [SerializeField] private string textToDisplay;
    [SerializeField] private TMP_Text textRef;
    private void Start()
    {
        textRef.text = textToDisplay;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) ActivateText();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) DectivateText();
    }

    private void ActivateText()
    {
        if (!textRef.gameObject.activeSelf) textRef.gameObject.SetActive(true);
    }

    private void DectivateText()
    {
        if (textRef.gameObject.activeSelf) textRef.gameObject.SetActive(false);
    }
}
