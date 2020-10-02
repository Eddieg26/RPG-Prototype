using UnityEngine;

[CreateAssetMenu(fileName = "New Misc", menuName = "Game/Item/Misc")]
public class Misc : Item {

    public override ItemType Type { get { return ItemType.Misc; } }
}
