using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UIElements;
using UIElements.Inventory;
using System.Collections.Generic;

public class ItemShopViewUI : MonoBehaviour {
    [SerializeField] private GameObject view;
    [SerializeField] private GameObject shopModeView;
    [SerializeField] private GameObject shopView;
    [SerializeField] private GameObject shopModeHolder;
    [SerializeField] private RectTransform itemViewHolder;
    [SerializeField] private ItemInfoView itemInfoView;
    [SerializeField] private PageView pageView;
    [SerializeField] private Text actionText;
    [SerializeField] private Text costText;
    [SerializeField] private Text goldAmountText;
    [SerializeField] private SpriteBoard spriteBoard;

    private ItemViewList itemViews;

    public void Init(UnityAction<ItemShopMode> setShopMode, UnityAction<ItemRef> selectItemCallback, UnityAction<int> selectPageCallback, UnityAction setCostCallback) {
        itemViews = new ItemViewList(itemViewHolder.GetComponentsInChildren<ItemViewUI>(), selectItemCallback);
        pageView.SetSelectPageCallback(selectPageCallback);

        foreach (var itemView in itemViews.ItemViews) {
            ItemAmountView amountView = itemView.GetComponent<ItemAmountView>();
            if (amountView)
                amountView.UpdateAmountAction += setCostCallback;
        }

        ShopModeButtonUI[] shopModeButtons = shopModeHolder.GetComponentsInChildren<ShopModeButtonUI>();
        foreach (var button in shopModeButtons)
            button.SelectShopModeAction += setShopMode;
    }

    public void Open() {
        view.SetActive(true);
        shopModeView.SetActive(true);
        shopView.SetActive(false);
    }

    public void Close() {
        view.SetActive(false);
        shopModeView.SetActive(false);
        shopView.SetActive(false);
    }

    public int GetItemViewCount() { return itemViews.GetCount(); }

    public void ToggleShopModeView(bool active) {
        shopModeView.SetActive(active);
        shopView.SetActive(!active);
    }

    public void SetShopMode(ItemShopMode shopMode) {
        foreach (var itemView in itemViews.ItemViews) {
            ItemAmountView amountView = itemView.GetComponent<ItemAmountView>();
            if (amountView != null)
                amountView.clampToItemAmount = shopMode == ItemShopMode.Selling;
        }

        actionText.text = shopMode == ItemShopMode.Buying ? "Buy" : "Sell";
    }

    public void SelectItem(ItemRef item) {
        if (item != null && item.ReferencedItem != null) {
            itemInfoView.SetView(item);
            SetStatViews(item.ReferencedItem);
        } else
            ClearItemInfoView();
    }

    private void SetStatViews(Item item) {
        if (item.Type == ItemType.Weapon || item.Type == ItemType.Armor) {
            StatInfo[] statInfo = InventoryUtil.GetStatInfo(item, spriteBoard);
            itemInfoView.SetStatViews(statInfo);
        }
    }

    public void SetItemViews(List<ItemRef> items, EquippedDelegate isEquippedCallback) {
        itemViews.Clear();

        int maxIndex = Mathf.Min(items.Count, itemViews.GetCount());
        for (int index = 0; index < maxIndex; ++index) {
            itemViews.SetItem(index, items[index], isEquippedCallback(items[index].ReferencedItem));
            ItemAmountView amountView = itemViews.ItemViews[index].GetComponent<ItemAmountView>();
            if (amountView != null)
                amountView.Clear();
        }
    }

    public void SetCost(int cost) {
        costText.text = cost.ToString("N0");
    }

    public void SetGoldAmount(int goldAmount) {
        goldAmountText.text = goldAmount.ToString("N0");
    }

    public void UpdatePageView(int pageCount) {
        pageView.Update(pageCount);
    }

    public List<ItemRef> GetShopItems() {
        List<ItemRef> shopItems = new List<ItemRef>();

        foreach (var itemView in itemViews.ItemViews) {
            ItemAmountView amountView = itemView.GetComponent<ItemAmountView>();
            int amount = amountView != null ? amountView.Amount : 0;

            if (amount > 0)
                shopItems.Add(new ItemRef(itemView.TargetItem.ReferencedItem, amount));
        }

        return shopItems;
    }

    public void ClearItemInfoView() {
        itemInfoView.Clear();
        itemInfoView.ClearStatViews();
    }

    public void Clear() {
        itemViews.Clear();
        itemInfoView.Clear();
        itemInfoView.ClearStatViews();
        pageView.Clear();
    }
}
