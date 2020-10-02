using System.Collections.Generic;

namespace StateMachineModule {
    public class StateBlackboard {
        private Dictionary<BlackboardKey, object> dataMap = new Dictionary<BlackboardKey, object>();

        public T Get<T>(BlackboardKey key) {
            return (T)dataMap[key];
        }

        public void Add<T>(BlackboardKey key, T data) {
            dataMap.Add(key, data);
        }

        public void Set<T>(BlackboardKey key, T data) {
            dataMap[key] = data;
        }
    }
}
