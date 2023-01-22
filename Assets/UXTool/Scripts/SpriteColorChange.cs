using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteColorChange
{
    public Color changedSpriteColor;
    public SpriteRenderer spriteRef;
    public float spriteColorChangeDuration;
    private SpriteColorChanger spriteColorChanger;


    public void SpriteColorChangeStartup()
    {
        if (spriteRef != null)
        {
            if (spriteColorChanger == null)
            {
                if (spriteRef.gameObject.GetComponent<SpriteColorChanger>()) spriteColorChanger = spriteRef.gameObject.GetComponent<SpriteColorChanger>();
                else spriteColorChanger = spriteRef.gameObject.AddComponent<SpriteColorChanger>();
            }
            spriteColorChanger.Startup(spriteRef);
        }
    }

    public void StartColorChange()
    {
        if (spriteColorChanger != null) spriteColorChanger.StartColorChange(spriteColorChangeDuration, changedSpriteColor);
    }
}
