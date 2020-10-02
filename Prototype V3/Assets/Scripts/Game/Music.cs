using UnityEngine;

[System.Serializable]
public class Music {
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private MusicType type;
    [SerializeField] private bool loop;

    public AudioClip Clip { get { return audioClip; } }
    public MusicType Type { get { return type; } }
    public bool Loop { get { return loop; } }
}
