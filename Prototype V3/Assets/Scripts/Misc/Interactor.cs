using UnityEngine;
using System.Collections.Generic;

public class Interactor : MonoBehaviour {
    [SerializeField] private GameAction hideTooltipAction;
    [SerializeField] private GameEvent showTooltipEvent;

    private Interactable currentInteractable;
    private List<Interactable> interactableList = new List<Interactable>();

    private const float facingThreshold = 0.6f;

    public void Interact() {
        if (currentInteractable != null && currentInteractable.CanInteract()) {
            currentInteractable.Interact(this);

            if (!currentInteractable.CanInteract()) {
                interactableList.Remove(currentInteractable);
                if (interactableList.Count == 0)
                    hideTooltipAction.Invoke();
            }

            currentInteractable = null;
        }
    }

    private void Update() {
        Interactable newInteractable = GetCurrentInteractable();
        if (newInteractable != null && newInteractable != currentInteractable) {
            currentInteractable = newInteractable;
            showTooltipEvent.Invoke(currentInteractable.GetTooltip());
        }
    }

    private void OnTriggerEnter(Collider other) {
        Interactable interactable = GetInteractableFromTransform(other.transform);
        if (interactable != null && !interactableList.Contains(interactable) && interactable.CanInteract())
            interactableList.Add(interactable);
    }

    private void OnTriggerExit(Collider other) {
        Interactable interactable = GetInteractableFromTransform(other.transform);
        if (interactable != null && interactableList.Contains(interactable))
            interactableList.Remove(interactable);

        if (interactable == currentInteractable)
            currentInteractable = null;

        if (interactableList.Count == 0)
            hideTooltipAction.Invoke();
    }

    private Interactable GetCurrentInteractable() {
        if (interactableList.Count == 0)
            return null;

        Interactable interactable = interactableList[0];
        float facingDir = GetFacingDirection(interactable.GetPosition());

        for (int index = 1; index < interactableList.Count; ++index) {
            float currentFacingDir = GetFacingDirection(interactableList[index].GetPosition());
            if (currentFacingDir > facingThreshold && currentFacingDir > facingDir) {
                interactable = interactableList[index];
                facingDir = currentFacingDir;
            }
        }

        return interactable;
    }

    private float GetFacingDirection(Vector3 interactablePosition) {
        Vector3 dir = (interactablePosition - transform.root.position).normalized;
        return Vector3.Dot(transform.root.forward, dir);
    }

    private Interactable GetInteractableFromTransform(Transform otherTransform) {
        Interactable retValue = null;

        while(otherTransform != null && retValue == null) {
            retValue = otherTransform.GetComponent<Interactable>();
            if(retValue == null)
                otherTransform = otherTransform.parent;
        }

        return retValue;
    }
}
