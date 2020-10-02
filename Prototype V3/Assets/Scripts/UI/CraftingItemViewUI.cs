using UnityEngine;
using UnityEngine.UI;

public class CraftingItemViewUI : MonoBehaviour {
    [SerializeField] private GameObject view;
    [SerializeField] private Image icon;
    [SerializeField] private Text nameText;
    [SerializeField] private Text amountText;

    private ItemRef craftingItem;
    private int obtainedAmount;

    public ItemRef CraftingItem { get { return craftingItem; } }

    private void Start() {
        Clear();
    }

    public void SetCraftingItem(ItemRef item, int obtainedAmount = 0) {
        if (item != null && item.ReferencedItem != null) {
            this.craftingItem = item;
            this.obtainedAmount = obtainedAmount;

            view.SetActive(true);

            icon.gameObject.SetActive(true);
            icon.sprite = item.ReferencedItem.Icon;

            nameText.text = item.ReferencedItem.Name;
            amountText.text = $"{obtainedAmount}/{item.Amount}";
        }
    }

    public void UpdateView(int obtainedAmount) {
        amountText.text = $"{obtainedAmount}/{craftingItem.Amount}";
    }

    public void Clear() {
        craftingItem = null;
        view.SetActive(false);
        icon.gameObject.SetActive(false);
        icon.sprite = null;
        nameText.text = string.Empty;
        amountText.text = string.Empty;
    }
}
