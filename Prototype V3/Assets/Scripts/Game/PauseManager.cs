using UnityEngine;

public class PauseManager : MonoBehaviour {
    [SerializeField] private GameEvent togglePauseEvent;
    [SerializeField] private GameEvent forcePauseEvent;
    [SerializeField] private GameAction openPauseViewAction;
    [SerializeField] private GameAction closePauseViewAction;

    private InputController inputController;
    private GameEventListener<bool> forcePauseListener;
    private PauseState currentPauseState;
    private BasePauseState[] pauseStates = new BasePauseState[(int)PauseState.MAX];

    private void Awake() {
        inputController = GetComponent<InputController>();

        forcePauseListener = new GameEventListener<bool>(ToggleForcePause);
        forcePauseEvent.AddListener(forcePauseListener);

        pauseStates[(int)PauseState.UnPaused] = new UnPausedState(togglePauseEvent, closePauseViewAction, inputController);
        pauseStates[(int)PauseState.Paused] = new PausedState(togglePauseEvent, openPauseViewAction);
        pauseStates[(int)PauseState.ForcePaused] = new ForcePauseState(togglePauseEvent);
    }

    private void Start() {
        InitInputBindings();
    }

    public void TogglePause(PauseState pauseState) {
        currentPauseState = pauseState;
   
        pauseStates[(int)currentPauseState].Invoke();
    }

    public void ToggleForcePause(bool isPaused) {
        TogglePause(isPaused ? PauseState.ForcePaused : PauseState.UnPaused);
    }

    private void InitInputBindings() {
        RuntimeInputConfiguration runtimeConfig = inputController.GetRuntimeInputConfig();
        InputBinding pauseBinding = runtimeConfig.GetBinding(InputBindingName.TOGGLE_PAUSE);

        pauseBinding.Action.Start = () => {
            PauseState pauseState = currentPauseState == PauseState.UnPaused || currentPauseState == PauseState.ForcePaused ? PauseState.Paused : PauseState.UnPaused;
            TogglePause(pauseState);
        };
    }

    private void OnDestroy() {
        forcePauseEvent.RemoveListener(forcePauseListener);
    }
}
