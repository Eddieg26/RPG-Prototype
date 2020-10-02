using UnityEngine;

[System.Serializable]
public class OrbitCameraState {
    [SerializeField] private float targetHeight = 2f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float xSpeed = 5f;
    [SerializeField] private float ySpeed = 5f;
    [SerializeField] private float yMinLimit = -20f;
    [SerializeField] private float yMaxLimit = 80f;
    [SerializeField] private float distanceMin = 1f;
    [SerializeField] private float distanceMax = 15f;

    private float horizontal;
    private float vertical;
    private float currentDistance;
    private Vector3 targetPosition;

    public void Init(Transform transform) {
        Vector3 angles = transform.eulerAngles;
        horizontal = angles.y;
        vertical = angles.x;
        currentDistance = distance;
    }

    public void Update(Transform transform, LayerMask collisionLayers) {
        vertical = ClampAngle(vertical, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(vertical, horizontal, 0);

        distance = Mathf.Clamp(distance, distanceMin, distanceMax);
        float trueDistance = distance;

        Vector3 targetOffset = Vector3.up * targetHeight;
        Vector3 position = targetPosition + (rotation * new Vector3(0f, 0f, -trueDistance) + targetOffset);

        RaycastHit hit;
        bool isCorrected = false;
        if (Physics.Linecast(targetPosition + targetOffset, position, out hit, collisionLayers)) {
            trueDistance = Vector3.Distance(targetPosition, hit.point) - 0.15f;
            isCorrected = true;
        }

        currentDistance = !isCorrected || trueDistance > currentDistance ? Mathf.Lerp(currentDistance, trueDistance, Time.deltaTime * 5f) : trueDistance;

        Vector3 negDistance = new Vector3(0.0f, 0.0f, -currentDistance);
        position = targetPosition + (rotation * negDistance + targetOffset);

        transform.rotation = rotation;
        transform.position = position;
    }

    public void Set(float horizontal, float vertical, Vector3 targetPosition) {
        this.horizontal += horizontal * xSpeed;
        this.vertical -= vertical * ySpeed;

        this.targetPosition = targetPosition;
    }

    private static float ClampAngle(float angle, float min, float max) {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
