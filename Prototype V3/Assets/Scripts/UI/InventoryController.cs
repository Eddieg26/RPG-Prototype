using UnityEngine;
using System.Collections.Generic;

public class InventoryController : MonoBehaviour {
    private InventoryViewUI inventoryView;
    private InputController inputController;
    private PlayerInventory inventory;
    private PlayerEquipment equipment;

    private ItemRef selectedItem;
    private List<ItemRef> targetItems;
    private ItemType currentItemType = ItemType.None;
    private int currentPage;
    private int pageCount;

    private void Start() {
        inventoryView = GetComponent<InventoryViewUI>();
        inputController = GetComponent<InputController>();
        inventory = FindObjectOfType<PlayerInventory>();
        equipment = FindObjectOfType<PlayerEquipment>();

        InitInputBindings();

        inventoryView.Init(OnOpenInventoryView, SelectItem, SelectItemType, SetCurrentPage);
    }

    private void SelectItemType(ItemType itemType) {
        if (currentItemType != itemType && itemType != ItemType.None) {
            currentItemType = itemType;
            SetTargetItems();
            SetPageAmount();
            ViewTargetItems();
            inventoryView.ClearItemInfoView();
        }
    }

    private void SelectItem(ItemRef item) {
        selectedItem = item;
        inventoryView.SelectItem(item);
    }

    private void SetCurrentPage(int currentPage) {
        if (this.currentPage != currentPage) {
            this.currentPage = currentPage;
            ViewTargetItems();
        }
    }

    private void SetTargetItems() {
        targetItems = inventory.GetItemsByType(currentItemType);
    }

    private void SetPageAmount() {
        pageCount = targetItems.Count > 0 ? Mathf.CeilToInt(targetItems.Count / (float)inventoryView.GetItemViewCount()) : 1;
        inventoryView.UpdatePageView(pageCount);
    }

    private void ViewTargetItems() {
        int startIndex = currentPage * inventoryView.GetItemViewCount();
        int endIndex = Mathf.Min(startIndex + inventoryView.GetItemViewCount(), targetItems.Count);

        inventoryView.SetItemViews(targetItems.Count > 0 ? targetItems.GetRange(startIndex, endIndex) : targetItems, equipment.IsEquipped);
    }

    private void OnOpenInventoryView() {
        currentItemType = ItemType.None;
        inventoryView.Clear();
        SelectItemType(ItemType.Weapon);
        inventoryView.UpdatePageView(pageCount);

        inputController.SetAsFocusedController();
    }

    private void InitInputBindings() {
        RuntimeInputConfiguration runtimeInput = inputController.GetRuntimeInputConfig();
        InputBinding equipBinding = runtimeInput.GetBinding(InputBindingName.PLAYER_INV_USEOREQUIP);

        equipBinding.Action.Start = EquipOrUseSelectedItem;
    }

    private void EquipOrUseSelectedItem() {
        if (selectedItem == null)
            return;

        if (selectedItem.ReferencedItem.Type == ItemType.Weapon || selectedItem.ReferencedItem.Type == ItemType.Armor) {
            bool isEquipped = equipment.IsEquipped(selectedItem.ReferencedItem);
            if (isEquipped) {
                if (selectedItem.ReferencedItem.Type == ItemType.Weapon)
                    equipment.UnEquipWeapon();
                else if (selectedItem.ReferencedItem.Type == ItemType.Armor)
                    equipment.UnEquipArmor((selectedItem.ReferencedItem as Armor).ArmorType);

                inventoryView.UpdateItemViews(equipment.IsEquipped);
            } else {
                if (selectedItem.ReferencedItem.Type == ItemType.Weapon) {
                    equipment.UnEquipWeapon();
                    equipment.EquipWeapon(selectedItem.ReferencedItem as Weapon);
                } else if (selectedItem.ReferencedItem.Type == ItemType.Armor) {
                    equipment.UnEquipArmor((selectedItem.ReferencedItem as Armor).ArmorType);
                    equipment.EquipArmor(selectedItem.ReferencedItem as Armor);
                }

                inventoryView.UpdateItemViews(equipment.IsEquipped);
            }
        } else if(selectedItem.ReferencedItem.Type == ItemType.Consumable) {
            if(equipment.UseConsumable(selectedItem.ReferencedItem as Consumable)) {
                inventory.RemoveItem(new ItemRef(selectedItem.ReferencedItem, 1));
                inventoryView.UpdateItemViews(equipment.IsEquipped);
                inventoryView.SelectItem(selectedItem);
            }
        }
    }
}
