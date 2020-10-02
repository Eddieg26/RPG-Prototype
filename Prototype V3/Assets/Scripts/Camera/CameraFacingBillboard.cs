using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour {
    private Transform cameraTransform;

    private void Start() {
        cameraTransform = Camera.main ? Camera.main.transform : null;
    }

    private void LateUpdate() {
        if (cameraTransform) {
            Vector3 lookPos = transform.position + cameraTransform.rotation * Vector3.forward;
            transform.LookAt(lookPos, cameraTransform.rotation * Vector3.up);
        }
    }
}
