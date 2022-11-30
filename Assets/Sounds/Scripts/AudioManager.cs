using System;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private Sound[] sounds;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this.gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObj = new GameObject("Sound_" + i + sounds[i].clipName);
            soundObj.transform.SetParent(this.transform);
            sounds[i].SetSource(soundObj.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string name)
    {
        SearchSound(name).PlayAudio();
    }

    public void StopSound(string name)
    {
        SearchSound(name).StopAudio();
    }

    public void SetLoop(string name, bool loop)
    {
        SearchSound(name).SetLoop(loop);
    }

    public bool IsPlaying(string name)
    {
        return SearchSound(name).IsPlaying();
    }

    private Sound SearchSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clipName == name);
        if (s == null)
        {
            Debug.LogError("Sound: " + name + " NotFound");
            return null;
        }
        return s;
    }

    public void StopAllSounds()
    {
        foreach (Sound s in sounds) s.StopAudio();
    }

    public void StopAllSounds(params string[] exception)
    {
        foreach (Sound s in sounds)
        {
            for (int i = 0; i < exception.Length; i++)
            {
                if (s.clipName == exception[i]) continue;
                s.StopAudio();
            }
        }
    }
}

