using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class InputBinding {
    [SerializeField] private string axisName;
    [SerializeField] private KeyCode buttonOrKey;
    [SerializeField] private InputBindingType bindingType;
    [SerializeField] private bool invokeInFixedUpdate = false;

    private InputAction action = new InputAction();
    private float value = 0f;

    public string AxisName {
        get { return axisName; }
        set { axisName = value; }
    }

    public KeyCode ButtonOrKey {
        get { return buttonOrKey; }
        set { buttonOrKey = value; }
    }

    public InputBindingType BindingType {
        get { return bindingType; }
        set { bindingType = value; }
    }

    public bool InvokeInFixedUpdate {
        get { return invokeInFixedUpdate; }
        set { invokeInFixedUpdate = value; }
    }

    public InputAction Action {
        get { return action; }
        set { action = value; }
    }

    public float Value {
        get { return value; }
        set { this.value = value; }
    }

    public bool CheckInput() {
        bool activate = false;

        switch (bindingType) {
            case InputBindingType.Down:
                activate = Input.GetKeyDown(buttonOrKey);
                break;
            case InputBindingType.Up:
                activate = Input.GetKeyUp(buttonOrKey);
                break;
            case InputBindingType.Hold:
                activate = Input.GetKey(buttonOrKey);
                break;
            case InputBindingType.Axis:
                value = Input.GetAxis(axisName);
                activate = Mathf.Abs(value) > InputConstants.INPUT_DEAD_ZONE;
                break;
        }

        return activate;
    }

    public void Copy(InputBinding inputBinding) {
        AxisName = inputBinding.AxisName;
        BindingType = inputBinding.BindingType;
        ButtonOrKey = inputBinding.ButtonOrKey;
        invokeInFixedUpdate = inputBinding.invokeInFixedUpdate;
        value = inputBinding.value;
    }

    public InputBinding GetCopy() {
        InputBinding binding = new InputBinding();
        binding.AxisName = axisName;
        binding.bindingType = bindingType;
        binding.buttonOrKey = buttonOrKey;
        binding.invokeInFixedUpdate = invokeInFixedUpdate;
        return binding;
    }
}
