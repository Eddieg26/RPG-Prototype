using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

namespace StateMachineModule {
    public class MeleeAttackState : BaseState {
        private List<BaseAction> actions = new List<BaseAction>();
        private int currentIndex;
        private BaseAction currentAction;

        public MeleeAttackState(StateBlackboard blackboard, SetMeleeAttackAction setMeleeAttackAction, SetAnimTriggerAction walkAction, MoveToTargetAction moveToAction, LookAtTargetAction lookAtAction, SetAnimTriggerAction attackAction, int priority = 0, bool repeatable = false) : base(blackboard, priority, repeatable) {
            actions.Add(setMeleeAttackAction);
            actions.Add(walkAction);
            actions.Add(moveToAction);
            actions.Add(lookAtAction);
            actions.Add(attackAction);
        }

        public override void Enter() {
            actions[0].Execute();
            currentIndex = 0;
            currentAction = actions[currentIndex];
            currentAction.Start();
        }

        public override void Update() {
            if (currentAction.Execute() == ActionStatus.Finished)
                NextAction();
        }

        public override void Exit() {
            if (currentAction != null)
                currentAction.Finish();
        }

        public override void OnAnimationEvent() {
            currentAction.OnAnimationEvent();
        }

        private void NextAction() {
            currentAction.Finish();
            currentIndex = ((currentIndex + 1 + actions.Count) % actions.Count);
            if (currentIndex == 1 && InAttackRange())
                currentIndex = 3;

            currentAction = actions[currentIndex];
            currentAction.Start();
        }

        private bool InAttackRange() {
            Transform transform = blackboard.Get<Transform>(BlackboardKey.Transform);
            Transform target = blackboard.Get<Transform>(BlackboardKey.Target);
            float attackRange = blackboard.Get<float>(BlackboardKey.AttackRange);

            float distance = (transform.position - target.position).magnitude;
            return distance <= attackRange;
        }

        public static MeleeAttackState Create(StateBlackboard blackboard, Animator animator, NavMeshAgent navAgent, MeleeAttackData[] meleeAttacks, Transform transform, BlackboardKey attackRangeKey, BlackboardKey attackTriggerKey, SetAnimTriggerAction walkAction, int priority) {

            SetMeleeAttackAction setMeleeAttackAction = new SetMeleeAttackAction(blackboard, meleeAttacks);
            MoveToTargetAction moveToTargetAction = new MoveToTargetAction(blackboard, navAgent, attackRangeKey);
            LookAtTargetAction lookAtAction = new LookAtTargetAction(blackboard, transform);
            SetAnimTriggerAction attackAction = new SetAnimTriggerAction(blackboard, animator, attackTriggerKey, true);

            return new MeleeAttackState(blackboard, setMeleeAttackAction, walkAction, moveToTargetAction, lookAtAction, attackAction, priority);
        }
    }
}
