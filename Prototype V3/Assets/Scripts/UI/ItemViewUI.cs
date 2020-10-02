using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemViewUI : MonoBehaviour {
    [SerializeField] private Image iconImage;
    [SerializeField] private Text nameText;
    [SerializeField] private Text valueText;
    [SerializeField] private Text equippedText;

    private ItemRef targetItem;

    public ItemRef TargetItem { get { return targetItem; } }
    public UnityAction<ItemRef> SelectItemEvent { get; set; }

    private void Awake() {
        Clear();
    }

    public void SetItem(ItemRef item, bool isEquipped = false) {
        if (item != null && item.ReferencedItem != null) {
            targetItem = item;
            if (equippedText != null)
                equippedText.gameObject.SetActive(isEquipped);

            if (iconImage != null) {
                iconImage.gameObject.SetActive(true);
                iconImage.sprite = item.ReferencedItem.Icon;
            }

            if (nameText != null)
                nameText.text = item.ReferencedItem.Name;

            if (valueText != null)
                valueText.text = item.ReferencedItem.Value.ToString("N0");
        }
    }

    public void UpdateView(bool isEquipped = false) {
        if (targetItem != null && targetItem.ReferencedItem != null) {
            if (equippedText != null)
                equippedText.gameObject.SetActive(isEquipped);
        }
    }

    public void Clear() {
        targetItem = null;
        if (equippedText != null)
            equippedText.gameObject.SetActive(false);

        if (iconImage != null) {
            iconImage.sprite = null;
            iconImage.gameObject.SetActive(false);
        }

        if (nameText != null)
            nameText.text = string.Empty;

        if (valueText != null)
            valueText.text = string.Empty;
    }

    public void OnSelect() {
        if (SelectItemEvent != null)
            SelectItemEvent(targetItem);
    }
}
