using UnityEngine;

namespace StateMachineModule {
    public class SetAnimFloatAction : BaseAction {
        private Animator animator;
        private string paramName;
        private BlackboardKey targetKey;
        private bool delayFinish;

        private ActionStatus delayedStatus;

        public SetAnimFloatAction(StateBlackboard blackboard, Animator animator, float value, string paramName, BlackboardKey targetKey, bool delayFinish = false) : base(blackboard) {
            this.animator = animator;
            this.paramName = paramName;
            this.targetKey = targetKey;
            this.delayFinish = delayFinish;
        }

        public override void Start() {
            float value = blackboard.Get<float>(targetKey);
            animator.SetFloat(paramName, value);
            delayedStatus = ActionStatus.Running;
        }

        public override ActionStatus Execute() {
            return delayFinish ? delayedStatus : ActionStatus.Finished;
        }

        public override void Finish() { }

        public override void OnAnimationEvent() {
            delayedStatus = ActionStatus.Finished;
        }

    }
}
