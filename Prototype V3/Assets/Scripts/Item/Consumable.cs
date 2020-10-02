using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Game/Item/Consumable")]
public class Consumable : Item {
    [SerializeField] private StatModifierEffect modifierEffect;
    [SerializeField] private StatModifier modifier;

    public StatModifierEffect ModifierEffect { get { return modifierEffect; } }
    public StatModifier Modifier { get { return modifier; } }
    public override ItemType Type { get { return ItemType.Consumable; } }
}
