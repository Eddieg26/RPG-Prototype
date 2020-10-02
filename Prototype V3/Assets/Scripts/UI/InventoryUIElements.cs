using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

namespace UIElements {
    namespace Inventory {
        public delegate bool EquippedDelegate(Item item);
        public delegate int GetItemAmountDelegate(Item item);

        [System.Serializable]
        public class ItemInfoView {
            [SerializeField] private GameObject view;
            [SerializeField] private Text itemNameText;
            [SerializeField] private Text itemInfoText;
            [SerializeField] private Text itemAmountText;
            [SerializeField] private Image itemIconImage;
            [SerializeField] private StatView[] statViews;

            public void SetView(ItemRef item) {
                Clear();

                this.view.SetActive(true);
                this.itemIconImage.gameObject.SetActive(true);
                this.itemNameText.text = item.ReferencedItem.Name;
                this.itemInfoText.text = item.ReferencedItem.Info;
                this.itemAmountText.text = item.Amount.ToString();
                this.itemIconImage.sprite = item.ReferencedItem.Icon;
            }

            public void SetStatViews(StatInfo[] statInfo) {
                ClearStatViews();

                int maxIndex = Mathf.Min(statInfo.Length, statViews.Length);
                for (int index = 0; index < maxIndex; ++index) {
                    if (statInfo[index] == null)
                        break;
                    statViews[index].SetView(statInfo[index].icon, statInfo[index].value);
                }
            }

            public void Clear() {
                this.view.SetActive(false);
                this.itemIconImage.gameObject.SetActive(false);
                this.itemNameText.text = string.Empty;
                this.itemInfoText.text = string.Empty;
                this.itemAmountText.text = string.Empty;
            }

            public void ClearStatViews() {
                foreach (StatView statView in statViews)
                    statView.Clear();
            }
        }

        [System.Serializable]
        public class StatView {
            [SerializeField] private GameObject view;
            [SerializeField] private Image iconImage;
            [SerializeField] private Text valueText;

            public void SetView(Sprite icon, int value) {
                this.view.SetActive(true);
                this.iconImage.gameObject.SetActive(true);
                this.iconImage.sprite = icon;
                this.valueText.text = value.ToString();
            }

            public void Clear() {
                this.view.SetActive(false);
                this.iconImage.gameObject.SetActive(false);
                this.valueText.text = string.Empty;
            }
        }

        public class StatInfo {
            public Sprite icon;
            public int value;

            public StatInfo(Sprite icon, int value) {
                this.icon = icon;
                this.value = value;
            }
        }

        public class ItemViewList {
            private ItemViewUI[] itemViews;

            public ItemViewUI[] ItemViews { get { return itemViews; } }

            public ItemViewList(ItemViewUI[] itemViews, UnityAction<ItemRef> selectItemCallback) {
                SetItemViews(itemViews);
                SetSelectItemCallback(selectItemCallback);
            }

            public void SetItemViews(ItemViewUI[] itemViews) {
                this.itemViews = itemViews;
            }

            public void SetSelectItemCallback(UnityAction<ItemRef> selectItemCallback) {
                foreach (var view in itemViews)
                    view.SelectItemEvent = selectItemCallback;
            }

            public void SetItem(int index, ItemRef item, bool isEquipped) {
                itemViews[index].SetItem(item, isEquipped);
            }

            public void UpdateViews(EquippedDelegate isEquippedCallback) {
                foreach (var view in itemViews)
                    view.UpdateView((view.TargetItem != null && view.TargetItem.ReferencedItem != null ? isEquippedCallback(view.TargetItem.ReferencedItem) : false));
            }

            public int GetCount() { return itemViews.Length; }

            public void Clear() {
                foreach (var view in itemViews)
                    view.Clear();
            }
        }

        public class CraftingItemViewList {
            private CraftingItemViewUI[] itemViews;

            public CraftingItemViewList(CraftingItemViewUI[] itemViews) {
                SetItemViews(itemViews);
            }

            public void SetItemViews(CraftingItemViewUI[] itemViews) {
                this.itemViews = itemViews;
            }

            public void UpdateViews(GetItemAmountDelegate getItemAmountCallback) {
                foreach(var view in itemViews)
                    view.UpdateView(view.CraftingItem != null && view.CraftingItem.ReferencedItem != null ? getItemAmountCallback(view.CraftingItem.ReferencedItem) : 0);
            }

            public void SetCraftingItem(int index, ItemRef item, GetItemAmountDelegate getItemAmountCallback) {
                itemViews[index].SetCraftingItem(item, getItemAmountCallback(item.ReferencedItem));
            }

            public int GetCount() { return itemViews.Length; }

            public void Clear() {
                foreach(var view in itemViews)
                    view.Clear();
            }
        }

        public class ItemBlueprintViewList {
            private ItemBlueprintViewUI[] itemBlueprintViews;

            public ItemBlueprintViewList(ItemBlueprintViewUI[] itemBlueprintViews, UnityAction<ItemBlueprint> selectItemBlueprintCallback) {
                SetItemBlueprintViews(itemBlueprintViews);
                SetSelectItemBlueprintCallback(selectItemBlueprintCallback);
            }

            public void SetItemBlueprintViews(ItemBlueprintViewUI[] itemBlueprintViews) {
                this.itemBlueprintViews = itemBlueprintViews;
            }

            public void SetSelectItemBlueprintCallback(UnityAction<ItemBlueprint> selectItemBlueprintCallback) {
                foreach(var view in itemBlueprintViews)
                    view.SelectItemBlueprintAction += selectItemBlueprintCallback;
            }

            public void SetItemBlueprint(int index, ItemBlueprint blueprint) {
                itemBlueprintViews[index].SetItemBlueprint(blueprint);
            }

            public int GetCount() { return itemBlueprintViews.Length; }

            public void Clear() {
                foreach(var view in itemBlueprintViews)
                    view.Clear();
            }
        }

        public static class InventoryUtil {
            public static StatInfo[] GetStatInfo(Item item, SpriteBoard spriteBoard) {
                StatInfo[] statInfo = new StatInfo[StatConstants.MAX_STATINFO_COUNT];
                if (item.Type == ItemType.Weapon) {
                    Weapon weapon = (Weapon)item;
                    statInfo[0] = new StatInfo(spriteBoard.GetSprite(SpriteBoardType.ICON_ATTACK), weapon.Damage);
                } else if (item.Type == ItemType.Armor) {
                    Armor armor = (Armor)item;
                    statInfo[0] = new StatInfo(spriteBoard.GetSprite(SpriteBoardType.ICON_DEFENSE), armor.Defense);
                }

                return statInfo;
            }
        }
    }
}