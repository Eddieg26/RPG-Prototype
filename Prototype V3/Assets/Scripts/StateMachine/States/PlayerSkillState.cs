using UnityEngine;

namespace StateMachineModule {
    public class PlayerSkillState : BaseState {
        private SetAnimTriggerAction setSkillTrigger;

        public PlayerSkillState(StateBlackboard blackboard, SetAnimTriggerAction setSkillTrigger, int priority = 0, bool repeatable = false) : base(blackboard, priority, repeatable) {
            this.setSkillTrigger = setSkillTrigger;
        }

        public override void Enter() {
            setSkillTrigger.Start();
        }

        public override void Update() { }

        public override void Exit() { 
            blackboard.Set<bool>(BlackboardKey.SkillFlag, false);
        }

        public override void OnAnimationEvent() {
            blackboard.Set<bool>(BlackboardKey.SkillFlag, false);
        }
    }
}
