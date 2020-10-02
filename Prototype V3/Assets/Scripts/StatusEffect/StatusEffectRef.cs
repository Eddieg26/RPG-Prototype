using UnityEngine;

[System.Serializable]
public class StatusEffectRef {
    private StatusEffect effect;
    private float durationTimer;

    private float updateTimer;

    public float DurationTimer { get { return durationTimer; } }
    public StatusEffect Effect { get { return effect; } }
    public StatusEffectObject EffectObject { get; set; }

    public StatusEffectRef() { }

    public StatusEffectRef(StatusEffect effect) {
        this.effect = effect;
    }

    public void Enter(Entity entity) {
        if (effect != null && entity != null)
            effect.Enter(entity);

        durationTimer = Time.time;
        updateTimer = Time.time;
    }

    public bool Update(Entity entity) {
        if (effect != null && entity != null) {
            if (Time.time > updateTimer + 0.9f) {
                effect.Execute(entity);
                updateTimer = Time.time;
            }
        } else
            return true;

        return Time.time > durationTimer + effect.Duration;
    }

    public void Exit(Entity entity) {
        if (effect != null && entity != null)
            effect.Exit(entity);
    }

    public bool CanEffect(Entity entity) {
        return effect != null && entity != null ? effect.CanEffect(entity) : false;
    }
}
