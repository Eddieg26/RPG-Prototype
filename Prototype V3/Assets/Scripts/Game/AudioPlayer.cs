using UnityEngine;

public class AudioPlayer : MonoBehaviour {
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSettings audioSettings;
    [SerializeField] private bool playOnAwake;

    private void Awake() {
        if (playOnAwake)
            PlayClip();
    }

    public void PlayClip() {
        audioSource.volume = audioSettings != null ? !audioSettings.IsMuted ? audioSettings.GetSfxVolume() : 0f : 1f;
        audioSource.spatialBlend = 1f;
        audioSource.Play();
    }

    public void PlayClip(AudioClip clip) {
        audioSource.volume = audioSettings != null ? !audioSettings.IsMuted ? audioSettings.GetSfxVolume() : 0f : 1f;
        audioSource.spatialBlend = 1f;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayOneShot(AudioClip clip) {
        audioSource.volume = audioSettings != null ? !audioSettings.IsMuted ? audioSettings.GetSfxVolume() : 0f : 1f;
        audioSource.spatialBlend = 1f;
        audioSource.PlayOneShot(clip);
    }
}
