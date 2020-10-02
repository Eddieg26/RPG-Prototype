using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "InputManager", menuName = "Game/Input/InputManager")]
public class InputManager : ScriptableObject {
    [SerializeField] private GameAction setPlayerInputControllerAction;
    private InputController controller;

    public void SetInputController(InputController controller) {
        if (controller != null && !controller.Unrestricted) {
            if (this.controller)
                this.controller.LoseControl();

            this.controller = controller;
            this.controller.GainControl();
        }
    }

    public void SetPlayerInputController() {
        setPlayerInputControllerAction.Invoke();
    }
}
