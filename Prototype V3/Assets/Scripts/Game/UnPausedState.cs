using UnityEngine;

public class UnPausedState : BasePauseState {
    private GameEvent togglePauseEvent;
    private GameAction closePauseViewAction;
    private InputController inputController;

    public UnPausedState(GameEvent togglePauseEvent, GameAction closePauseViewAction, InputController inputController) {
        this.togglePauseEvent = togglePauseEvent;
        this.closePauseViewAction = closePauseViewAction;
        this.inputController = inputController;
    }

    public override void Invoke() {
        Time.timeScale = 1f;
        closePauseViewAction.Invoke();
        inputController.InputManager.SetPlayerInputController();
        togglePauseEvent.Invoke(false);
    }
}
