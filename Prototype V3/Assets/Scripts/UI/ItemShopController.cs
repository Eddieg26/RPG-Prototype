using UnityEngine;
using System.Collections.Generic;

public class ItemShopController : MonoBehaviour {
    [SerializeField] private GameEvent openItemShopEvent;
    [SerializeField] private GameEvent forcePauseEvent;
    [SerializeField] private GameEvent togglePauseEvent;
    [SerializeField] private IntObject goldObject;

    private ItemShopViewUI itemShopView;
    private InputController inputController;
    private Inventory itemShopInventory;
    private Inventory playerInventory;
    private PlayerEquipment playerEquipment;
    private ItemShopMode shopMode;
    private ItemRef selectedItem;
    private List<ItemRef> targetItems;
    private int currentPage;
    private int pageCount;
    private int cost;

    private GameEventListener<Inventory> openItemShopListener;
    private GameEventListener<bool> togglePauseListener;

    private void Awake() {
        itemShopView = GetComponent<ItemShopViewUI>();
        inputController = GetComponent<InputController>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        playerEquipment = FindObjectOfType<PlayerEquipment>();

        itemShopView.Init(SetShopMode, SelectItem, SetCurrentPage, SetCost);

        openItemShopListener = new GameEventListener<Inventory>(Open);
        openItemShopEvent.AddListener(openItemShopListener);

        togglePauseListener = new GameEventListener<bool>(TogglePause);
    }

    private void Open(Inventory itemShopInventory) {
        this.itemShopInventory = itemShopInventory;

        forcePauseEvent.Invoke(true);
        togglePauseEvent.AddListener(togglePauseListener);

        itemShopView.Open();
        SetTargetItems();
        SetCurrentPage(0);
        SetPageCount();
        ViewTargetItems();
        itemShopView.ClearItemInfoView();
        itemShopView.SetGoldAmount(goldObject.Value);
        inputController.SetAsFocusedController();
    }

    private void Close() {
        itemShopView.Close();
        forcePauseEvent.Invoke(false);
        togglePauseEvent.RemoveListener(togglePauseListener);
    }

    private void TogglePause(bool isPaused) {
        itemShopView.Close();
    }

    private void SetShopMode(ItemShopMode shopMode) {
        this.shopMode = shopMode;
        itemShopView.SetShopMode(shopMode);
        itemShopView.ToggleShopModeView(false);

        SetTargetItems();
        SetPageCount();
        ViewTargetItems();
        itemShopView.ClearItemInfoView();
    }

    private void SelectItem(ItemRef item) {
        selectedItem = item;
        itemShopView.SelectItem(item);
    }

    private void SetCurrentPage(int currentPage) {
        if (this.currentPage != currentPage) {
            this.currentPage = currentPage;
            ViewTargetItems();
        }
    }

    private void SetTargetItems() {
        targetItems = shopMode == ItemShopMode.Buying ? itemShopInventory.GetItems() : playerInventory.GetItems();
    }

    private void SetPageCount() {
        pageCount = targetItems.Count > 0 ? Mathf.CeilToInt(targetItems.Count / (float)itemShopView.GetItemViewCount()) : 1;
        itemShopView.UpdatePageView(pageCount);
    }

    private void ViewTargetItems() {
        int startIndex = currentPage * itemShopView.GetItemViewCount();
        int endIndex = Mathf.Min(startIndex + itemShopView.GetItemViewCount(), targetItems.Count);

        itemShopView.SetItemViews(targetItems.Count > 0 ? targetItems.GetRange(startIndex, endIndex) : targetItems, playerEquipment.IsEquipped);
    }

    private void SetCost() {
        cost = 0;
        List<ItemRef> shopItems = itemShopView.GetShopItems();
        shopItems.ForEach((item) => {
            cost += item.ReferencedItem.Value * item.Amount;
        });

        itemShopView.SetCost(cost);
    }

    public void ExecuteAction() {
        if (shopMode == ItemShopMode.Buying)
            BuyItems();
        else
            SellItems();
    }

    public void CancelAction() {
        itemShopView.ToggleShopModeView(true);
    }

    private void BuyItems() {
        if (goldObject.Value >= cost) {
            List<ItemRef> shopItems = itemShopView.GetShopItems();
            shopItems.ForEach((item) => playerInventory.AddItem(item));
            goldObject.Value -= cost;
            itemShopView.SetGoldAmount(goldObject.Value);
            itemShopView.ToggleShopModeView(true);
        }
    }

    private void SellItems() {
        List<ItemRef> shopItems = itemShopView.GetShopItems();
        if (shopItems.Count > 0) {
            shopItems.ForEach((item) => playerInventory.RemoveItem(item));
            itemShopView.ToggleShopModeView(true);
            goldObject.Value += cost;
            itemShopView.SetGoldAmount(goldObject.Value);
        }
    }

    private void OnDestroy() {
        openItemShopEvent.RemoveListener(openItemShopListener);
        togglePauseEvent.RemoveListener(togglePauseListener);
    }
}

public enum ItemShopMode {
    Buying,
    Selling
}
