using UnityEngine;

[CreateAssetMenu(fileName = "New StatModifierEffect", menuName = "Game/StatusEffects/StatModifier")]
public class StatModifierEffect : StatusEffect {
    [SerializeField] private StatModifier modifier;

    public StatModifier Modifier { get { return modifier; } }

    public override void Enter(Entity entity) {
        StatModifierUtil.ApplyStatModifier(entity, modifier);
    }

    public override void Execute(Entity entitiy) { }

    public override void Exit(Entity entity) {
        StatModifierUtil.RemoveStatModifier(entity, modifier);
    }

    public override bool CanEffect(Entity entity) {
        return StatModifierUtil.CanApplyStatModifier(entity, modifier);
    }

    public override bool IsEqual(StatusEffect effect) { return false; }
}
