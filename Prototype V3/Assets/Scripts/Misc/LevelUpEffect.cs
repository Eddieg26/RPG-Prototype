using UnityEngine;
using UnityEngine.UI;

public class LevelUpEffect : MonoBehaviour {
    [SerializeField] private AudioPlayer audioPlayer;
    [SerializeField] private ParticleSystem mainParticles;
    [SerializeField] private Text levelUpText;
    [SerializeField] private float duration;

    private bool active;
    private float timer;

    private void Update() {
        if(active && Time.time > timer + duration)
            Deactivate();
    }

    public void Activate() {
        mainParticles.gameObject.SetActive(true);
        mainParticles.Play(true);
        levelUpText.gameObject.SetActive(true);
        timer = Time.time;
        audioPlayer.PlayClip();
        active = true;
    }

    public void Deactivate() {
        mainParticles.Stop();
        mainParticles.gameObject.SetActive(false);
        levelUpText.gameObject.SetActive(false);
        active = false;
    }
}
