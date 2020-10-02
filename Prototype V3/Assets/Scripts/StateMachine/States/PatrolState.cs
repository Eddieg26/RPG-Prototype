using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

namespace StateMachineModule {
    public class PatrolState : BaseState {
        private List<BaseAction> actions = new List<BaseAction>();
        private int currentIndex;
        private BaseAction currentAction;

        public PatrolState(StateBlackboard blackboard, SetAnimTriggerAction setIdleAction, TimerAction timerAction, SetAnimTriggerAction setWalkAction, SetRandomDestinationAction setDestinationAction, MoveToDestinationAction moveToAction, int priority = 0, bool repeatable = false) : base(blackboard, priority, repeatable) {
            actions.Add(setIdleAction);
            actions.Add(timerAction);
            actions.Add(setWalkAction);
            actions.Add(setDestinationAction);
            actions.Add(moveToAction);
        }

        public override void Enter() {
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

        private void NextAction() {
            currentAction.Finish();
            currentIndex = ((currentIndex + 1 + actions.Count) % actions.Count);

            currentAction = actions[currentIndex];
            currentAction.Start();
        }

        public static PatrolState Create(StateBlackboard blackboard, Animator animator, NavMeshAgent navAgent, string idleTrigger, string runTrigger, Vector3 origin, float patrolRadius, float minWaitDuration, float maxWaitDuration, out SetAnimTriggerAction walkAction, bool randomWaitDuration = false) {

            SetAnimTriggerAction idleAction = new SetAnimTriggerAction(blackboard, animator, idleTrigger);
            walkAction = new SetAnimTriggerAction(blackboard, animator, runTrigger);
            SetRandomDestinationAction destinationAction = new SetRandomDestinationAction(blackboard, origin, patrolRadius);
            MoveToDestinationAction moveToAction = new MoveToDestinationAction(blackboard, navAgent);
            TimerAction timerAction = randomWaitDuration ? new TimerAction(blackboard, minWaitDuration, maxWaitDuration) : new TimerAction(blackboard, maxWaitDuration);

            return new PatrolState(blackboard, idleAction, timerAction, walkAction, destinationAction, moveToAction);
        }
    }
}
