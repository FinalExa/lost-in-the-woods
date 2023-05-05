using System;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    private AudioSource source;
    public AudioClip clip;
    [Range(0, 256)] public int priority = 128;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0f, 3f)] public float pitch = 1f;
    [Range(-1f, 1f)] public float stereoPan = 0f;
    [Range(0f, 1f)] public float spatialBlend = 0f;
    [Range(0f, 1.1f)] public float reverbZoneMix = 1f;
    public bool loop;
    public bool playOnAwake;
    public AudioMixerGroup mixer;


    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = this.clip;
        source.priority = this.priority;
        source.volume = this.volume;
        source.pitch = this.pitch;
        source.panStereo = this.stereoPan;
        source.spatialBlend = this.spatialBlend;
        source.reverbZoneMix = this.reverbZoneMix;
        source.loop = this.loop;
        source.playOnAwake = this.playOnAwake;
        source.outputAudioMixerGroup = this.mixer;
    }

    public void PlayAudio()
    {
        if (source != null) source.Play();
    }

    public void StopAudio()
    {
        source.Stop();
    }

    public void SetLoop(bool loop)
    {
        source.loop = loop;
    }

    public bool IsPlaying()
    {
        return source.isPlaying;
    }
}