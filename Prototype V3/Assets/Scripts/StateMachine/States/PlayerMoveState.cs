using UnityEngine;

namespace StateMachineModule {
    public class PlayerMoveState : BaseState {
        private SetAnimTriggerAction setRunTriggerAction;
        private SetMoveDirectionAction setMoveDirAction;
        private MoveControllerAction moveControllerAction;

        public PlayerMoveState(StateBlackboard blackboard, SetAnimTriggerAction setRunTriggerAction, SetMoveDirectionAction setMoveDirAction, MoveControllerAction moveControllerAction, int priority = 0, bool repeatable = false) : base(blackboard, priority, repeatable) {
            this.setRunTriggerAction = setRunTriggerAction;
            this.setMoveDirAction = setMoveDirAction;
            this.moveControllerAction = moveControllerAction;
        }

        public override void Enter() {
            setRunTriggerAction.Start();
            setMoveDirAction.Start();
        }

        public override void Update() {
            moveControllerAction.Execute();
        }

        public override void LateUpdate() {
            setMoveDirAction.Execute();
        }

        public override void Exit() { }

    }
}
