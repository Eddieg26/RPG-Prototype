using UnityEngine;

public abstract class StatusEffect : ScriptableObject {
    [SerializeField] private string effectName;
    [SerializeField] private Sprite icon;
    [SerializeField] private int buildup;
    [SerializeField] private float duration;
    [SerializeField] private StatusEffectObject effectObject;

    public string Name { get { return effectName; } }
    public Sprite Icon { get { return icon; } }
    public int Buildup { get { return buildup; } }
    public float Duration { get { return duration; } }
    public StatusEffectObject EffectObject { get { return effectObject; } }

    public abstract void Enter(Entity entity);
    public abstract void Execute(Entity entitiy);
    public abstract void Exit(Entity entity);
    public abstract bool CanEffect(Entity entity);
    public abstract bool IsEqual(StatusEffect effect);
}