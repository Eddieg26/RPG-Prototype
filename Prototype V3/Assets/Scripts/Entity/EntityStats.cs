using UnityEngine;

[System.Serializable]
public class EntityStats {
    [SerializeField] private int health;
    [SerializeField] private int mana;
    [SerializeField] private int baseMaxHealth;
    [SerializeField] private int baseMaxMana;
    [SerializeField] private int baseAttack;
    [SerializeField] private int baseDefense;
    [SerializeField] private int baseDexterity;
    [SerializeField] private StatAugments augments;
    [SerializeField] private StatusBuildup buildup;
    [SerializeField] private Resistances resistances;

    public EntityStats() {
        health = 100;
        mana = 100;
        baseMaxHealth = 100;
        baseMaxMana = 100;
    }

    public int Health {
        get { return health; }
        set { health = Mathf.Clamp(value, StatConstants.ZERO, GetMaxHealth()); }
    }

    public int Mana {
        get { return mana; }
        set { mana = Mathf.Clamp(value, StatConstants.ZERO, GetMaxMana()); }
    }

    public int BaseMaxHealth {
        get { return baseMaxHealth; }
        set { baseMaxHealth = value; }
    }

    public int BaseMaxMana {
        get { return baseMaxMana; }
        set { baseMaxMana = value; }
    }

    public int BaseAttack {
        get { return baseAttack; }
        set { baseAttack = value; }
    }

    public int BaseDefense {
        get { return baseDefense; }
        set { baseDefense = value; }
    }

    public int BaseDexterity {
        get { return baseDexterity; }
        set { baseDexterity = value; }
    }

    public StatAugments Augments {
        get { return augments; }
        set { augments = value; }
    }

    public StatusBuildup Buildup {
        get { return buildup; }
        set { buildup = value; }
    }

    public Resistances Resistances {
        get { return resistances; }
        set { resistances = value; }
    }

    public int GetMaxHealth() {
        return Mathf.Clamp(baseMaxHealth + augments.MaxHealth, health, StatConstants.MAX_HEALTH);
    }

    public int GetMaxMana() {
        return Mathf.Clamp(baseMaxMana + augments.MaxMana, mana, StatConstants.MAX_MANA);
    }

    public int GetAttack() {
        return Mathf.RoundToInt((baseAttack + augments.Attack) * (augments.AttackMult / StatConstants.MOD));
    }

    public int GetDefense() {
        return Mathf.RoundToInt((baseDefense + augments.Defense) * (augments.DefenseMult / StatConstants.MOD));
    }

    public int GetDexterity() {
        return baseDexterity + augments.Dexterity;
    }
}

[System.Serializable]
public class StatAugments {
    [SerializeField] private int maxHealth;
    [SerializeField] private int maxMana;
    [SerializeField] private int attack;
    [SerializeField] private int defense;
    [SerializeField] private int dexterity;
    [SerializeField, Range(StatConstants.ZERO, StatConstants.MAX_MULT)] private int attackMult;
    [SerializeField, Range(StatConstants.ZERO, StatConstants.MAX_MULT)] private int defenseMult;

    public StatAugments() {
        attackMult = 100;
        defenseMult = 100;
    }

    public int MaxHealth {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int MaxMana {
        get { return maxMana; }
        set { maxMana = value; }
    }

    public int Attack {
        get { return attack; }
        set { attack = value; }
    }

    public int Defense {
        get { return defense; }
        set { defense = value; }
    }

    public int Dexterity {
        get { return dexterity; }
        set { dexterity = value; }
    }

    public int AttackMult {
        get { return Mathf.Clamp(attackMult, StatConstants.MIN_MULT, StatConstants.MAX_MULT); }
        set { attackMult = value; }
    }

    public int DefenseMult {
        get { return Mathf.Clamp(defenseMult, StatConstants.MIN_MULT, StatConstants.MAX_MULT); }
        set { defenseMult = value; }
    }
}

[System.Serializable]
public class StatusBuildup {
    [SerializeField, Range(StatConstants.ZERO, StatConstants.MAX_BUILDUP)] private int fire;
    [SerializeField, Range(StatConstants.ZERO, StatConstants.MAX_BUILDUP)] private int poison;

    public int Fire {
        get { return Mathf.Clamp(fire, StatConstants.ZERO, StatConstants.MAX_BUILDUP); }
        set { fire = value; }
    }

    public int Poison {
        get { return Mathf.Clamp(poison, StatConstants.ZERO, StatConstants.MAX_BUILDUP); }
        set { poison = value; }
    }
}

[System.Serializable]
public class Resistances {
    [SerializeField, Range(StatConstants.ZERO, StatConstants.MAX_RESISTANCE)] private int fire;
    [SerializeField, Range(StatConstants.ZERO, StatConstants.MAX_RESISTANCE)] private int poison;

    public int Fire {
        get { return Mathf.Clamp(fire, StatConstants.ZERO, StatConstants.MAX_RESISTANCE); }
        set { fire = value; }
    }

    public int Poison {
        get { return Mathf.Clamp(poison, StatConstants.ZERO, StatConstants.MAX_RESISTANCE); }
        set { poison = value; }
    }
}
