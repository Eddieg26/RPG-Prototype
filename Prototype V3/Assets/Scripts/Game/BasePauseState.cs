using UnityEngine;

public enum PauseState {
    UnPaused = 0,
    Paused = 1,
    ForcePaused = 2,

    MAX
}

public abstract class BasePauseState {

    public abstract void Invoke();
}

