using UnityEngine;

namespace StateMachineModule {
    public class DeathState : BaseState {
        private SetAnimTriggerAction deathAction;

        public DeathState(StateBlackboard blackboard, SetAnimTriggerAction deathAction, int priority = 0, bool repeatable = false) : base(blackboard, priority, repeatable) {
            this.deathAction = deathAction;
        }

        public override void Enter() {
            deathAction.Start();
        }

        public override void Update() {
            deathAction.Execute();
        }

        public override void Exit() {
            deathAction.Finish();
        }

        public static DeathState Create(StateBlackboard blackboard, Animator animator, string deathTrigger, int priority) {
            SetAnimTriggerAction deathAction = new SetAnimTriggerAction(blackboard, animator, deathTrigger);
            return new DeathState(blackboard, deathAction, priority);
        }
    }
}
