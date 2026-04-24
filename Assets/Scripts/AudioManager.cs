using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;


public enum Sound { Click }
public enum Music { Main }

[Serializable]
public class SoundData
{
    public Sound sound;
    public AudioClip audioClip;
}

[Serializable]
public class MusicData
{
    public Music music;
    public AudioClip audioClip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Data")]
    [SerializeField] SoundData[] soundEffects;
    [SerializeField] MusicData[] musics;

    [Space(5), Header("Audio Sources")]
    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioSource musicAudioSource;

    [Space(5), Header("Mixer")]
    [SerializeField] AudioMixer mixer;

    private readonly Dictionary<Sound, AudioClip> soundDict = new();
    private readonly Dictionary<Music, AudioClip> musicDict = new();

    public bool IsMusicMuted { get; private set; }
    public bool IsSFXMuted { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        foreach (var sound in soundEffects)
            soundDict.Add(sound.sound, sound.audioClip);

        foreach (var music in musics)
            musicDict.Add(music.music, music.audioClip);
    }

    private void Start()
    {
        LoadVolumeSettings();
    }

    private void LoadVolumeSettings()
    {
        SetMusicVolume(PlayerPrefs.GetInt("music_volume", 1));
        SetSFXVolume(PlayerPrefs.GetInt("sfx_volume", 1));
    }

    public void PlaySfx(Sound sound)
    {
        if (!sfxAudioSource)
            return;

        if (soundDict.TryGetValue(sound, out AudioClip clip))
            sfxAudioSource.PlayOneShot(clip);
    }

    public void PlayMusic(Music music)
    {
        if (!musicAudioSource)
            return;

        if (musicDict.TryGetValue(music, out AudioClip clip))
        {
            StopMusic();

            musicAudioSource.clip = clip;
            musicAudioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicAudioSource && musicAudioSource.isPlaying)
            musicAudioSource.Stop();
    }

    public void SetSFXVolume(float volume)
    {
        IsSFXMuted = volume <= 0;
        float volumeInDecibels = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        mixer.SetFloat("SFXVolume", volumeInDecibels);
    }

    public void SetMusicVolume(float volume)
    {
        IsMusicMuted = volume <= 0;
        float volumeInDecibels = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        mixer.SetFloat("MusicVolume", volumeInDecibels);
    }

    public void ToggleMuteSFX(bool isMuted)
    {
        int volume = isMuted ? 0 : 1;
        SetSFXVolume(volume);
        PlayerPrefs.SetInt("sfx_volume", volume);
    }

    public void ToggleMuteMusic(bool isMuted)
    {
        int volume = isMuted ? 0 : 1;
        SetMusicVolume(volume);
        PlayerPrefs.SetInt("music_volume", volume);
    }
}