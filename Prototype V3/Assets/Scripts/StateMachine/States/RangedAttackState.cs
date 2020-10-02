using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

namespace StateMachineModule {
    public class RangedAttackState : BaseState {
        private List<BaseAction> actions = new List<BaseAction>();
        private int currentIndex;
        private BaseAction currentAction;

        public RangedAttackState(StateBlackboard blackboard, SetAnimTriggerAction idleAction, TimerAction timerAction, SetRandomDestinationAction destinationAction, SetAnimTriggerAction runAction, MoveToDestinationAction moveToAction, SetSkillAction setSkillAction, LookAtTargetAction lookAtAction, SetAnimTriggerAction attackAction, int priority = 0, bool repeatable = false) : base(blackboard, priority, repeatable) {
            actions.Add(idleAction);
            actions.Add(timerAction);
            actions.Add(destinationAction);
            actions.Add(runAction);
            actions.Add(moveToAction);
            actions.Add(setSkillAction);
            actions.Add(lookAtAction);
            actions.Add(attackAction);
        }

        public override void Enter() {
            currentIndex = InRunRange() ? 2 : actions.Count - 3;
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
            if (currentIndex == 2 && !InRunRange())
                currentIndex = actions.Count - 3;

            currentAction = actions[currentIndex];
            currentAction.Start();
        }

        private bool InRunRange() {
            Transform transform = blackboard.Get<Transform>(BlackboardKey.Transform);
            Transform target = blackboard.Get<Transform>(BlackboardKey.Target);
            float runRange = blackboard.Get<float>(BlackboardKey.RunRange);

            float distance = (transform.position - target.position).magnitude;
            return distance <= runRange;
        }
    }
}
