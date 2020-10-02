using UnityEngine;
using UnityEngine.Audio;

public class EntityAudioPlayer : MonoBehaviour {
    [SerializeField] private Entity self;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSettings audioSettings;

    private void Start() {
        self.TakeDamage += PlayHitClip;
    }

    public void PlayClip(AudioClip clip) {
        float volume = audioSettings ? audioSettings.GetSfxVolume() : 1f;
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void SpawnAudioClip(AudioClip clip) {
        float volume = audioSettings ? audioSettings.GetSfxVolume() : 1f;
        AudioSource.PlayClipAtPoint(clip, transform.position, volume);
    }

    private void PlayHitClip(DamageInfo damageInfo) {
        if(damageInfo.HitClip != null)
            SpawnAudioClip(damageInfo.HitClip);
    }
}
