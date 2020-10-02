using UnityEngine;

[CreateAssetMenu(fileName = "InventoryInputConfig", menuName = "Game/Input/Inventory")]
public class InventoryInputConfig : InputConfiguration {
    [SerializeField] private InputBinding equipBinding;

    public override RuntimeInputConfiguration GetRuntimeInputConfiguration() {
        RuntimeInputConfiguration runtimeConfig = new RuntimeInputConfiguration();

        runtimeConfig.AddInputBinding(InputBindingName.PLAYER_INV_USEOREQUIP, equipBinding.GetCopy());

        return runtimeConfig;
    }

    public override void SetDefaultBindings(RuntimeInputConfiguration runtimeConfig) {
        InputBinding equipBinding = runtimeConfig.GetBinding(InputBindingName.PLAYER_INV_USEOREQUIP);

        equipBinding.Copy(this.equipBinding);
    }
}
