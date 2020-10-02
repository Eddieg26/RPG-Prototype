using UnityEngine;
using System.Collections.Generic;

public class EntityStatus : MonoBehaviour {
    [SerializeField] private Entity self;
    [SerializeField] private GameEvent killEntityEvent;
    [SerializeField] private Transform statusEffectSpawnHolder;
    [SerializeField] private bool godMode = false;
    
    private List<StatusEffectRef> statusEffects = new List<StatusEffectRef>();

    private void Start() {
        self.AddStatusEffect += AddStatusEffect;
        self.TakeDamage += TakeDamage;
        self.EntityInit += InitEntity;
        self.Die += Die;
    }

    private void InitEntity() {
        self.Stats.Health = self.Stats.GetMaxHealth();
        self.Stats.Mana = self.Stats.GetMaxMana();

        RemoveStatusEffects();

        self.OnUpdateHealth();
        self.OnUpdateMana();
        self.OnUpdateStats();
        self.OnUpdateStatusEffects(new StatusEffectRefList(statusEffects));
    }

    private void TakeDamage(DamageInfo damageInfo) {
        if (!godMode) {
            int damage = EntityDamageCalculator.CalculateDamage(damageInfo.Damage, self.Stats.GetDefense());
            self.Stats.Health -= damage;
        }

        if (damageInfo.HitEffect != null) {
            Vector3 position = statusEffectSpawnHolder.position + Random.insideUnitSphere;
            Instantiate(damageInfo.HitEffect, position, transform.rotation);
        }
    }

    public void AddStatusEffect(StatusEffect statusEffect) {
        StatusEffectRef foundEffect = statusEffects.Find((effectRef) => { return effectRef.Effect.IsEqual(statusEffect); });
        if (foundEffect == null) {
            StatusEffectRef effectRef = new StatusEffectRef(statusEffect);
            if (effectRef.CanEffect(self)) {
                effectRef.Enter(self);
                statusEffects.Add(effectRef);
                self.OnUpdateStatusEffects(new StatusEffectRefList(statusEffects));

                StatusEffectObject effectObject = Instantiate(statusEffect.EffectObject, statusEffectSpawnHolder.position, statusEffectSpawnHolder.rotation, statusEffectSpawnHolder);
                effectRef.EffectObject = effectObject;
            }
        }
    }

    private void Update() {
        List<StatusEffectRef> effectsToRemove = new List<StatusEffectRef>();
        statusEffects.ForEach(effectRef => {
            if (effectRef.Update(self))
                effectsToRemove.Add(effectRef);
        });

        effectsToRemove.ForEach(effectRef => {
            effectRef.Exit(self);
            statusEffects.Remove(effectRef);
            self.OnRemoveStatusEffect(effectRef.Effect);
            effectRef.EffectObject.Destroy();
        });

        self.OnUpdateStatusEffects(new StatusEffectRefList(statusEffects));
    }

    private void RemoveStatusEffects() {
        statusEffects.ForEach(effectRef => {
            effectRef.EffectObject.Destroy();
        });

        statusEffects.Clear();
    }

    private void Die(Entity entity) {
        if (killEntityEvent != null)
            killEntityEvent.Invoke(new EntityKillData(self.ID));
    }
}
