using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(ItemViewUI))]
public class ItemAmountView : MonoBehaviour {
    [SerializeField] private Text amountText;

    private ItemViewUI itemViewUI;
    private int amount;

    public bool clampToItemAmount { get; set; }
    public UnityAction UpdateAmountAction { get; set; }
    public int Amount { get { return amount; } }

    private void Start() {
        itemViewUI = GetComponent<ItemViewUI>();
    }

    public void IncreaseAmount() {
        if (itemViewUI.TargetItem != null && itemViewUI.TargetItem.ReferencedItem != null) {
            amount = Mathf.Clamp(amount + 1, 0, clampToItemAmount ? itemViewUI.TargetItem.Amount : 99);
            Update();
        }
    }

    public void DecreaseAmount() {
        if (amount > 0) {
            amount--;
            Update();
        }
    }

    public void Clear() {
        amount = 0;
        Update();
    }

    private void Update() {
        amountText.text = amount.ToString();
        if (UpdateAmountAction != null)
            UpdateAmountAction();
    }
}
