using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteColorChange
{
    public Color onHitSpriteColor;
    public SpriteRenderer spriteRef;
    public float spriteColorChangeDuration;
    [HideInInspector] public bool spritehasChangedColor;
    [HideInInspector] public Color spriteRefBaseColor;

    public void GetSpriteBaseColor()
    {
        spriteRefBaseColor = spriteRef.color;
    }

    public void SetSpriteColor()
    {
        spriteRef.color = onHitSpriteColor;
        spritehasChangedColor = true;
    }

    public void ResetSpriteColor()
    {
        spriteRef.color = spriteRefBaseColor;
        spritehasChangedColor = false;
    }
}
