using UnityEngine;
using System.Collections.Generic;

public class ItemCraftingController : MonoBehaviour {
    [SerializeField] private GameEvent openCraftingViewEvent;
    [SerializeField] private GameEvent forcePauseEvent;
    [SerializeField] private GameEvent togglePauseEvent;

    private ItemCraftingViewUI itemCraftingView;
    private InputController inputController;
    private List<ItemBlueprint> itemBlueprints;
    private Inventory playerInventory;
    private ItemBlueprint selectedBlueprint;
    private int currentPage;
    private int pageCount;

    private GameEventListener<List<ItemBlueprint>> openCraftingViewListener;
    private GameEventListener<bool> togglePauseListener;

    private void Awake() {
        itemCraftingView = GetComponent<ItemCraftingViewUI>();
        inputController = GetComponent<InputController>();
        playerInventory = FindObjectOfType<PlayerInventory>();

        itemCraftingView.Init(SelectItemBlueprint, SetCurrentPage);

        openCraftingViewListener = new GameEventListener<List<ItemBlueprint>>(Open);
        openCraftingViewEvent.AddListener(openCraftingViewListener);

        togglePauseListener = new GameEventListener<bool>(TogglePause);
    }

    private void Open(List<ItemBlueprint> itemBlueprints) {
        this.itemBlueprints = itemBlueprints;

        forcePauseEvent.Invoke(true);
        togglePauseEvent.AddListener(togglePauseListener);

        itemCraftingView.Open();
        SetCurrentPage(0);
        SetPageCount();
        ViewItemBlueprints();
        itemCraftingView.ClearItemInfo();
        itemCraftingView.ClearCraftingItemViews();
        inputController.SetAsFocusedController();
    }

    public void Close() {
        itemCraftingView.Close();
        forcePauseEvent.Invoke(false);
    }

    private void TogglePause(bool isPaused) {
        itemCraftingView.Close();
    }

    private void SelectItemBlueprint(ItemBlueprint blueprint) {
        selectedBlueprint = blueprint;
        itemCraftingView.SelectItemBlueprint(blueprint, playerInventory.GetItemAmount);
    }

    private void SetCurrentPage(int currentPage) {
        if (this.currentPage != currentPage) {
            this.currentPage = currentPage;
            ViewItemBlueprints();
        }
    }

    private void SetPageCount() {
        pageCount = itemBlueprints.Count > 0 ? Mathf.CeilToInt(itemBlueprints.Count / (float)itemCraftingView.GetBlueprintViewCount()) : 1;
        itemCraftingView.UpdatePageView(pageCount);
    }

    private void ViewItemBlueprints() {
        int startIndex = currentPage * itemCraftingView.GetBlueprintViewCount();
        int endIndex = Mathf.Min(startIndex + itemCraftingView.GetBlueprintViewCount(), itemBlueprints.Count);

        itemCraftingView.SetItemBlueprintViews(itemBlueprints.Count > 0 ? itemBlueprints.GetRange(startIndex, endIndex) : itemBlueprints);
    }

    public void CraftItem() {
        if (selectedBlueprint != null && selectedBlueprint.TargetItem != null && CanCraft()) {
            for (int index = 0; index < selectedBlueprint.ItemCount; ++index) {
                ItemRef craftingItem = selectedBlueprint.GetCraftingItem(index);
                playerInventory.RemoveItem(craftingItem);
            }

            playerInventory.AddItem(new ItemRef(selectedBlueprint.TargetItem, 1));
            SelectItemBlueprint(selectedBlueprint);
        }
    }

    private bool CanCraft() {
        bool result = true;
        for (int index = 0; index < selectedBlueprint.ItemCount; ++index) {
            int itemAmount = playerInventory.GetItemAmount(selectedBlueprint.GetCraftingItem(index).ReferencedItem);
            if (itemAmount < selectedBlueprint.GetCraftingItem(index).Amount) {
                result = false;
                break;
            }
        }

        return result;
    }

    private void OnDestroy() {
        openCraftingViewEvent.RemoveListener(openCraftingViewListener);
        togglePauseEvent.RemoveListener(togglePauseListener);
    }
}
