using UnityEngine;

namespace StateMachineModule {
    public abstract class BaseAction {
        protected StateBlackboard blackboard;

        public BaseAction(StateBlackboard blackboard) {
            this.blackboard = blackboard;
        }

        public abstract void Start();
        public abstract ActionStatus Execute();
        public abstract void Finish();

        public virtual void OnAnimationEvent() { }
    }
}
