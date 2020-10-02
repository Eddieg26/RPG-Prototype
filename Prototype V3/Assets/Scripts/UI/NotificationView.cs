using UnityEngine;
using UnityEngine.UI;

public class NotificationView : MonoBehaviour {
    [SerializeField] private Image iconImage;
    [SerializeField] private Text text;

    public void Show(NotificationInfo info) {
        gameObject.SetActive(true);
        iconImage.gameObject.SetActive(info.icon != null);
        iconImage.sprite = info.icon;
        text.text = info.info;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}

public class NotificationInfo {
    public Sprite icon;
    public string info;
    public float duration;

    public NotificationInfo(Sprite icon, string info, float duration) {
        this.icon = icon;
        this.info = info;
        this.duration = duration;
    }
}
