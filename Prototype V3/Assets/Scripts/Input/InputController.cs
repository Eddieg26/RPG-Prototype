using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class InputController : MonoBehaviour {
    [SerializeField] private InputConfiguration inputConfiguration;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private bool gainControlOnStart;
    [SerializeField] private bool unrestricted;

    private bool hasControl;
    public bool Unrestricted { get { return unrestricted; } }
    private RuntimeInputConfiguration runtimeInputConfiguration;
    private List<InputAction> fixedUpdateActions = new List<InputAction>();
    private List<InputAction> startedActions = new List<InputAction>();
    private List<InputAction> fixedUpdateFinishedActions = new List<InputAction>();

    public InputManager InputManager { get { return inputManager; } }
    public UnityAction GainControlAction { get; set; }
    public UnityAction LoseControlAction { get; set; }

    private void Start() {
        if (gainControlOnStart)
            SetAsFocusedController();
    }

    public void SetAsFocusedController() {
        // An unrestricted input controller cannot be set as the focused input controller
        if (!unrestricted)
            inputManager.SetInputController(this);
    }

    public InputConfiguration GetInputConfig() {
        return inputConfiguration;
    }

    public RuntimeInputConfiguration GetRuntimeInputConfig() {
        if (runtimeInputConfiguration == null)
            runtimeInputConfiguration = inputConfiguration.GetRuntimeInputConfiguration();

        return runtimeInputConfiguration;
    }

    public void GainControl() {
        hasControl = true;
        if (GainControlAction != null)
            GainControlAction();
    }

    public void LoseControl() {
        hasControl = false;
        if (LoseControlAction != null)
            LoseControlAction();
    }

    private void Update() {
        if (!hasControl && !unrestricted || !inputConfiguration)
            return;

        List<InputBinding> bindings = GetRuntimeInputConfig().GetInputBindings();

        bindings.ForEach(binding => {
            bool activate = false;

            activate = binding.CheckInput();

            if (activate) {
                if (!startedActions.Contains(binding.Action)) {
                    startedActions.Add(binding.Action);
                }

                if (binding.InvokeInFixedUpdate)
                    fixedUpdateActions.Add(binding.Action);
                else {
                    if (binding.Action.Start != null)
                        binding.Action.Start.Invoke();
                }
            } else {
                if (startedActions.Contains(binding.Action)) {
                    startedActions.Remove(binding.Action);

                    if (binding.InvokeInFixedUpdate)
                        fixedUpdateFinishedActions.Add(binding.Action);
                    else {
                        if (binding.Action.End != null)
                            binding.Action.End.Invoke();
                    }
                }
            }
        });
    }

    private void FixedUpdate() {
        if (!hasControl && !unrestricted)
            return;

        fixedUpdateActions.ForEach(action => {
            if (action.Start != null)
                action.Start.Invoke();
        });
        fixedUpdateActions.Clear();

        fixedUpdateFinishedActions.ForEach(action => {
            if (action.End != null)
                action.End.Invoke();
        });
        fixedUpdateFinishedActions.Clear();
    }
}
