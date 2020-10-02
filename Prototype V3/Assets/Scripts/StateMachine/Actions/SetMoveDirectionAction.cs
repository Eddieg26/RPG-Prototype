using UnityEngine;

namespace StateMachineModule {
    public class SetMoveDirectionAction : BaseAction {
        private Transform targetTransorm;
        private Transform cameraTransform;
        private float horizontal;
        private float vertical;

        public SetMoveDirectionAction(StateBlackboard blackboard, Transform targetTransorm, Transform cameraTransform) : base(blackboard) {
            this.targetTransorm = targetTransorm;
            this.cameraTransform = cameraTransform;
        }

        public override void Start() {
            horizontal = blackboard.Get<float>(BlackboardKey.Horizontal);
            vertical = blackboard.Get<float>(BlackboardKey.Vertical);
        }

        public override ActionStatus Execute() {
            horizontal = blackboard.Get<float>(BlackboardKey.Horizontal);
            vertical = blackboard.Get<float>(BlackboardKey.Vertical);

            Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
            forward.y = 0;
            forward = forward.normalized;

            Vector3 right = new Vector3(forward.z, 0, -forward.x);

            Vector3 targetDirection = horizontal * right + vertical * forward;

            if (targetDirection != Vector3.zero) {
                Vector3 moveDirection = targetDirection;
                moveDirection = moveDirection.normalized;

                targetTransorm.rotation = Quaternion.LookRotation(moveDirection);
                blackboard.Set<Vector3>(BlackboardKey.MoveDirection, moveDirection);
            }

            return ActionStatus.Finished;
        }

        public override void Finish() { }
    }

}