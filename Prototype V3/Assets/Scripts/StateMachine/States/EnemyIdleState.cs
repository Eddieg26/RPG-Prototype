using UnityEngine;
using System.Collections.Generic;

namespace StateMachineModule {
    public class EnemyIdleState : BaseState {
        private SetAnimTriggerAction idleAction;

        public EnemyIdleState(StateBlackboard blackboard, SetAnimTriggerAction idleAction, int priority = 0, bool repeatable = false) : base(blackboard, priority, repeatable) {
            this.idleAction = idleAction;
        }

        public override void Enter() {
            idleAction.Execute();
        }

        public override void Update() {
            idleAction.Execute();
        }

        public override void Exit() {
            idleAction.Finish();
        }

        public static EnemyIdleState Create(StateBlackboard blackboard, Animator animator, string idleTrigger, int priority) {
            SetAnimTriggerAction idleAction = new SetAnimTriggerAction(blackboard, animator, idleTrigger);
            return new EnemyIdleState(blackboard, idleAction, priority);
        }
    }
}
