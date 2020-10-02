using UnityEngine;

namespace StateMachineModule {
    public class LookAtTargetAction : BaseAction {
        Transform transform;

        public LookAtTargetAction(StateBlackboard blackboard, Transform transform) : base(blackboard) {
            this.transform = transform;
        }

        public override void Start() { }

        public override ActionStatus Execute() {
            Transform lookTarget = blackboard.Get<Transform>(BlackboardKey.Target);
            if (lookTarget != null) {
                Vector3 lookDir = new Vector3(lookTarget.position.x, transform.position.y, lookTarget.position.z);
                transform.LookAt(lookDir);
            }

            return ActionStatus.Finished;
        }

        public override void Finish() { }
    }
}
