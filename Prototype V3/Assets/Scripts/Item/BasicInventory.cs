using UnityEngine;
using System.Collections.Generic;

public class BasicInventory : MonoBehaviour, Inventory {
    [SerializeField] private List<ItemRef> items;

    public void AddItem(ItemRef item) {
        ItemRef foundItem = items.Find((itemRef) => itemRef.ReferencedItem == item.ReferencedItem);

        if (foundItem != null)
            foundItem.Amount += item.Amount;
        else
            items.Add(item);
    }

    public void RemoveItem(ItemRef item) {
        ItemRef foundItem = items.Find((itemRef) => itemRef.ReferencedItem == item.ReferencedItem);

        if (foundItem != null) {
            foundItem.Amount = Mathf.Max(foundItem.Amount - item.Amount, 0);
            if (foundItem.Amount == 0)
                items.Remove(foundItem);
        }
    }

    public List<ItemRef> GetItems() {
        return new List<ItemRef>(items);
    }

    public List<ItemRef> GetItemsByType(ItemType type) {
        return items.FindAll((item) => item.ReferencedItem.Type == type);
    }

    public int GetItemAmount(Item item) {
        ItemRef foundItem = items.Find((itemRef) => itemRef.ReferencedItem == item);
        return foundItem != null ? foundItem.Amount : 0;
    }

    public void SetItems(List<ItemRef> itemList) {
        items.Clear();
        items.AddRange(itemList);
    }

    public void Clear() {
        items.Clear();
    }
}
