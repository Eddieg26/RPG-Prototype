using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachineModule {
    public class SetAnimTriggerAction : BaseAction {
        private Animator animator;
        private string trigger;
        private bool delayFinish;
        private bool isDynamic;
        private BlackboardKey triggerKey;

        private ActionStatus delayedStatus;

        public SetAnimTriggerAction(StateBlackboard blackboard, Animator animator, string trigger, bool delayFinish = false) : base(blackboard) {
            this.animator = animator;
            this.trigger = trigger;
            this.delayFinish = delayFinish;
            this.isDynamic = false;
        }

        public SetAnimTriggerAction(StateBlackboard blackboard, Animator animator, BlackboardKey triggerKey, bool delayFinish = false) : base(blackboard) {
            this.animator = animator;
            this.delayFinish = delayFinish;
            this.triggerKey = triggerKey;
            this.isDynamic = true;
        }

        public override void Start() {
            if (isDynamic)
                trigger = blackboard.Get<string>(triggerKey);

            animator.SetTrigger(trigger);
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
