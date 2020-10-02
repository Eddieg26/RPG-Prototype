
public enum ItemType {
    None,
    Weapon,
    Armor,
    Consumable,
    Misc
}

public enum ArmorType {
    None = -1,
    Helmet = 0,
    Torso = 1,
    Gloves = 2,
    Boots = 3,

    // Not an actual armor type. Used for counting purposes
    Max = 4
}
