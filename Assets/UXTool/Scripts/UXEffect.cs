using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class UXEffect
{
    public bool hasSound;
    public Sound sound;
    public bool hasSpriteColorChange;
    public SpriteColorChange spriteColorChange;
    public bool hasCameraShake;
    public CameraShake cameraShake;
    [HideInInspector] public GameObject soundParent;

    public void UXEffectStartup()
    {
        if (hasSound) GetAudioSource();
        if (hasSpriteColorChange) spriteColorChange.SpriteColorChangeStartup();
        if (hasCameraShake) GetCameraShaker();
    }

    private void GetAudioSource()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("SoundParent");
        if (parent != null) soundParent = parent;
        else soundParent = CreateSoundParent();
        sound.SetSource(soundParent.AddComponent<AudioSource>());
    }

    private GameObject CreateSoundParent()
    {
        GameObject parent = new GameObject();
        parent.name = "SoundParent";
        parent.tag = "SoundParent";
        return parent;
    }

    private void GetCameraShaker()
    {
        cameraShake.cinemachineCameraShakerRef = GameObject.FindObjectOfType<CinemachineCameraShaker>();
        if (cameraShake.cinemachineCameraShakerRef == null) hasCameraShake = false;
    }
}
