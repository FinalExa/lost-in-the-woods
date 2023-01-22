using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorChanger : MonoBehaviour
{
    private SpriteRenderer spriteRef;
    private Color spriteRefBaseColor;

    public void Startup(SpriteRenderer reference)
    {
        spriteRef = reference;
        spriteRefBaseColor = spriteRef.color;
    }

    public void StartColorChange(float duration, Color changedSpriteColor)
    {
        StartCoroutine(LaunchSpriteColorChange(duration, changedSpriteColor));
    }

    private void SetSpriteColor(Color changedSpriteColor)
    {
        spriteRef.color = changedSpriteColor;
    }

    private void ResetSpriteColor()
    {
        spriteRef.color = spriteRefBaseColor;
    }

    private IEnumerator LaunchSpriteColorChange(float duration, Color changedColor)
    {
        SetSpriteColor(changedColor);
        yield return new WaitForSeconds(duration);
        ResetSpriteColor();
    }
}
