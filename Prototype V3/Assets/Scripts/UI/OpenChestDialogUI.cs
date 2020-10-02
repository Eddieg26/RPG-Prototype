using UnityEngine;
using UIElements;
using UIElements.Inventory;
using System.Collections.Generic;

public class OpenChestDialogUI : MonoBehaviour {
    [SerializeField] private GameObject view;
    [SerializeField] private ItemInfoView itemInfoView;
    [SerializeField] private PageView pageView;
    [SerializeField] private SpriteBoard spriteBoard;
    [SerializeField] private GameEvent openChestEvent;
    [SerializeField] private GameEvent forcePauseEvent;
    [SerializeField] private GameEvent togglePauseEvent;

    private List<ItemRef> items = new List<ItemRef>();
    private GameEventListener<List<ItemRef>> openChestListener;
    private GameEventListener<bool> togglePauseListener;
    private InputController inputController;

    private void Awake() {
        openChestListener = new GameEventListener<List<ItemRef>>(Open);
        togglePauseListener = new GameEventListener<bool>(OnTogglePause);

        openChestEvent.AddListener(openChestListener);

        pageView.SetSelectPageCallback(SetCurrentItem);

        inputController = GetComponent<InputController>();

        HideView();
    }

    public void Open(List<ItemRef> items) {
        forcePauseEvent.Invoke(true);
        togglePauseEvent.AddListener(togglePauseListener);

        SetItems(items);
        SetCurrentItem(0);
        ShowView();

        inputController.SetAsFocusedController();
    }

    public void Close() {
        HideView();
        forcePauseEvent.Invoke(false);
        togglePauseEvent.RemoveListener(togglePauseListener);
    }

    private void ShowView() {
        view.SetActive(true);
    }

    private void HideView() {
        view.SetActive(false);
    }

    public void SetCurrentItem(int index) {
        ShowItem(items[index]);
    }

    public void ShowItem(ItemRef item) {
        itemInfoView.SetView(item);
        itemInfoView.SetStatViews(InventoryUtil.GetStatInfo(item.ReferencedItem, spriteBoard));
    }

    private void SetItems(List<ItemRef> items) {
        this.items.Clear();
        this.items.AddRange(items);
        pageView.Update(items.Count);
    }

    private void OnTogglePause(bool isPaused) {
        HideView();
    }

    private void OnDestroy() {
        openChestEvent.RemoveListener(openChestListener);
        togglePauseEvent.RemoveListener(togglePauseListener);
    }
}
