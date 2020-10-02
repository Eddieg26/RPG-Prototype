using UnityEngine;

public class SelfDestruct : MonoBehaviour {
    [SerializeField] private float destroyTime = 5f;

    private void Start() {
        Destroy(this.gameObject, destroyTime);
    }
}
