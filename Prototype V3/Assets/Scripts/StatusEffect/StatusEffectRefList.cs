using System.Collections.Generic;

public class StatusEffectRefList {
    private List<StatusEffectRef> statusEffects;

    public int Count { get { return statusEffects.Count; } }

    public StatusEffectRefList(List<StatusEffectRef> effects) {
        this.statusEffects = effects;
    }

    public StatusEffectRef GetStatusEffect(int index) {
        return statusEffects[index];
    }
}
