using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ItemBlueprintViewUI : MonoBehaviour {
    [SerializeField] private Image icon;
    [SerializeField] private Text nameText;

    private ItemBlueprint itemBlueprint;

    public UnityAction<ItemBlueprint> SelectItemBlueprintAction { get; set; }

    public void SetItemBlueprint(ItemBlueprint blueprint) {
        if(blueprint != null) {
            this.itemBlueprint = blueprint;
            icon.gameObject.SetActive(true);
            icon.sprite = itemBlueprint.TargetItem.Icon;
            nameText.text = itemBlueprint.TargetItem.Name;
        }
    }

    public void OnSelect() {
        if(SelectItemBlueprintAction != null)
            SelectItemBlueprintAction(itemBlueprint);
    }

    public void Clear() {
        this.itemBlueprint = null;
        icon.gameObject.SetActive(false);
        icon.sprite = null;
        nameText.text = string.Empty;
    }
}
