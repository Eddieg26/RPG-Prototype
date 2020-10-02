using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Game/Item/Weapon")]
public class Weapon : Item {
    [SerializeField] private int damage;

    public int Damage { get { return damage; } }
    public override ItemType Type { get { return ItemType.Weapon; } }
}
