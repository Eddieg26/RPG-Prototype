using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New ItemBlueprint", menuName = "Game/Item/Blueprint")]
public class ItemBlueprint : ScriptableObject {
    [SerializeField] private Item targetItem;
    [SerializeField] private List<ItemRef> craftingItems;

    public Item TargetItem { get { return targetItem; } }
    public int ItemCount { get { return craftingItems.Count; } }

    public ItemRef GetCraftingItem(int index) {
        return craftingItems[index];
    }
}
