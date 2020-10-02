using UnityEngine;
using System.Collections.Generic;

public abstract class InputConfiguration : ScriptableObject {

    public abstract RuntimeInputConfiguration GetRuntimeInputConfiguration();
    public abstract void SetDefaultBindings(RuntimeInputConfiguration runtimeConfig);
}

public class RuntimeInputConfiguration {
    private Dictionary<InputBindingName, InputBinding> inputBindings = new Dictionary<InputBindingName, InputBinding>();
    
    public void AddInputBinding(InputBindingName name, InputBinding binding) {
        inputBindings.Add(name, binding);
    }

    public List<InputBinding> GetInputBindings() {
        List<InputBinding> bindings = new List<InputBinding>();

        foreach (var key in inputBindings.Keys)
            bindings.Add(inputBindings[key]);

        return bindings;
    }

    public InputBinding GetBinding(InputBindingName name) {
        return inputBindings[name];
    }
}
