using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private OrbitCameraState orbitState;
    [SerializeField] private LookFrameState lookFrameState;
    [SerializeField] private CameraStateType cameraStateType;
    [SerializeField] LayerMask collisionLayers = -1;

    private Vector3 startOffset;
    private Vector3 targetPosition;

    private void Awake() {
        orbitState.Init(transform);
        startOffset = transform.position;
    }

    private void LateUpdate() {
        // if (cameraStateType == CameraStateType.Orbit)
        //     orbitState.Update(transform, collisionLayers);
        // else if (cameraStateType == CameraStateType.LookFrame)
        //     lookFrameState.Update(transform);

        transform.position = Vector3.Lerp(transform.position, startOffset + targetPosition, Time.deltaTime * 5f);
    }

    public void SetCameraStateType(CameraStateType cameraStateType) {
        this.cameraStateType = cameraStateType;
        if (cameraStateType == CameraStateType.Orbit)
            orbitState.Init(transform);
    }

    public void UpdateCamera(float horizontal, float vertical, Vector3 targetPosition, Vector3 lookTargetPosition) {
        orbitState.Set(horizontal, vertical, targetPosition);
        lookFrameState.Set(lookTargetPosition, targetPosition);

        this.targetPosition = targetPosition;
    }
}
