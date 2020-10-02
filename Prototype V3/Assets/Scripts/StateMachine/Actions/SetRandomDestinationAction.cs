using UnityEngine;
using UnityEngine.AI;

namespace StateMachineModule {
    public class SetRandomDestinationAction : BaseAction {
        private Vector3 origin;
        private float radius;

        public SetRandomDestinationAction(StateBlackboard blackboard, Vector3 origin, float radius) : base(blackboard) {
            this.origin = origin;
            this.radius = radius;
        }

        public override void Start() { }

        public override ActionStatus Execute() {
            Vector3 randomPoint = origin + Random.insideUnitSphere * radius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
                blackboard.Set<Vector3>(BlackboardKey.NavMeshDestination, hit.position);
                return ActionStatus.Finished;
            }

            return ActionStatus.Running;
        }

        public override void Finish() { }
    }
}
