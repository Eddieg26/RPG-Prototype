using UnityEngine;

[System.Serializable]
public class LookFrameState {
    [SerializeField] private float distance = 10f;
    [SerializeField] private float height = 5f;
    [SerializeField] private float damping = 2f;

    private Vector3 direction;
    private Vector3 wantedPosition;
    private Vector3 lookTargetPosition;
    private Vector3 frameTargetPosition;

    public void Update(Transform transform) {
        direction = (frameTargetPosition - lookTargetPosition);

        wantedPosition = frameTargetPosition + (direction.normalized * distance);
        wantedPosition.y = wantedPosition.y + height;

        transform.position = Vector3.Lerp(transform.position, wantedPosition, damping * Time.deltaTime);

        transform.LookAt(lookTargetPosition);
    }

    public void Set(Vector3 lookTargetPosition, Vector3 frameTargetPosition) {
        this.lookTargetPosition = lookTargetPosition;
        this.frameTargetPosition = frameTargetPosition;
    }
}
