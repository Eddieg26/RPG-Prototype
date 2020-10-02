using UnityEngine;

namespace StateMachineModule {
    public delegate bool Predicate();
    
    public class Condition {
        private Predicate condition;

        public Condition(Predicate condition) {
            this.condition = condition;
        }

        public bool Check() {
            return condition();
        }
    }
}
