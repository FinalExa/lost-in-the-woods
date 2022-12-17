using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;

public class CinemachineCameraShaker : MonoBehaviour
{
    public float IdleAmplitude = 0f;
    public float IdleFrequency = 1f;

    public float DefaultShakeAmplitude = .5f;
    public float DefaultShakeFrequency = 10f;

    protected Vector3 _initialPosition;
    protected Quaternion _initialRotation;

    protected CinemachineBasicMultiChannelPerlin _perlin;
    protected CinemachineVirtualCamera _virtualCamera;

    protected virtual void Awake()
    {
        _virtualCamera = this.gameObject.GetComponent<CinemachineVirtualCamera>();
        _perlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    protected virtual void Start()
    {
        CameraReset();
    }

    public virtual void ShakeCamera(float duration)
    {
        StartCoroutine(ShakeCameraCo(duration, DefaultShakeAmplitude, DefaultShakeFrequency));
    }

    public virtual void ShakeCamera(float duration, float amplitude, float frequency)
    {
        StartCoroutine(ShakeCameraCo(duration, amplitude, frequency));
    }

    protected virtual IEnumerator ShakeCameraCo(float duration, float amplitude, float frequency)
    {
        _perlin.m_AmplitudeGain = amplitude;
        _perlin.m_FrequencyGain = frequency;
        yield return new WaitForSeconds(duration);
        CameraReset();
    }

    public virtual void CameraReset()
    {
        _perlin.m_AmplitudeGain = IdleAmplitude;
        _perlin.m_FrequencyGain = IdleFrequency;
    }
}
