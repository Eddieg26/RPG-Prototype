using UnityEngine;
using UnityEngine.UI;

public class InteractTooltipUI : MonoBehaviour {
    [SerializeField] private GameObject view;
    [SerializeField] private Text tooltipText;
    [SerializeField] private GameAction hideTooltipAction;
    [SerializeField] private GameEvent showTooltipEvent;

    private GameEventListener hideTooltipListener;
    private GameEventListener<string> showTooltipListener;

    private void Awake() {
        hideTooltipListener = new GameEventListener(HideTooltip);
        showTooltipListener = new GameEventListener<string>(ShowTooltip);

        hideTooltipAction.AddListener(hideTooltipListener);
        showTooltipEvent.AddListener(showTooltipListener);
    }

    private void Start() {
        HideTooltip();
    }

    private void HideTooltip() {
        view.SetActive(false);
    }

    private void ShowTooltip(string tooltip) {
        view.SetActive(true);
        tooltipText.text = tooltip;
    }

    private void OnDestroy() {
        hideTooltipAction.RemoveListener(hideTooltipListener);
        showTooltipEvent.RemoveListener(showTooltipListener);
    }
}
