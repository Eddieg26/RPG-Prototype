using UnityEngine;
using UnityEngine.AI;
using StateMachineModule;
using System.Collections.Generic;

public class DracoController : MonoBehaviour {
    [SerializeField] private Entity self;
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private EntityDetectionTrigger detectionTrigger;
    [SerializeField] private EntityAttackValidator attackValidator;
    [SerializeField] private Vector3 origin;
    [SerializeField] private float patrolRadius;

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
        blackboard.Add<float>(BlackboardKey.AttackRange, 0f);

        EnemyIdleState idleState = EnemyIdleState.Create(blackboard, animator, "Idle", 0);
        StateNode idleNode = new StateNode(idleState, new Condition(() => { return true; }));

        SetAnimTriggerAction walkAction =  new SetAnimTriggerAction(blackboard, animator, "Run");
        MeleeAttackState meleeAttackState = MeleeAttackState.Create(blackboard, animator, navAgent, attackValidator.MeleeAttacks, transform, BlackboardKey.AttackRange, BlackboardKey.AnimTrigger, walkAction, 300);
        StateNode attackNode = new StateNode(meleeAttackState, new Condition(() => { return target != null; }));

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
