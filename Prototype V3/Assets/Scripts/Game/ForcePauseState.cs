using UnityEngine;

public class ForcePauseState : BasePauseState {
    private GameEvent togglePauseEvent;

    public ForcePauseState(GameEvent togglePauseEvent) {
        this.togglePauseEvent = togglePauseEvent;
    }

    public override void Invoke() {
        Time.timeScale = 0f;
        togglePauseEvent.Invoke(true);
    }
}