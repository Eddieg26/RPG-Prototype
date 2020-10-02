using UnityEngine;

namespace StateMachineModule {
    public class MoveControllerAction : BaseAction {
        private CharacterController controller;
        private float moveSpeed;

        public MoveControllerAction(StateBlackboard blackboard, CharacterController controller, float moveSpeed) : base(blackboard) {
            this.controller = controller;
            this.moveSpeed = moveSpeed;
        }

        public override void Start() {}

        public override ActionStatus Execute() {
            Vector3 moveDirection = blackboard.Get<Vector3>(BlackboardKey.MoveDirection);
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);

            return ActionStatus.Finished;
        }

        public override void Finish() {}
    }
}
