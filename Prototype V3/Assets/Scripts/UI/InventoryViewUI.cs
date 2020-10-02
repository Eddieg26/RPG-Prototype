using UnityEngine;
using UnityEngine.Events;
using UIElements;
using UIElements.Inventory;
using System.Collections.Generic;

public class InventoryViewUI : MonoBehaviour, IViewUI {
    [SerializeField] private GameObject view;
    [SerializeField] private RectTransform itemViewHolder;
    [SerializeField] private RectTransform itemTypeButtonsHolder;
    [SerializeField] private ItemInfoView itemInfoView;
    [SerializeField] private PageView pageView;
    [SerializeField] private SpriteBoard spriteBoard;
    [SerializeField] private GameEvent registerViewEvent;

    private ItemViewList itemViews;
    private UnityAction openCallback { get; set; }

    private void Start() {
        registerViewEvent.Invoke(new RegisterViewData(this, UIConstants.INVENTORY_VIEW_INDEX));
    }

    public void Init(UnityAction openCallback, UnityAction<ItemRef> selectItemCallback, UnityAction<ItemType> selectItemTypeCallback, UnityAction<int> selectPageCallback) {
        this.openCallback = openCallback;

        itemViews = new ItemViewList(itemViewHolder.GetComponentsInChildren<ItemViewUI>(), selectItemCallback);

        ItemTypeButtonUI[] itemTypeButtons = itemTypeButtonsHolder.GetComponentsInChildren<ItemTypeButtonUI>();
        foreach (var itemTypeButton in itemTypeButtons)
            itemTypeButton.SelectItemTypeAction = selectItemTypeCallback;

        pageView.SetSelectPageCallback(selectPageCallback);
    }

    public void Open() {
        view.SetActive(true);

        if (openCallback != null)
            openCallback();
    }

    public void Close() {
        view.SetActive(false);
    }

    public string GetTitle() { return "Inventory"; }

    public int GetItemViewCount() { return itemViews.GetCount(); }

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
        for (int index = 0; index < maxIndex; ++index)
            itemViews.SetItem(index, items[index], isEquippedCallback(items[index].ReferencedItem));
    }

    public void UpdateItemViews(EquippedDelegate isEquippedCallback) {
        itemViews.UpdateViews(isEquippedCallback);
    }

    public void UpdatePageView(int pageCount) {
        pageView.Update(pageCount);
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
