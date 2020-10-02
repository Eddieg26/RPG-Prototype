using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace UIElements {
    [System.Serializable]
    public class PageView {
        [SerializeField] private RectTransform pageButtonsHolder;

        public void SetSelectPageCallback(UnityAction<int> selectPageCallback) {
            PageButtonUI[] pageButtons = pageButtonsHolder.GetComponentsInChildren<PageButtonUI>(true);
            foreach (var pageButton in pageButtons)
                pageButton.SelectPageCallback = selectPageCallback;
        }

        public void Update(int pageCount) {
            int index = 0;
            int maxIndex = Mathf.Min(pageCount, pageButtonsHolder.childCount);
            for (; index < maxIndex; ++index)
                pageButtonsHolder.GetChild(index).gameObject.SetActive(true);

            for (; index < pageButtonsHolder.childCount; ++index)
                pageButtonsHolder.GetChild(index).gameObject.SetActive(false);
        }

        public void Clear() {
            for (int index = 0; index < pageButtonsHolder.childCount; ++index)
                pageButtonsHolder.GetChild(index).gameObject.SetActive(false);
        }
    }

    [System.Serializable]
    public class ViewTitleUI {
        [SerializeField] private GameObject view;
        [SerializeField] private GameObject button;
        [SerializeField] private Text titleText;

        public void Enable() {
            view.SetActive(true);
        }

        public void Disable() {
            view.SetActive(false);
        }

        public void SetTitle(string title) {
            titleText.text = title;
        }
    }

    [System.Serializable]
    public class RewardItemView {
        [SerializeField] private GameObject view;
        [SerializeField] private Image iconImage;
        [SerializeField] private Text amountText;

        public void SetView(ItemRef item) {
            this.view.SetActive(true);
            this.iconImage.gameObject.SetActive(true);
            this.iconImage.sprite = item.ReferencedItem.Icon;
            this.amountText.text = $"x {item.Amount.ToString()}";
        }

        public void Clear() {
            this.view.SetActive(false);
            this.iconImage.gameObject.SetActive(false);
            this.iconImage.sprite = null;
            this.amountText.text = string.Empty;
        }
    }

    [System.Serializable]
    public class VolumeView {
        [SerializeField] private Text volumeText;
        [SerializeField] private Slider volumeSlider;

        public Slider VolumeSlider { get { return volumeSlider; } }

        public void SetVolumeText(string label, int volume) {
            volumeText.text = $"{label}: {volume}";
        }
    }
}
