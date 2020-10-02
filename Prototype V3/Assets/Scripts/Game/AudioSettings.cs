using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AudioSettings", menuName = "Game/AudioSettings")]
public class AudioSettings : ScriptableObject {
    [SerializeField, Range(0, 100)] private int musicVolume = 100;
    [SerializeField, Range(0, 100)] private int sfxVolume = 100;
    [SerializeField] private bool isMuted;

    public int MusicVolume {
        get { return musicVolume; }
        set {
            musicVolume = value;
            OnMusicVolumeChanged();
        }
    }

    public int SfxVolume {
        get { return sfxVolume; }
        set {
            sfxVolume = value;
            OnSfxVolumeChanged();
        }
    }

    public bool IsMuted {
        get { return isMuted; }
        set {
            isMuted = value;
            OnToggleMute();
        }
    }

    public UnityAction<float> MusicVolumeChanged { get; set; }
    public UnityAction<float> SfxVolumeChanged { get; set; }
    public UnityAction<bool> ToggleMute { get; set; }

    public float GetMusicVolume() {
        return musicVolume / 100f;
    }

    public float GetSfxVolume() {
        return sfxVolume / 100f;
    }

    private void OnMusicVolumeChanged() {
        if (MusicVolumeChanged != null)
            MusicVolumeChanged(GetMusicVolume());
    }

    private void OnSfxVolumeChanged() {
        if (SfxVolumeChanged != null)
            SfxVolumeChanged(GetSfxVolume());
    }

    private void OnToggleMute() {
        if (ToggleMute != null)
            ToggleMute(isMuted);
    }
}
