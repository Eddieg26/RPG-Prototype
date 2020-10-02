using UnityEngine;

namespace StateMachineModule {
    public class PlayerIdleState : BaseState {
        private SetAnimTriggerAction setAnimTriggerAction;
        private LookAtTargetAction lookAtAction;

        public PlayerIdleState(StateBlackboard blackboard, SetAnimTriggerAction setAnimTriggerAction, LookAtTargetAction lookAtAction, int priority = 0, bool repeatable = false) : base(blackboard, priority, repeatable) {
            this.setAnimTriggerAction = setAnimTriggerAction;
            this.lookAtAction = lookAtAction;
        }

        public override void Enter() {
            setAnimTriggerAction.Start();
            lookAtAction.Execute();
        }

        public override void Update() {
        }

        public override void Exit() {}
    }
}
