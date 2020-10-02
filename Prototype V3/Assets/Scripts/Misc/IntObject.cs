using UnityEngine;

[CreateAssetMenu(fileName = "New IntObject", menuName = "Game/Misc/IntObject")]
public class IntObject : ScriptableObject {
    [SerializeField] private int value;

    public int Value {
        get { return value; }
        set { this.value = value; }
    }
}
