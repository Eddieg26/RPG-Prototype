using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PauseManagerInputConfig", menuName = "Game/Input/PauseManager")]
public class PauseManagerInputConfig : InputConfiguration {
    [SerializeField] private InputBinding pauseBinding;

    public override RuntimeInputConfiguration GetRuntimeInputConfiguration() {
        RuntimeInputConfiguration runtimeInputConfiguration = new RuntimeInputConfiguration();

        runtimeInputConfiguration.AddInputBinding(InputBindingName.TOGGLE_PAUSE, pauseBinding.GetCopy());

        return runtimeInputConfiguration;
    }

    public override void SetDefaultBindings(RuntimeInputConfiguration runtimeConfig) {
        InputBinding pauseBinding = runtimeConfig.GetBinding(InputBindingName.TOGGLE_PAUSE);
        pauseBinding.Copy(this.pauseBinding);
    }
}
