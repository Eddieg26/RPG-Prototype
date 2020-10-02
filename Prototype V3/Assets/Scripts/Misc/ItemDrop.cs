using UnityEngine;
using System.Collections.Generic;

public class ItemDrop : MonoBehaviour, Interactable {
    [SerializeField] private GameObject mesh;

    private ItemRef item;
    private AudioPlayer audioPlayer;
    private Inventory targetInventory;

    private void Start() {
        audioPlayer = GetComponent<AudioPlayer>();
    }

    public bool CanInteract() {
        return item != null && item.ReferencedItem != null;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public string GetTooltip() {
        return item != null && item.ReferencedItem != null ? item.ReferencedItem.Name : string.Empty;
    }

    public void SetItem(ItemRef item) {
        this.item = item;
    }

    public void Interact(Interactor interactor) {
        SetTargetInventory(interactor.transform);
        if(item != null && item.ReferencedItem != null && targetInventory != null) {
            targetInventory.AddItem(item);
            targetInventory = null;
            SetItem(null);
            mesh.SetActive(false);
            Destroy(this.gameObject, 3f);
            audioPlayer.PlayClip();
        }
    }

    private void SetTargetInventory(Transform targetTransform) {
        targetInventory = targetTransform.GetComponent<Inventory>();
        if(targetInventory == null && targetTransform.root != null)
            targetInventory = targetTransform.root.GetComponent<Inventory>();
    }
}
