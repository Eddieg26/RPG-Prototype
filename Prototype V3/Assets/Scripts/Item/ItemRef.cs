using UnityEngine;

[System.Serializable]
public class ItemRef {
    [SerializeField, Range(1, 99)] private int amount = 1;
    [SerializeField] private Item referencedItem;

    public int Amount {
        get { return amount; }
        set { amount = value; }
    }

    public Item ReferencedItem { get { return referencedItem; } }

    public ItemRef() {
        this.amount = 1;
        this.referencedItem = null;
    }

    public ItemRef(Item referencedItem, int amount) {
        this.amount = amount;
        this.referencedItem = referencedItem;
    }
}
