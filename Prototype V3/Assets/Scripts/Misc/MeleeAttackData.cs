using UnityEngine;

[System.Serializable]
public class MeleeAttackData {
    [SerializeField] private float attackRange;
    [SerializeField] private float directionThreshold;
    [SerializeField] private string animatorTrigger;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private GameObject hitEffect;

    public float AttackRange { get { return attackRange; } }
    public float DirectionThreshold { get { return directionThreshold; } }
    public string AnimatorTrigger { get { return animatorTrigger; } }
    public AudioClip HitClip { get { return hitClip; } }
    public GameObject HitEffect { get { return hitEffect; } }
}
