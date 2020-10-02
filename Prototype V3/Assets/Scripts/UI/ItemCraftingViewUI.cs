using UnityEngine;
using UnityEngine.Events;
using UIElements;
using UIElements.Inventory;
using System.Collections.Generic;

public class ItemCraftingViewUI : MonoBehaviour {
    [SerializeField] private GameObject view;
    [SerializeField] private RectTransform itemViewsHolder;
    [SerializeField] private RectTransform craftingItemViewsHolder;
    [SerializeField] private ItemInfoView itemInfoView;
    [SerializeField] private PageView pageView;
    [SerializeField] private SpriteBoard spriteBoard;

    private ItemBlueprintViewList itemBlueprintViews;
    private CraftingItemViewList craftingItemViews;

    public void Init(UnityAction<ItemBlueprint> selectItemBlueprintCallback, UnityAction<int> selectPageCallback) {
        itemBlueprintViews = new ItemBlueprintViewList(itemViewsHolder.GetComponentsInChildren<ItemBlueprintViewUI>(), selectItemBlueprintCallback);
        craftingItemViews = new CraftingItemViewList(craftingItemViewsHolder.GetComponentsInChildren<CraftingItemViewUI>());

        pageView.SetSelectPageCallback(selectPageCallback);
    }

    public void Open() {
        view.SetActive(true);
    }

    public void Close() {
        view.SetActive(false);
    }

    public int GetBlueprintViewCount() { return itemBlueprintViews.GetCount(); }

    public void SelectItemBlueprint(ItemBlueprint blueprint, GetItemAmountDelegate getItemAmountCallback) {
        if (blueprint != null && blueprint.TargetItem != null) {
            itemInfoView.SetView(new ItemRef(blueprint.TargetItem, getItemAmountCallback(blueprint.TargetItem)));
            SetStatViews(blueprint.TargetItem);
            SetCraftingItemViews(blueprint, getItemAmountCallback);
        }
    }

    private void SetStatViews(Item item) {
        if (item.Type == ItemType.Weapon || item.Type == ItemType.Armor) {
            StatInfo[] statInfo = InventoryUtil.GetStatInfo(item, spriteBoard);
            itemInfoView.SetStatViews(statInfo);
        }
    }

    private void SetCraftingItemViews(ItemBlueprint blueprint, GetItemAmountDelegate getItemAmountCallback) {
        craftingItemViews.Clear();

        int maxIndex = Mathf.Min(blueprint.ItemCount, craftingItemViews.GetCount());
        for (int index = 0; index < maxIndex; ++index)
            craftingItemViews.SetCraftingItem(index, blueprint.GetCraftingItem(index), getItemAmountCallback);
    }

    public void SetItemBlueprintViews(List<ItemBlueprint> blueprints) {
        itemBlueprintViews.Clear();

        int maxIndex = Mathf.Min(blueprints.Count, itemBlueprintViews.GetCount());
        for (int index = 0; index < maxIndex; ++index)
            itemBlueprintViews.SetItemBlueprint(index, blueprints[index]);
    }

    public void UpdatePageView(int pageCount) {
        pageView.Update(pageCount);
    }

    public void ClearItemInfo() {
        itemInfoView.Clear();
        itemInfoView.ClearStatViews();
    }

    public void ClearCraftingItemViews() {
        craftingItemViews.Clear();
    }

    public void Clear() {
        itemInfoView.Clear();
        itemInfoView.ClearStatViews();
        craftingItemViews.Clear();
        itemBlueprintViews.Clear();
    }
}
