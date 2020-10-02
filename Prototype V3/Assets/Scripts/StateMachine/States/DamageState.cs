using UnityEngine;

namespace StateMachineModule {
    public class DamageState : BaseState {
        private SetAnimTriggerAction damageAction;

        public DamageState(StateBlackboard blackboard, SetAnimTriggerAction damageAction, int priority = 0, bool repeatable = false) : base(blackboard, priority, repeatable) {
            this.damageAction = damageAction;
        }

        public override void Enter() {}

        public override void Update() {
            bool repeatDamage = blackboard.Get<bool>(BlackboardKey.RepeatDamageFlag);
            if (repeatDamage) {
                damageAction.Start();
                blackboard.Set<bool>(BlackboardKey.RepeatDamageFlag, false);
            }
        }

        public override void Exit() {
            damageAction.Finish();
        }

        public override void OnAnimationEvent() {
            damageAction.OnAnimationEvent();
            blackboard.Set<bool>(BlackboardKey.DamageFlag, false);
        }

        public static DamageState Create(StateBlackboard blackboard, Animator animator, string damageTrigger, int priority) {
            SetAnimTriggerAction damageAction = new SetAnimTriggerAction(blackboard, animator, damageTrigger, true);
            return new DamageState(blackboard, damageAction, priority);
        }
    }
}
