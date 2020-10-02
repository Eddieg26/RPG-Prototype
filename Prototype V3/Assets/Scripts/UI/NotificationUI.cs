using UnityEngine;
using System.Collections.Generic;

public class NotificationUI : MonoBehaviour {
    [SerializeField] private GameObject view;
    [SerializeField] private GameEvent addItemEvent;
    [SerializeField] private GameEvent removeItemEvent;
    [SerializeField] private float notificationDuration;
    [SerializeField] private bool showNotifications = true;

    private List<NotificationInfo> notifications = new List<NotificationInfo>();
    private NotificationView[] notificationViews;
    private GameEventListener<ItemRef> addItemListener;
    private GameEventListener<ItemRef> removeItemListener;

    private void Awake() {
        notificationViews = view.GetComponentsInChildren<NotificationView>(true);

        addItemListener = new GameEventListener<ItemRef>(OnAddItem);
        removeItemListener = new GameEventListener<ItemRef>(OnRemoveItem);

        addItemEvent.AddListener(addItemListener);
        removeItemEvent.AddListener(removeItemListener);
    }

    private void Update() {
        if (notifications.Count > 0) {
            if (Time.time > notifications[0].duration) {
                notifications.RemoveAt(0);
                UpdateNotificationViews();
            }
        }
    }

    private void OnAddItem(ItemRef item) {
        if(!showNotifications) return;

        float duration = notificationDuration + Mathf.Min(notifications.Count, notificationViews.Length);
        NotificationInfo notification = new NotificationInfo(item.ReferencedItem.Icon, $"{item.ReferencedItem.Name} added: {item.Amount}", Time.time + duration);
        notifications.Add(notification);
        UpdateNotificationViews();
    }

    private void OnRemoveItem(ItemRef item) {
        float duration = notificationDuration + Mathf.Min(notifications.Count, notificationViews.Length);
        NotificationInfo notification = new NotificationInfo(item.ReferencedItem.Icon, $"{item.ReferencedItem.Name} discarded: {item.Amount}", Time.time + duration);
        notifications.Add(notification);
        UpdateNotificationViews();
    }

    private void UpdateNotificationViews() {
        int maxIndex = Mathf.Min(notifications.Count, notificationViews.Length);
        int index = 0;
        for (; index < maxIndex; ++index)
            notificationViews[index].Show(notifications[index]);

        for (; index < notificationViews.Length; ++index)
            notificationViews[index].Hide();

        view.SetActive(notifications.Count > 0);
    }

    private void OnDestroy() {
        addItemEvent.RemoveListener(addItemListener);
        removeItemEvent.RemoveListener(removeItemListener);
    }
}
