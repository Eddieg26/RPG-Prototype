using UnityEngine;

public class PlayerEquipment : MonoBehaviour {
    private Entity self;
    private Weapon weapon;
    private Armor[] equippedArmor = new Armor[(int)ArmorType.Max];

    private void Awake() {
        self = GetComponent<Entity>();
    }

    public void EquipWeapon(Weapon weapon) {
        Debug.Assert(weapon != null, "EquipWeapon: Weapon parameter cannot be null.");
        Debug.Assert(this.weapon == null, "EquipWeapon: Trying to equip weapon before unequipping current weapon.");

        this.weapon = weapon;
        self.Stats.Augments.Attack += weapon.Damage;
    }

    public Weapon UnEquipWeapon() {
        if (weapon != null) {
            self.Stats.Augments.Attack -= weapon.Damage;
            self.OnUpdateStats();
        }

        Weapon unequippedWeapon = weapon;
        weapon = null;
        return unequippedWeapon;
    }

    public void EquipArmor(Armor armor) {
        Debug.Assert(armor != null, "EquipArmor: Armor parameter cannot be null.");
        Debug.Assert(equippedArmor[(int)armor.ArmorType] == null, $"EquipArmor: Trying to equip armor{armor.ArmorType} before unequipping current armor{armor.ArmorType}.");

        equippedArmor[(int)armor.ArmorType] = armor;
        self.Stats.Augments.Defense += armor.Defense;
        StatModifierUtil.ApplyStatModifiers(self, armor.Modifiers);
        self.OnUpdateStats();
    }

    public Armor UnEquipArmor(ArmorType type) {
        Armor armor = equippedArmor[(int)type];

        if (armor != null) {
            self.Stats.Augments.Defense -= armor.Defense;
            StatModifierUtil.RemoveStatModifiers(self, armor.Modifiers);
            self.OnUpdateStats();
        }

        equippedArmor[(int)type] = null;

        return armor;
    }

    public bool IsEquipped(Item item) {
        if (item == null)
            return false;

        if (item.Type == ItemType.Weapon)
            return IsWeaponEquipped((Weapon)item);
        else if (item.Type == ItemType.Armor)
            return IsArmorEquipped((Armor)item);

        return false;
    }

    public bool IsWeaponEquipped(Weapon weapon) {
        if (weapon == null || this.weapon == null)
            return false;

        return this.weapon == weapon;
    }

    public bool IsArmorEquipped(Armor armor) {
        if (armor == null || equippedArmor[(int)armor.ArmorType] == null)
            return false;

        if (armor.ArmorType == ArmorType.None)
            return false;

        return armor == equippedArmor[(int)armor.ArmorType];
    }

    public bool UseConsumable(Consumable consumable) {
        bool result = false;
        StatModifierEffect modifierEffect = consumable.ModifierEffect;
        StatModifier modifier = consumable.Modifier;

        if (modifierEffect != null && StatModifierUtil.CanApplyStatModifier(self, modifierEffect.Modifier)) {
            self.AddStatusEffect(modifierEffect);
            result = true;
        }

        if (modifier != null && StatModifierUtil.CanApplyStatModifier(self, modifier)) {
            StatModifierUtil.ApplyStatModifier(self, modifier);
            result = true;
        }

        return result;
    }
}
