using UnityEngine;
using System.Collections.Generic;

namespace StateMachineModule {
    public class StateMachine {
        private StateBlackboard blackboard;
        private List<StateNode> stateNodes = new List<StateNode>();
        private BaseState currentState;
        private BaseState initialState;
        private bool isStopped;

        public bool IsStopped { get { return isStopped; } }

        public StateMachine(StateBlackboard blackboard, List<StateNode> stateNodes, BaseState initialState) {
            this.blackboard = blackboard;
            this.stateNodes = stateNodes;
            this.initialState = initialState;
        }

        public void Start() {
            isStopped = false;
            currentState = initialState;
            currentState.Enter();
        }

        public void Update() {
            if (isStopped)
                return;

            currentState.Update();
        }

        public void LateUpdate() {
            if(isStopped)
                return;

            currentState.LateUpdate();
            
            BaseState nextState = GetNextState();
            if (nextState != null) {
                if (currentState != nextState || (currentState == nextState && currentState.Repeatable))
                    SetNextState(nextState);
            }
        }

        public void Stop() {
            isStopped = true;
        }

        public void Resume() {
            isStopped = false;
        }

        private BaseState GetNextState() {
            BaseState nextState = null;

            int statePriority = int.MinValue;
            foreach (StateNode node in stateNodes) {
                if (node.Check() && statePriority < node.State.Priority) {
                    statePriority = node.State.Priority;
                    nextState = node.State;
                }
            }

            return nextState;
        }

        private void SetNextState(BaseState nextState) {
            if (currentState != null)
                currentState.Exit();

            currentState = nextState;
            currentState.Enter();
        }

        public void OnAnimationEvent() {
            if (currentState != null)
                currentState.OnAnimationEvent();
        }
    }
}
