using UnityEngine;

public class PausedState : BasePauseState {
    private GameEvent togglePauseEvent;
    private GameAction openPauseViewAction;

    public PausedState(GameEvent togglePauseEvent, GameAction openPauseViewAction) {
        this.openPauseViewAction = openPauseViewAction;
        this.togglePauseEvent = togglePauseEvent;
    }

    public override void Invoke() {
        Time.timeScale = 0f;
        openPauseViewAction.Invoke();
        togglePauseEvent.Invoke(true);
    }
}