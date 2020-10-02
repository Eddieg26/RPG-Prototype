using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Game/Item/Armor")]
public class Armor : Item {
    [SerializeField] private int defense;
    [SerializeField] private ArmorType armorType;
    [SerializeField] private StatModifierList modifiers;

    public int Defense { get { return defense; } }
    public ArmorType ArmorType { get { return armorType; } }
    public override ItemType Type { get { return ItemType.Armor; } }
    public StatModifierList Modifiers { get { return modifiers; } }
}
