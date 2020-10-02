using UnityEngine;

namespace StateMachineModule {
    public class SetSkillAction : BaseAction {
        private EntitySkillController skillController;

        public SetSkillAction(StateBlackboard blackboard, EntitySkillController skillController) : base(blackboard) {
            this.skillController = skillController;
        }

        public override void Start() { }

        public override ActionStatus Execute() {
            int skillIndex = Random.Range(0, skillController.SkillCount);
            skillController.SetCurrentSkill(skillIndex);
            blackboard.Set<string>(BlackboardKey.AnimTrigger, skillController.CurrentSkill.SkillInfo.MetaData.AnimTrigger);
            return ActionStatus.Finished;
        }

        public override void Finish() { }
    }
}
