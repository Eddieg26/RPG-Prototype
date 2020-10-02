using UnityEngine;

public abstract class Item : ScriptableObject {
    [SerializeField] private string itemName;
    [SerializeField, TextArea] private string info;
    [SerializeField] private Sprite icon;
    [SerializeField] private int value;

    public string Name { get { return itemName; } }
    public string Info { get { return info; } }
    public Sprite Icon { get { return icon; } }
    public int Value { get { return value; } }
    public virtual ItemType Type { get; }
}
