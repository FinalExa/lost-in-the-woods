using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CameraShake
{
    public float shakeAmplitude;
    public float shakeFrequency;
    public float shakeDuration;
    [HideInInspector] public CinemachineCameraShaker cinemachineCameraShakerRef;

    public void StartCameraShake()
    {
        cinemachineCameraShakerRef.ShakeCamera(shakeDuration, shakeAmplitude, shakeFrequency);
    }
}
