
namespace StateMachineModule {
    public class StateNode {
        private BaseState state;
        private Condition condition;

        public BaseState State { get { return state; } }

        public StateNode(BaseState state, Condition condition) {
            this.state = state;
            this.condition = condition;
        }

        public bool Check() {
            return condition != null ? condition.Check() : false;
        }
    }
}
