using UnityEngine;
using UnityEngine.Events;

public class ShopModeButtonUI : MonoBehaviour {
    [SerializeField] private ItemShopMode shopMode;

    public UnityAction<ItemShopMode> SelectShopModeAction { get; set; }

    public void OnSelect() {
        if (SelectShopModeAction != null)
            SelectShopModeAction(shopMode);
    }
}
