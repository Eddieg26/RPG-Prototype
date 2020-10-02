using UnityEngine;
using UnityEngine.AI;

namespace StateMachineModule {
    public class MoveToTargetAction : BaseAction {
        private NavMeshAgent navAgent;
        private float stoppingDistance;
        private BlackboardKey key;
        private bool isDynamic;

        public MoveToTargetAction(StateBlackboard blackboard, NavMeshAgent navAgent, float stoppingDistance) : base(blackboard) {
            this.navAgent = navAgent;
            this.stoppingDistance = stoppingDistance;
            isDynamic = false;
        }

        public MoveToTargetAction(StateBlackboard blackboard, NavMeshAgent navAgent, BlackboardKey key) : base(blackboard) {
            this.navAgent = navAgent;
            this.key = key;
            isDynamic = true;
        }

        public override void Start() {
            navAgent.isStopped = false;
        }

        public override ActionStatus Execute() {
            if(isDynamic)
                stoppingDistance = blackboard.Get<float>(key);

            Transform target = blackboard.Get<Transform>(BlackboardKey.Target);
            if (target) {
                navAgent.SetDestination(target.position);
                navAgent.isStopped = false;
                if (navAgent.remainingDistance <= stoppingDistance)
                    return ActionStatus.Finished;
            } else {
                return ActionStatus.Finished;
            }

            return ActionStatus.Running;
        }

        public override void Finish() {
            navAgent.isStopped = true;
        }
    }
}
