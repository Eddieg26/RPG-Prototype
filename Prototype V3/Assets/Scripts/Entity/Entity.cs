using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour {
    [SerializeField] private EntityID id;
    [SerializeField] private EntityStats stats;

    public EntityID ID { get { return id; } }
    public EntityStats Stats { get { return stats; } }
    public bool IsAlive { get { return stats.Health > 0; } }

    public UnityAction EntityInit { get; set; }
    public UnityAction<int, int> UpdateHealth { get; set; }
    public UnityAction<int, int> UpdateMana { get; set; }
    public UnityAction<EntityStats> UpdateStats { get; set; }
    public UnityAction<int> RecoverHealth { get; set; }
    public UnityAction<int> RecoverMana { get; set; }
    public UnityAction<DamageInfo> TakeDamage { get; set; }
    public UnityAction<int> TakePureDamage { get; set; }
    public UnityAction<bool> SetFocus { get; set; }
    public UnityAction<StatusEffect> AddStatusEffect { get; set; }
    public UnityAction<StatusEffect> RemoveStatusEffect { get; set; }
    public UnityAction<StatusEffectRefList> UpdateStatusEffects { get; set; }
    public UnityAction<Entity> Die { get; set; }

    public void OnEntityInit() {
        if(EntityInit != null)
            EntityInit();
    }

    public void OnUpdateHealth() {
        if (UpdateHealth != null)
            UpdateHealth(stats.Health, stats.GetMaxHealth());
    }

    public void OnUpdateMana() {
        if (UpdateMana != null)
            UpdateMana(stats.Mana, stats.GetMaxMana());
    }

    public void OnUpdateStats() {
        if (UpdateStats != null)
            UpdateStats(stats);
    }

    public void OnRecoverHealth(int recoverAmount) {
        if (RecoverHealth != null)
            RecoverHealth(recoverAmount);

        OnUpdateHealth();
    }

    public void OnRecoverMana(int recoverAmount) {
        if (RecoverMana != null)
            RecoverMana(recoverAmount);

        OnUpdateMana();
    }

    public void OnTakeDamage(DamageInfo damageInfo) {
        if (TakeDamage != null)
            TakeDamage(damageInfo);

        OnUpdateHealth();

        if (!IsAlive)
            OnDie();
    }

    public void OnUseMana(int amount) {
        stats.Mana -= amount;
        if (UpdateMana != null)
            OnUpdateMana();
    }

    public void OnTakePureDamage(int damage) {
        stats.Health -= damage;

        if (TakePureDamage != null)
            TakePureDamage(damage);

        OnUpdateHealth();

        if (!IsAlive)
            OnDie();
    }

    public void OnSetFocus(bool focus) {
        if (SetFocus != null)
            SetFocus(focus);
    }

    public void OnAddStatusEffect(StatusEffect effect) {
        if (AddStatusEffect != null)
            AddStatusEffect(effect);
    }

    public void OnRemoveStatusEffect(StatusEffect effect) {
        if (RemoveStatusEffect != null)
            RemoveStatusEffect(effect);
    }

    public void OnUpdateStatusEffects(StatusEffectRefList statusEffects) {
        if (UpdateStatusEffects != null)
            UpdateStatusEffects(statusEffects);
    }

    private void OnDie() {
        if (Die != null)
            Die(this);
    }
}
