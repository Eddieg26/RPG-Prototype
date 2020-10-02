using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PageButtonUI : MonoBehaviour {
    private int pageIndex;

    public UnityAction<int> SelectPageCallback { get; set; }

    private void Start() {
        SetPageIndex();
    }

    public void OnSelect() {
        if(SelectPageCallback != null)
            SelectPageCallback(pageIndex);
    }

    private void SetPageIndex() {
        if (transform.parent != null) {
            for (int index = 0; index < transform.parent.childCount; ++index) {
                if (transform.parent.GetChild(index) == transform) {
                    pageIndex = index;
                    break;
                }
            }
        }
    }
}
