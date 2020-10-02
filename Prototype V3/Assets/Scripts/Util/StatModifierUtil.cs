using UnityEngine;

public static class StatModifierUtil {

    public static void ApplyStatModifier(Entity entity, StatModifier modifier) {
        switch (modifier.Type) {
            case StatType.Health:
                entity.Stats.Health += modifier.Value;
                entity.OnUpdateHealth();
                break;
            case StatType.Mana:
                entity.Stats.Mana += modifier.Value;
                entity.OnUpdateMana();
                break;
            // case StatType.BaseMaxHealth:
            //     entity.Stats.BaseMaxHealth += modifier.Value;
            //     break;
            // case StatType.BaseMaxMana:
            //     entity.Stats.BaseMaxMana += modifier.Value;
            //     break;
            // case StatType.BaseAttack:
            //     entity.Stats.BaseAttack += modifier.Value;
            //     break;
            // case StatType.BaseDefense:
            //     entity.Stats.BaseDefense += modifier.Value;
            //     break;
            // case StatType.BaseDexterity:
            //     entity.Stats.BaseDexterity += modifier.Value;
            //     break;
            case StatType.MaxHealthAug:
                entity.Stats.Augments.MaxHealth += modifier.Value;
                entity.OnUpdateHealth();
                break;
            case StatType.MaxManaAug:
                entity.Stats.Augments.MaxMana += modifier.Value;
                entity.OnUpdateMana();
                break;
            case StatType.AttackAug:
                entity.Stats.Augments.Attack += modifier.Value;
                break;
            case StatType.DefenseAug:
                entity.Stats.Augments.Defense += modifier.Value;
                break;
            case StatType.DexterityAug:
                entity.Stats.Augments.Dexterity += modifier.Value;
                break;
            case StatType.AttackMultAug:
                entity.Stats.Augments.AttackMult += modifier.Value;
                break;
            case StatType.DefenseMultAug:
                entity.Stats.Augments.DefenseMult += modifier.Value;
                break;
            case StatType.FireBuildup:
                entity.Stats.Buildup.Fire += modifier.Value;
                break;
            case StatType.PoisonBuildup:
                entity.Stats.Buildup.Poison += modifier.Value;
                break;
            case StatType.FireResistance:
                entity.Stats.Resistances.Fire += modifier.Value;
                break;
            case StatType.PoisonResistance:
                entity.Stats.Resistances.Poison += modifier.Value;
                break;
        }
    }

    public static void RemoveStatModifier(Entity entity, StatModifier modifier) {
        switch (modifier.Type) {
            case StatType.Health:
                entity.Stats.Health -= modifier.Value;
                entity.OnUpdateHealth();
                break;
            case StatType.Mana:
                entity.Stats.Mana -= modifier.Value;
                entity.OnUpdateMana();
                break;
            // case StatType.BaseMaxHealth:
            //     entity.Stats.BaseMaxHealth -= modifier.Value;
            //     break;
            // case StatType.BaseMaxMana:
            //     entity.Stats.BaseMaxMana -= modifier.Value;
            //     break;
            // case StatType.BaseAttack:
            //     entity.Stats.BaseAttack -= modifier.Value;
            //     break;
            // case StatType.BaseDefense:
            //     entity.Stats.BaseDefense -= modifier.Value;
            //     break;
            // case StatType.BaseDexterity:
            //     entity.Stats.BaseDexterity -= modifier.Value;
            //     break;
            case StatType.MaxHealthAug:
                entity.Stats.Augments.MaxHealth -= modifier.Value;
                entity.OnUpdateHealth();
                break;
            case StatType.MaxManaAug:
                entity.Stats.Augments.MaxMana -= modifier.Value;
                entity.OnUpdateMana();
                break;
            case StatType.AttackAug:
                entity.Stats.Augments.Attack -= modifier.Value;
                break;
            case StatType.DefenseAug:
                entity.Stats.Augments.Defense -= modifier.Value;
                break;
            case StatType.DexterityAug:
                entity.Stats.Augments.Dexterity -= modifier.Value;
                break;
            case StatType.AttackMultAug:
                entity.Stats.Augments.AttackMult -= modifier.Value;
                break;
            case StatType.DefenseMultAug:
                entity.Stats.Augments.DefenseMult -= modifier.Value;
                break;
            case StatType.FireBuildup:
                entity.Stats.Buildup.Fire -= modifier.Value;
                break;
            case StatType.PoisonBuildup:
                entity.Stats.Buildup.Poison -= modifier.Value;
                break;
            case StatType.FireResistance:
                entity.Stats.Resistances.Fire -= modifier.Value;
                break;
            case StatType.PoisonResistance:
                entity.Stats.Resistances.Poison -= modifier.Value;
                break;
        }
    }

    public static bool CanApplyStatModifier(Entity entity, StatModifier modifier) {
        switch (modifier.Type) {
            case StatType.None:
                return false;
            case StatType.Health:
                return entity.Stats.Health < entity.Stats.GetMaxHealth();
            case StatType.Mana:
                return entity.Stats.Mana < entity.Stats.GetMaxMana();
            case StatType.BaseMaxHealth:
            case StatType.BaseMaxMana:
            case StatType.BaseAttack:
            case StatType.BaseDefense:
            case StatType.BaseDexterity:
                return false;
            case StatType.MaxHealthAug:
                return entity.Stats.GetMaxHealth() < StatConstants.MAX_HEALTH;
            case StatType.MaxManaAug:
                return entity.Stats.GetMaxMana() < StatConstants.MAX_MANA;
            case StatType.AttackAug:
            case StatType.DefenseAug:
            case StatType.DexterityAug:
                return true;
            case StatType.AttackMultAug:
                return entity.Stats.Augments.AttackMult < StatConstants.MAX_MULT;
            case StatType.DefenseMultAug:
                return entity.Stats.Augments.DefenseMult < StatConstants.MAX_MULT;
            case StatType.FireBuildup:
                return entity.Stats.Buildup.Fire < StatConstants.MAX_BUILDUP;
            case StatType.PoisonBuildup:
                return entity.Stats.Buildup.Poison < StatConstants.MAX_BUILDUP;
            case StatType.FireResistance:
                return entity.Stats.Resistances.Fire < StatConstants.MAX_RESISTANCE;
            case StatType.PoisonResistance:
                return entity.Stats.Resistances.Poison < StatConstants.MAX_RESISTANCE;
        }

        return false;
    }

    public static void ApplyStatModifiers(Entity entity, StatModifierList modifierList) {
        for (int index = 0; index < modifierList.Count; ++index)
            ApplyStatModifier(entity, modifierList.Get(index));

        entity.OnUpdateStats();
    }

    public static void RemoveStatModifiers(Entity entity, StatModifierList modifierList) {
        for (int index = 0; index < modifierList.Count; ++index)
            RemoveStatModifier(entity, modifierList.Get(index));
    }

    public static bool CanApplyStatModifiers(Entity entity, StatModifierList modifierList) {
        for (int index = 0; index < modifierList.Count; ++index) {
            if(CanApplyStatModifier(entity, modifierList.Get(index)))
                return true;
        }

        return false;
    }
}
