using UnityEngine;
using System.Collections.Generic;

public class Chest : MonoBehaviour, Interactable {
    private const string OPEN_TRIGGER = "Open";
    private const string TOOL_TIP = "Open Chest";

    [SerializeField] private Animator animator;
    [SerializeField] private List<ItemRef> items;
    [SerializeField] private GameEvent openChestEvent;
    [SerializeField] private AudioPlayer audioPlayer;
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip giveItemsClip;

    private Inventory targetInventory;

    public void Interact(Interactor interactor) {
        SetTargetInventory(interactor.transform);
        if (targetInventory != null) {
            items.ForEach((item) => targetInventory.AddItem(item));
            openChestEvent.Invoke<List<ItemRef>>(items);
            items.Clear();
            animator.SetTrigger(OPEN_TRIGGER);
            PlayAudio(openClip);
            PlayAudio(giveItemsClip);
        }
    }

    public bool CanInteract() {
        return items.Count > 0;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public string GetTooltip() {
        return TOOL_TIP;
    }

    private void PlayAudio(AudioClip clip) {
        if (audioPlayer != null && clip != null)
            audioPlayer.PlayOneShot(clip);
    }

    private void SetTargetInventory(Transform targetTransform) {
        targetInventory = targetTransform.GetComponent<Inventory>();
        if (targetInventory == null && targetTransform.root != null)
            targetInventory = targetTransform.root.GetComponent<Inventory>();
    }
}
