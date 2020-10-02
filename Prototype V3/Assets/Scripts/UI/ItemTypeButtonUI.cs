using UnityEngine;
using UnityEngine.Events;

public class ItemTypeButtonUI : MonoBehaviour {
    [SerializeField] private ItemType itemType;

    public UnityAction<ItemType> SelectItemTypeAction { get; set; }

    public void OnSelect() {
        if(SelectItemTypeAction != null)
            SelectItemTypeAction(itemType);
    }
}
