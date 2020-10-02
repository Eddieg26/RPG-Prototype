using UnityEngine;
using UnityEngine.UI;
using UIElements;

public class SettingsViewUI : MonoBehaviour, IViewUI {
    [SerializeField] private GameObject view;
    [SerializeField] private VolumeView musicVolumeView;
    [SerializeField] private VolumeView sfxVolumeView;
    [SerializeField] private Toggle muteToggle;
    [SerializeField] private AudioSettings audioSettings;
    [SerializeField] private GameEvent registerViewEvent;

    private void Start() {
        registerViewEvent.Invoke(new RegisterViewData(this, UIConstants.SETTINGS_VIEW_INDEX));
    }

    public void Open() {
        view.SetActive(true);
        SetSettings();
    }

    public void Close() {
        view.SetActive(false);
    }

    public string GetTitle() {
        return "Settings";
    }

    public void SetMusicVolume() {
        audioSettings.MusicVolume = Mathf.RoundToInt(musicVolumeView.VolumeSlider.normalizedValue * 100);
        musicVolumeView.SetVolumeText("Music", audioSettings.MusicVolume);
    }

    public void SetSfxVolume() {
        audioSettings.SfxVolume = Mathf.RoundToInt(sfxVolumeView.VolumeSlider.normalizedValue * 100);
        sfxVolumeView.SetVolumeText("Sfx", audioSettings.SfxVolume);
    }

    public void SetIsMuted() {
        audioSettings.IsMuted = muteToggle.isOn;
    }

    private void SetSettings() {
        musicVolumeView.VolumeSlider.value = audioSettings.MusicVolume;
        sfxVolumeView.VolumeSlider.value = audioSettings.SfxVolume;
        muteToggle.isOn = audioSettings.IsMuted;
    }
}
