using UnityEngine;
using UnityEngine.AI;
using StateMachineModule;
using System.Collections.Generic;

public class ChameleonController : MonoBehaviour {
    [SerializeField] private Entity self;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private EntityDetectionTrigger detectionTrigger;
    [SerializeField] private EntitySkillController skillController;

    private StateBlackboard blackboard;
    private StateMachine stateMachine;

    private Entity target;

    private void Start() {
        InitStateMachine();
        stateMachine.Start();

        self.TakeDamage += TakeDamage;

        detectionTrigger.AddCallback += AddTarget;
        detectionTrigger.RemoveCallback += RemoveTarget;
    }

    private void Update() {
        stateMachine.Update();
    }

    private void LateUpdate() {
        stateMachine.LateUpdate();
    }

    private void TakeDamage(DamageInfo damageInfo) {
        blackboard.Set<bool>(BlackboardKey.DamageFlag, true);
        blackboard.Set<bool>(BlackboardKey.RepeatDamageFlag, true);
    }

    private void SetTarget(Entity entity) {
        target = entity;
        blackboard.Set<Transform>(BlackboardKey.Target, target != null ? target.transform : null);
    }

    private void AddTarget(Entity entity) {
        if (target == null)
            SetTarget(entity);
    }

    private void RemoveTarget(Entity entity) {
        if (target == entity) {
            SetTarget(GetNewTarget());
        }
    }

    private Entity GetNewTarget() {
        Entity newTarget = null;
        float currentDistance = float.MaxValue;

        detectionTrigger.Targets.ForEach((entity) => {
            float distance = Vector3.Distance(entity.transform.position, transform.position);
            if (distance < currentDistance) {
                currentDistance = distance;
                newTarget = entity;
            }
        });

        return newTarget;
    }

    private void OnAnimationEvent() {
        stateMachine.OnAnimationEvent();
    }

    private void InitStateMachine() {
        blackboard = new StateBlackboard();
        blackboard.Add<bool>(BlackboardKey.DamageFlag, false);
        blackboard.Add<bool>(BlackboardKey.RepeatDamageFlag, false);
        blackboard.Add<Transform>(BlackboardKey.Transform, transform);
        blackboard.Add<float>(BlackboardKey.RunRange, 3f);

        EnemyIdleState idleState = EnemyIdleState.Create(blackboard, animator, "Idle", 0);
        StateNode idleNode = new StateNode(idleState, new Condition(() => { return true; }));

        SetAnimTriggerAction idleAction = new SetAnimTriggerAction(blackboard, animator, "Idle");
        SetAnimTriggerAction runAction = new SetAnimTriggerAction(blackboard, animator, "Run");
        TimerAction timerAction = new TimerAction(blackboard, 0.5f, 1.5f);
        SetRandomDestinationAction destinationAction = new SetRandomDestinationAction(blackboard, transform.position, 6f);
        MoveToDestinationAction moveToAction = new MoveToDestinationAction(blackboard, navAgent);
        SetSkillAction setSkillAction = new SetSkillAction(blackboard, skillController);
        LookAtTargetAction lookAtAction = new LookAtTargetAction(blackboard, transform);
        SetAnimTriggerAction attackAction = new SetAnimTriggerAction(blackboard, animator, BlackboardKey.AnimTrigger, true);
        RangedAttackState attackState = new RangedAttackState(blackboard, idleAction, timerAction, destinationAction, runAction, moveToAction, setSkillAction, lookAtAction, attackAction, 300);
        StateNode attackNode = new StateNode(attackState, new Condition(() => target != null));

        DamageState damageState = DamageState.Create(blackboard, animator, "Damage", 500);
        StateNode damageNode = new StateNode(damageState, new Condition(() => { return blackboard.Get<bool>(BlackboardKey.DamageFlag); }));

        DeathState deathState = DeathState.Create(blackboard, animator, "Death", int.MaxValue);
        StateNode deathNode = new StateNode(deathState, new Condition(() => { return !self.IsAlive; }));

        List<StateNode> nodes = new List<StateNode>();
        nodes.Add(idleNode);
        nodes.Add(attackNode);
        nodes.Add(damageNode);
        nodes.Add(deathNode);

        stateMachine = new StateMachine(blackboard, nodes, idleState);
    }
}
