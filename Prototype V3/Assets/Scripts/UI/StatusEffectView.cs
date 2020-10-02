using UnityEngine;
using UnityEngine.UI;

public class StatusEffectView : MonoBehaviour {
    [SerializeField] private GameObject view;
    [SerializeField] private Image icon;
    [SerializeField] private Image fillImage;

    public void UpdateView(StatusEffectRef statusEffectRef) {
        view.SetActive(true);
        icon.gameObject.SetActive(true);
        fillImage.gameObject.SetActive(true);

        icon.sprite = statusEffectRef.Effect.Icon;

        float startTime = statusEffectRef.DurationTimer;
        float currentTime = Time.time - startTime;

        fillImage.fillAmount = currentTime / Mathf.Max(statusEffectRef.Effect.Duration, 1f);
    }

    public void Clear() {
        view.SetActive(false);
        icon.gameObject.SetActive(false);
        fillImage.gameObject.SetActive(false);
    }
}
