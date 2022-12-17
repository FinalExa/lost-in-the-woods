using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class UXEffect
{
    public bool hasSound;
    public GameObject soundParent;
    public Sound sound;
    public bool hasSpriteColorChange;
    public SpriteColorChange spriteColorChange;
    public bool hasCameraShake;

    public void UXEffectStartup()
    {
        if (hasSound) CreateAudioSource();
        if (hasSpriteColorChange) spriteColorChange.GetSpriteBaseColor();
    }

    public void CreateAudioSource()
    {
        if (soundParent != null) sound.SetSource(soundParent.AddComponent<AudioSource>());
    }
}
