using UnityEngine;

[CreateAssetMenu(fileName = "New BurnEffect", menuName = "Game/StatusEffects/Burn")]
public class BurnEffect : StatusEffect {

    public override void Enter(Entity entity) {}

    public override void Execute(Entity entity) {
        entity.OnTakePureDamage(StatConstants.BURN_DAMAGE);
    }

    public override void Exit(Entity entity) {
        entity.Stats.Buildup.Fire = 0;
    }

    public override bool CanEffect(Entity entity) {
        float resistance = Mathf.Abs((StatConstants.MOD - entity.Stats.Resistances.Fire) / StatConstants.MOD);
        entity.Stats.Buildup.Fire += Mathf.RoundToInt(Buildup * resistance);
        return entity.Stats.Buildup.Fire >= StatConstants.MAX_BUILDUP && entity.Stats.Resistances.Fire < StatConstants.MAX_RESISTANCE;
    }

    public override bool IsEqual(StatusEffect effect){
        BurnEffect burnEffect = (BurnEffect)effect;
        return burnEffect != null;
    }
}
