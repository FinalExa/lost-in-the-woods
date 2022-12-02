using System;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    private AudioSource source;
    public string clipName;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 2f)]
    public float pitch;
    public bool loop;
    public bool playOnAwake;
    public AudioMixerGroup mixer;


    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = this.clip;
        source.volume = this.volume;
        source.pitch = this.pitch;
        source.loop = this.loop;
        source.playOnAwake = this.playOnAwake;
        source.outputAudioMixerGroup = this.mixer;
    }

    public void PlayAudio()
    {
        source.Play();
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