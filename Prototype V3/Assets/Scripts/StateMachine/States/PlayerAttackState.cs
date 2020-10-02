using UnityEngine;

namespace StateMachineModule {
    public class PlayerAttackState : BaseState {
        private SetAnimTriggerAction setAttackTrigger;
        private SetAnimTriggerAction setNextAttackTrigger;
        private LookAtTargetAction lookAtAction;
        private float maxAttackTimer = 0.5f;
        private float currentAttackTimer = 0f;
        private int currentAttack = 0;
        private int attackCount = 3;

        public PlayerAttackState(StateBlackboard blackboard, SetAnimTriggerAction setAttackTrigger, SetAnimTriggerAction setNextAttackTrigger, LookAtTargetAction lookAtAction, int priority = 0, bool repeatable = false) : base(blackboard, priority, repeatable) {
            this.setAttackTrigger = setAttackTrigger;
            this.setNextAttackTrigger = setNextAttackTrigger;
            this.lookAtAction = lookAtAction;
        }

        public override void Enter() {
            setAttackTrigger.Start();
            currentAttackTimer = Time.time;
            currentAttack = 0;
            lookAtAction.Execute();
        }

        public override void Update() {
            bool nextAttack = blackboard.Get<bool>(BlackboardKey.NextAttackFlag);

            if (Time.time > currentAttackTimer + maxAttackTimer && nextAttack) {
                if (currentAttack < attackCount) {
                    lookAtAction.Execute();
                    setNextAttackTrigger.Start();
                }

                ++currentAttack;
                blackboard.Set<bool>(BlackboardKey.NextAttackFlag, false);
                currentAttackTimer = Time.time;
            }
        }

        public override void Exit() { }

        public override void OnAnimationEvent() {
            blackboard.Set<bool>(BlackboardKey.AttackingFlag, false);
            blackboard.Set<bool>(BlackboardKey.NextAttackFlag, false);
        }
    }
}
