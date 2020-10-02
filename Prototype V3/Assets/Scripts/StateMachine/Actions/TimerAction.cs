using UnityEngine;

namespace StateMachineModule {
    public class TimerAction : BaseAction {
        private float duration;
        private float minDuration;
        private float maxDuration;
        private bool randomDuration;

        private float timer;

        public TimerAction(StateBlackboard blackboard, float minDuration, float maxDuration) : base(blackboard) {
            this.randomDuration = true;
            this.minDuration = minDuration;
            this.maxDuration = maxDuration;
        }

        public TimerAction(StateBlackboard blackboard, float duration) : base(blackboard) {
            this.randomDuration = false;
            this.duration = duration;
            this.minDuration = this.maxDuration = 0;
        }

        public override void Start() {
            if (randomDuration)
                duration = Random.Range(minDuration, maxDuration);

            timer = Time.time;
        }

        public override ActionStatus Execute() {
            return Time.time >= timer + duration ? ActionStatus.Finished : ActionStatus.Running;
        }

        public override void Finish() { }
    }
}
