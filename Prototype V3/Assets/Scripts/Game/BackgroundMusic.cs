using UnityEngine;
using System.Collections.Generic;

public class BackgroundMusic : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip initialClip;
    [SerializeField] private AudioSettings audioSettings;
    [SerializeField] private GameEvent changeBGMusicEvent;
    [SerializeField] private List<Music> musicList;

    private float musicVolume;
    private Music queuedMusic;
    private FadeState fadeState;
    private GameEventListener<MusicType> changeBGMusicListener;

    private void Start() {
        audioSettings.MusicVolumeChanged += MusicVolumeChanged;
        audioSettings.ToggleMute += ToggleMute;

        changeBGMusicListener = new GameEventListener<MusicType>(PlayMusic);
        changeBGMusicEvent.AddListener(changeBGMusicListener);

        SetMusicVolume();

        PlayMusic(initialClip);
    }

    private void Update() {
        if(fadeState == FadeState.FadeOut && queuedMusic != null) {
            if(audioSource.volume > 0f) {
                audioSource.volume -= Time.deltaTime * 0.3f;
                if(IsFloatEqual(audioSource.volume, 0f, 0.001f)) {
                    audioSource.volume = 0f;
                    audioSource.clip = queuedMusic.Clip;
                    audioSource.loop = queuedMusic.Loop;
                    audioSource.Play();
                    fadeState = FadeState.FadeIn;
                }
            }
        }

        if(fadeState == FadeState.FadeIn && queuedMusic != null) {
            if(audioSource.volume < musicVolume) {
                audioSource.volume += Time.deltaTime * 0.2f;
                if(IsFloatEqual(audioSource.volume, musicVolume, 0.001f)) {
                    audioSource.volume = musicVolume;
                    fadeState = FadeState.None;
                    queuedMusic = null;
                }
            }
        }
    }

    private void PlayMusic(AudioClip clip) {
        float volume = musicVolume;

        audioSource.volume = volume;
        audioSource.clip = initialClip;
        audioSource.Play();
    }

    private void PlayMusic(MusicType musicType) {
        Music music = musicList.Find(m => m.Type == musicType);
        if(music != null && queuedMusic != music) {
            queuedMusic = music;
            fadeState = FadeState.FadeOut;
        }
    }

    private void MusicVolumeChanged(float volume) {
        audioSource.volume = volume;
        musicVolume = volume;
    }

    private void ToggleMute(bool isMuted) {
        audioSource.volume = isMuted ? 0f : audioSettings != null ? audioSettings.GetMusicVolume() : 1f;
    }

    private void SetMusicVolume() {
        musicVolume = audioSettings != null ? !audioSettings.IsMuted ? audioSettings.GetMusicVolume() : 0f : 1f;
    }

    private void OnDestroy() {
        changeBGMusicEvent.RemoveListener(changeBGMusicListener);
    }

    private static bool IsFloatEqual(float valueA, float valueB, float threshold) {
        return Mathf.Abs(valueA - valueB) < threshold;
    }
}

public enum FadeState {
    None,
    FadeIn,
    FadeOut
}
