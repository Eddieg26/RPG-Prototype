using UnityEngine;

[System.Serializable]
public class DamageInfo {
    [SerializeField] private Entity owner;
    [SerializeField] private int damage;
    [SerializeField] private StatusEffect statusEffect;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private AudioClip hitClip;

    public Entity Owner { get { return owner; } }
    public int Damage { get { return damage; } }
    public StatusEffect StatusEffect { get { return statusEffect; } }
    public AudioClip HitClip {get { return hitClip; } }
    public GameObject HitEffect { get { return hitEffect; } }

    public DamageInfo() { }

    public DamageInfo(Entity owner, int damage, GameObject hitEFfect = null, AudioClip hitClip = null, StatusEffect statusEffect = null) {
        this.owner = owner;
        this.damage = damage;
        this.statusEffect = statusEffect;
        this.hitClip = hitClip;
        this.hitEffect = hitEFfect;
    }
}
