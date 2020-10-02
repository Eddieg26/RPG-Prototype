using UnityEngine;

public interface Interactable {
    void Interact(Interactor interactor);
    bool CanInteract();
    Vector3 GetPosition();
    string GetTooltip();
}
