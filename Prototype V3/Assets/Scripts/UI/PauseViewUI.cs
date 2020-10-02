using UnityEngine;
using UnityEngine.UI;
using UIElements;
using System.Collections.Generic;

public class PauseViewUI : MonoBehaviour {
    private const string EMPTY_TITLE = "Empty";

    [SerializeField] private GameObject view;
    [SerializeField] private Text mainTitleText;
    [SerializeField] private ViewTitleUI leftViewTitle;
    [SerializeField] private ViewTitleUI rightViewTitle;
    [SerializeField] private Text goldText;
    [SerializeField] private IntObject goldObject;
    [SerializeField] private GameEvent registerViewEvent;
    [SerializeField] private GameAction openPauseViewAction;
    [SerializeField] private GameAction closePauseViewAction;

    private List<IViewUI> viewList = new List<IViewUI>();
    private int currentViewIndex;

    private GameEventListener openPauseViewListener;
    private GameEventListener closePauseViewListener;
    private GameEventListener<RegisterViewData> registerViewListener;

    private void Awake() {
        registerViewListener = new GameEventListener<RegisterViewData>(RegisterView);
        openPauseViewListener = new GameEventListener(Open);
        closePauseViewListener = new GameEventListener(Close);

        registerViewEvent.AddListener(registerViewListener);
        openPauseViewAction.AddListener(openPauseViewListener);
        closePauseViewAction.AddListener(closePauseViewListener);
    }

    private void Open() {
        view.SetActive(true);
        currentViewIndex = 0;
        if (currentViewIndex < viewList.Count) {
            if (viewList[currentViewIndex] != null)
                viewList[currentViewIndex].Open();
            UpdateViewTitles();
        }

        goldText.text = goldObject.Value.ToString("N0");
    }

    private void Close() {
        if (viewList.Count > 0 && currentViewIndex < viewList.Count) {
            if (viewList[currentViewIndex] != null)
                viewList[currentViewIndex].Close();
        }

        view.SetActive(false);
    }

    public void NextView() {
        if (currentViewIndex < viewList.Count - 1) {
            if (viewList[currentViewIndex] != null)
                viewList[currentViewIndex].Close();

            ++currentViewIndex;

            if (viewList[currentViewIndex] != null)
                viewList[currentViewIndex].Open();

            UpdateViewTitles();
        }
    }

    public void PrevView() {
        if (currentViewIndex > 0) {
            if (viewList[currentViewIndex] != null)
                viewList[currentViewIndex].Close();

            --currentViewIndex;

            if (viewList[currentViewIndex] != null)
                viewList[currentViewIndex].Open();

            UpdateViewTitles();
        }
    }

    private void UpdateViewTitles() {
        if (viewList.Count == 1) {
            leftViewTitle.Disable();
            rightViewTitle.Disable();
        } else if (currentViewIndex == 0) {
            leftViewTitle.Disable();
            EnableViewTitle(rightViewTitle, GetNextViewUI());
        } else if (currentViewIndex == viewList.Count - 1) {
            rightViewTitle.Disable();
            EnableViewTitle(leftViewTitle, GetPrevViewUI());
        } else {
            EnableViewTitle(rightViewTitle, GetNextViewUI());
            EnableViewTitle(leftViewTitle, GetPrevViewUI());
        }

        SetMainTitle(viewList[currentViewIndex]);
    }

    private void EnableViewTitle(ViewTitleUI viewTitleUI, IViewUI viewUI) {
        viewTitleUI.Enable();

        string title = viewUI != null ? viewUI.GetTitle() : EMPTY_TITLE;
        viewTitleUI.SetTitle(title);
    }

    private void SetMainTitle(IViewUI viewUI) {
        string title = viewUI != null ? viewUI.GetTitle() : EMPTY_TITLE;
        mainTitleText.text = title;
    }

    private IViewUI GetNextViewUI() {
        int index = currentViewIndex + 1;
        return index < viewList.Count ? viewList[index] : null;
    }

    private IViewUI GetPrevViewUI() {
        int index = currentViewIndex - 1;
        return index < viewList.Count && index >= 0 ? viewList[index] : null;
    }

    private void RegisterView(RegisterViewData data) {
        while (data.Index >= viewList.Count)
            viewList.Add(null);

        viewList[data.Index] = data.View;
    }

    private void OnDestroy() {
        registerViewEvent.RemoveListener(registerViewListener);
        openPauseViewAction.RemoveListener(openPauseViewListener);
        closePauseViewAction.RemoveListener(closePauseViewListener);
    }
}
