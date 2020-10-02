using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class StatModifier {
    [SerializeField] private StatType type;
    [SerializeField] private int value;
    public StatType Type { get { return type; } set { type = value; } }
    public int Value { get { return value; } set { this.value = value; } }

    public StatModifier() { }

    public StatModifier(StatType type, int value) {
        this.type = type;
        this.value = value;
    }

    public override bool Equals(object obj) {
        if (Object.ReferenceEquals(null, obj))
            return false;

        if (Object.ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != this.GetType())
            return false;

        return IsEqual((StatModifier)obj);
    }

    public bool Equals(StatModifier modifier) {
        if (Object.ReferenceEquals(null, modifier))
            return false;

        if (Object.ReferenceEquals(this, modifier))
            return true;

        return IsEqual(modifier);
    }

    private bool IsEqual(StatModifier modifier) {
        return (modifier != null) && (modifier.Type == type) && (modifier.value == value);
    }

    public override int GetHashCode() {
        unchecked {
            int hash = 13;
            hash = (hash * 7) + type.GetHashCode();
            hash = (hash * 7) + value.GetHashCode();
            return hash;
        }
    }

    public static bool operator ==(StatModifier modifierA, StatModifier modifierB) {
        if (Object.ReferenceEquals(modifierA, modifierB))
            return true;

        if (Object.ReferenceEquals(null, modifierA))
            return false;

        return modifierA.Equals(modifierB);
    }

    public static bool operator !=(StatModifier modifierA, StatModifier modifierB) {
        return !(modifierA == modifierB);
    }
}

[System.Serializable]
public class StatModifierList {
    [SerializeField] private List<StatModifier> modifiers = new List<StatModifier>();

    public int Count { get { return modifiers.Count; } }

    public StatModifierList() {
        modifiers = new List<StatModifier>();
    }

    public StatModifierList(List<StatModifier> modifiers) {
        this.modifiers.AddRange(modifiers);
    }

    public StatModifierList(params StatModifier[] modifiers) {
        this.modifiers.AddRange(modifiers);
    }

    public StatModifier Get(int index) {
        return modifiers[index];
    }

    public void Set(StatModifier modifier, int index) {
        modifiers[index] = modifier;
    }

    public void Add(StatModifier modifier) {
        modifiers.Add(modifier);
    }

    public void RemoveAt(int index) {
        modifiers.RemoveAt(index);
    }

    public bool Remove(StatModifier modifier) {
        return modifiers.Remove(modifier);
    }
}
