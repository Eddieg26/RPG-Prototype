using UnityEngine;
using UnityEngine.AI;

namespace StateMachineModule {
    public class MoveToDestinationAction : BaseAction {
        private NavMeshAgent navAgent;

        public MoveToDestinationAction(StateBlackboard blackboard, NavMeshAgent navAgent) : base(blackboard) {
            this.navAgent = navAgent;
        }

        public override void Start() {
            Vector3 destination = blackboard.Get<Vector3>(BlackboardKey.NavMeshDestination);
            navAgent.SetDestination(destination);
            navAgent.isStopped = false;
        }

        public override ActionStatus Execute() {
            if (navAgent.pathPending)
                return ActionStatus.Running;

            if (navAgent.remainingDistance <= 0.1f && (!navAgent.hasPath || navAgent.velocity.sqrMagnitude <= 0.001f))
                return ActionStatus.Finished;

            return ActionStatus.Running;
        }

        public override void Finish() {
            navAgent.isStopped = true;
        }
    }
}
