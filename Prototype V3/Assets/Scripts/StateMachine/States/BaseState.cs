using UnityEngine;

namespace StateMachineModule {
    public abstract class BaseState {
        protected StateBlackboard blackboard;
        protected int priority;
        protected bool repeatable;

        public int Priority { get { return priority; } }
        public bool Repeatable { get { return repeatable; } }

        public BaseState(StateBlackboard blackboard, int priority = 0, bool repeatable = false) {
            this.blackboard = blackboard;
            this.priority = priority;
        }

        public abstract void Enter();
        public abstract void Update();
        public virtual void LateUpdate() {

        }
        public abstract void Exit();

        public virtual void OnAnimationEvent() { }
    }
}
