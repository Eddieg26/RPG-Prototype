using UnityEngine;
using UnityEngine.Events;

public class InputAction {
    [SerializeField] private UnityAction start;
    [SerializeField] private UnityAction end;

    public UnityAction Start {
        get { return start; }
        set { start = value; }
    }

    public UnityAction End {
        get { return end; }
        set { end = value; }
    }
}
