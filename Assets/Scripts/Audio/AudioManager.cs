using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private Sound[] musicSounds, sfxSounds;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource musicSource, sfxSource;

    private string _lastSong;
    private float _musicVolume;
    private float _SFXVolume;
    private float _globalVolume;
    private const string MixerMusic = "MusicVolume";
    private const string MixerSFX = "SfxVolume";
    private const string MixerMaster = "MasterVolume";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void PlayMusic(string musicName)
    {
        Sound sound = Array.Find(musicSounds, x => x.soundName == musicName);
        if (sound == null)
        {
            Debug.LogError("Sound not found");
        }
        else
        {
            if (musicName == _lastSong) return;
            _lastSong = sound.soundName;
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }
    public void PlayEffect(string effectName)
    {
        Sound effect = Array.Find(sfxSounds, x => x.soundName == effectName);
        if (effect == null)
        {
            Debug.LogError("Effect not found");
        }
        else
        {
            sfxSource.PlayOneShot(effect.clip);
        }
    }
    public void MusicVolume(float volume)
    {
        _musicVolume = volume;
        audioMixer.SetFloat(MixerMusic, Mathf.Log10(volume) * 20);
    }
    public void SfxVolume(float volume)
    {
        _SFXVolume = volume;
        audioMixer.SetFloat(MixerSFX, Mathf.Log10(volume) * 20);
    }
    
    public void GlobalVolume(float volume)
    {
        _globalVolume = volume;
        audioMixer.SetFloat(MixerMaster, Mathf.Log10(volume) * 20);
    }

    public float GetMusicVolume()
    {
        return _musicVolume;
    }
    public float GetSFXVolume()
    {
        return _SFXVolume;
    }
    
    public float GetGlobalVolume()
    {
        return _globalVolume;
    }
    public void StopMusic()
    {
        _lastSong = "";
        musicSource.Stop();
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    
}

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;
}