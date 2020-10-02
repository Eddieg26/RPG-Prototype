using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplayConfig", menuName = "Game/Input/Gameplay")]
public class GameplayConfiguration : InputConfiguration {
    [SerializeField] private InputBinding horizontalBinding;
    [SerializeField] private InputBinding verticalBinding;
    [SerializeField] private InputBinding attackBinding;
    [SerializeField] private InputBinding camHorizontalBinding;
    [SerializeField] private InputBinding camVerticalBinding;
    [SerializeField] private InputBinding interactBinding;
    [SerializeField] private InputBinding toggleTargetingBinding;
    [SerializeField] private InputBinding nextTargetBinding;
    [SerializeField] private InputBinding primarySkillBinding;
    [SerializeField] private InputBinding secondarySkillBinding;

    public override RuntimeInputConfiguration GetRuntimeInputConfiguration() {
        RuntimeInputConfiguration runtimeInputConfiguration = new RuntimeInputConfiguration();

        runtimeInputConfiguration.AddInputBinding(InputBindingName.GAMEPLAY_HORIZONTAL, horizontalBinding.GetCopy());
        runtimeInputConfiguration.AddInputBinding(InputBindingName.GAMEPLAY_VERTICAL, verticalBinding.GetCopy());
        runtimeInputConfiguration.AddInputBinding(InputBindingName.GAMEPLAY_ATTACK, attackBinding.GetCopy());
        runtimeInputConfiguration.AddInputBinding(InputBindingName.GAMEPLAY_CAMHORIZONTAL, camHorizontalBinding.GetCopy());
        runtimeInputConfiguration.AddInputBinding(InputBindingName.GAMEPLAY_CAMVERTICAL, camVerticalBinding.GetCopy());
        runtimeInputConfiguration.AddInputBinding(InputBindingName.GAMEPLAY_INTERACT, interactBinding.GetCopy());
        runtimeInputConfiguration.AddInputBinding(InputBindingName.GAMEPLAY_TOGGLE_TARGETING, toggleTargetingBinding.GetCopy());
        runtimeInputConfiguration.AddInputBinding(InputBindingName.GAMEPLAY_NEXT_TARGET, nextTargetBinding.GetCopy());
        runtimeInputConfiguration.AddInputBinding(InputBindingName.GAMEPLAY_PRIMARY_SKILL, primarySkillBinding.GetCopy());
        runtimeInputConfiguration.AddInputBinding(InputBindingName.GAMEPLAY_SECONDARY_SKILL, secondarySkillBinding.GetCopy());

        return runtimeInputConfiguration;
    }

    public override void SetDefaultBindings(RuntimeInputConfiguration runtimeConfig) {
        InputBinding horizontalBinding = runtimeConfig.GetBinding(InputBindingName.GAMEPLAY_HORIZONTAL);
        InputBinding verticalBinding = runtimeConfig.GetBinding(InputBindingName.GAMEPLAY_VERTICAL);
        InputBinding attackBinding = runtimeConfig.GetBinding(InputBindingName.GAMEPLAY_ATTACK);
        InputBinding camHorizontal = runtimeConfig.GetBinding(InputBindingName.GAMEPLAY_CAMHORIZONTAL);
        InputBinding camVertical = runtimeConfig.GetBinding(InputBindingName.GAMEPLAY_CAMVERTICAL);
        InputBinding interactBinding = runtimeConfig.GetBinding(InputBindingName.GAMEPLAY_INTERACT);
        InputBinding toggleTargetingBinding = runtimeConfig.GetBinding(InputBindingName.GAMEPLAY_TOGGLE_TARGETING);
        InputBinding nextTargetBinding = runtimeConfig.GetBinding(InputBindingName.GAMEPLAY_NEXT_TARGET);
        InputBinding primarySkillBinding = runtimeConfig.GetBinding(InputBindingName.GAMEPLAY_PRIMARY_SKILL);
        InputBinding secondarySkillBinding = runtimeConfig.GetBinding(InputBindingName.GAMEPLAY_SECONDARY_SKILL);

        horizontalBinding.Copy(this.horizontalBinding);
        verticalBinding.Copy(this.verticalBinding);
        attackBinding.Copy(this.attackBinding);
        camHorizontal.Copy(this.camHorizontalBinding);
        camVertical.Copy(this.camVerticalBinding);
        interactBinding.Copy(this.interactBinding);
        toggleTargetingBinding.Copy(this.toggleTargetingBinding);
        nextTargetBinding.Copy(this.nextTargetBinding);
        primarySkillBinding.Copy(this.primarySkillBinding);
        secondarySkillBinding.Copy(this.secondarySkillBinding);
    }
}
