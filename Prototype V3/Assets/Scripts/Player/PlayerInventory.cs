using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour, Inventory {
    [SerializeField] private List<ItemRef> items;
    [SerializeField] private GameEvent addItemEvent;
    [SerializeField] private GameEvent removeItemEvent;

    public void AddItem(ItemRef item) {
        ItemRef foundItem = items.Find((i) => i.ReferencedItem == item.ReferencedItem);

        if (Object.ReferenceEquals(item, foundItem))
            return;

        if (foundItem != null)
            foundItem.Amount += item.Amount;
        else
            items.Add(item);

        OnAddItem(new ItemRef(item.ReferencedItem, item.Amount));
    }

    public void RemoveItem(ItemRef item) {
        ItemRef foundItem = items.Find((i) => i.ReferencedItem == item.ReferencedItem);

        if (foundItem != null) {
            int amount = item.Amount;
            foundItem.Amount -= amount;

            if (foundItem.Amount == 0)
                items.Remove(foundItem);

            OnRemoveItem(new ItemRef(item.ReferencedItem, amount));
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

    private void OnAddItem(ItemRef item) {
        if (addItemEvent != null)
            addItemEvent.Invoke<ItemRef>(item);
    }

    private void OnRemoveItem(ItemRef item) {
        if (removeItemEvent != null)
            removeItemEvent.Invoke<ItemRef>(item);
    }
}
