using UnityEngine;

namespace StateMachineModule {
    public class SetMeleeAttackAction : BaseAction {
        private MeleeAttackData[] meleeAttacks;

        public SetMeleeAttackAction(StateBlackboard blackboard, MeleeAttackData[] meleeAttacks) : base(blackboard) {
            this.meleeAttacks = meleeAttacks;
        }

        public override void Start() { }

        public override ActionStatus Execute() {
            if (meleeAttacks.Length > 0) {
                int attackIndex = Random.Range(0, meleeAttacks.Length);
                MeleeAttackData attackData = meleeAttacks[attackIndex];
                blackboard.Set<float>(BlackboardKey.AttackRange, attackData.AttackRange);
                blackboard.Set<string>(BlackboardKey.AnimTrigger, attackData.AnimatorTrigger);
            }

            return ActionStatus.Finished;
        }

        public override void Finish() { }
    }
}
