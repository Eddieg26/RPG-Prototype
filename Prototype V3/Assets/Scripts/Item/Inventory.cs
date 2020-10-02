using UnityEngine;
using System.Collections.Generic;

public interface Inventory {
    void AddItem(ItemRef item);
    void RemoveItem(ItemRef item);
    List<ItemRef> GetItems();
    List<ItemRef> GetItemsByType(ItemType type);
    int GetItemAmount(Item item);
    void SetItems(List<ItemRef> itemList);
    void Clear();
}
