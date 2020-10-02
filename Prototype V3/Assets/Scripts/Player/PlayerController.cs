using UnityEngine;
using UnityEngine.Events;
using StateMachineModule;
using System.Collections.Generic;

[RequireComponent(typeof(InputController))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private Interactor interactor;
    [SerializeField] private EntitySkillController skillController;
    [SerializeField] private EntityDetectionTrigger detectionTrigger;
    [SerializeField] private GameAction setPlayerInputControllerAction;
    [SerializeField] private GameEvent changeBGMusicEvent;
    [SerializeField] private float runSpeed;

    private Entity self;
    private Animator animator;
    private CameraController cameraController;
    private InputController inputController;
    private StateMachine stateMachine;
    private StateBlackboard blackboard;

    private Entity target;
    private bool isTargeting;

    private GameEventListener setPlayerInputControllerListener;
    private UnityAction<float, float, Vector3, Vector3> updateCameraAction;
    private UnityAction<CameraStateType> setCameraStateAction;

    private void Awake() {
        Init();
        InitInputController();
        InitStateMachine();
    }

    private void Start() {
        inputController.SetAsFocusedController();
        stateMachine.Start();
    }

    private void Update() {
        UpdateBlackboard();
        UpdateCamera();
        stateMachine.Update();
    }

    private void LateUpdate() {
        stateMachine.LateUpdate();
    }

    private void TakeDamage(DamageInfo damageInfo) {
        blackboard.Set<bool>(BlackboardKey.DamageFlag, true);
        blackboard.Set<bool>(BlackboardKey.RepeatDamageFlag, true);
    }

    private void UpdateBlackboard() {
        float horizontal = inputController.GetRuntimeInputConfig().GetBinding(InputBindingName.GAMEPLAY_HORIZONTAL).Value;
        float vertical = inputController.GetRuntimeInputConfig().GetBinding(InputBindingName.GAMEPLAY_VERTICAL).Value;

        blackboard.Set<float>(BlackboardKey.Horizontal, horizontal);
        blackboard.Set<float>(BlackboardKey.Vertical, vertical);
    }

    private void UpdateCamera() {
        RuntimeInputConfiguration inputConfig = inputController.GetRuntimeInputConfig();
        float camHorizontal = inputConfig.GetBinding(InputBindingName.GAMEPLAY_CAMHORIZONTAL).Value;
        float camVertical = inputConfig.GetBinding(InputBindingName.GAMEPLAY_CAMVERTICAL).Value;

        if (updateCameraAction != null)
            updateCameraAction(camHorizontal, camVertical, transform.position, target ? target.transform.position : Vector3.zero);
    }

    private void ToggleTargeting() {
        bool toggle = !isTargeting && detectionTrigger.Targets.Count > 0;
        if (toggle == isTargeting)
            return;

        if (toggle)
            StartTargeting();
        else
            EndTargeting();
    }

    private void StartTargeting() {
        isTargeting = true;
        SetTarget(GetClosestTarget());
        setCameraStateAction(CameraStateType.LookFrame);
    }

    private void EndTargeting() {
        isTargeting = false;
        ClearTarget();
        setCameraStateAction(CameraStateType.Orbit);
    }

    private void GetNextTarget() {
        int targetIndex = detectionTrigger.Targets.FindIndex((e) => { return e == target; });
        if (targetIndex >= 0 && detectionTrigger.Targets.Count > 0) {
            targetIndex = (targetIndex + 1 + detectionTrigger.Targets.Count) % detectionTrigger.Targets.Count;
            SetTarget(detectionTrigger.Targets[targetIndex]);
        }
    }

    private Entity GetClosestTarget() {
        Entity closestTarget = null;
        float distance = float.MaxValue;
        List<Entity> targets = new List<Entity>(detectionTrigger.Targets);

        foreach (Entity entity in targets) {
            if (entity.IsAlive) {
                float currentDistance = (entity.transform.position - transform.position).magnitude;
                if (currentDistance < distance) {
                    distance = currentDistance;
                    closestTarget = entity;
                }
            }
        }

        return closestTarget;
    }

    private void SetTarget(Entity newTarget) {
        if (target != newTarget && newTarget != null) {
            Entity prevTarget = target;
            target = newTarget;

            if (prevTarget)
                prevTarget.OnSetFocus(false);

            target.OnSetFocus(true);
            blackboard.Set<Transform>(BlackboardKey.Target, target != null ? target.transform : null);
        }
    }

    private void ClearTarget() {
        target = null;
        blackboard.Set<Transform>(BlackboardKey.Target, null);
    }

    private void AddTarget(Entity entity) {
        if (detectionTrigger.Targets.Count == 1)
            changeBGMusicEvent.Invoke(MusicType.Battle);
    }

    private void RemoveTarget(Entity entity) {
        if (target == entity && isTargeting) {
            if (detectionTrigger.Targets.Count > 0)
                SetTarget(GetClosestTarget());
            else {
                ClearTarget();
                EndTargeting();
            }
        }

        if (detectionTrigger.Targets.Count == 0)
            changeBGMusicEvent.Invoke(MusicType.Adventure);
    }

    private void OnAnimationEvent() {
        stateMachine.OnAnimationEvent();
    }

    private void Die(Entity entity) {
        if (isTargeting)
            EndTargeting();

        setCameraStateAction(CameraStateType.None);
    }

    private void InitEntity() {
        setCameraStateAction(CameraStateType.Orbit);
    }

    private void Init() {
        inputController = GetComponent<InputController>();
        animator = GetComponent<Animator>();
        self = GetComponent<Entity>();

        self.EntityInit += InitEntity;
        self.TakeDamage += TakeDamage;
        self.Die += Die;

        Debug.Assert(Camera.main, "There is no main Camera in the scene. Make sure you have a Camera tagged as 'MainCamera' in the scene.");
        CameraController cameraController = Camera.main.GetComponent<CameraController>();

        Debug.Assert(cameraController, "Main camera does not have a CameraController attached to it.");
        updateCameraAction = cameraController.UpdateCamera;
        setCameraStateAction = cameraController.SetCameraStateType;

        setCameraStateAction(CameraStateType.Orbit);

        isTargeting = false;

        detectionTrigger.AddCallback += AddTarget;
        detectionTrigger.RemoveCallback += RemoveTarget;

        setPlayerInputControllerListener = new GameEventListener(() => inputController.SetAsFocusedController());
        setPlayerInputControllerAction.AddListener(setPlayerInputControllerListener);
    }

    private void InitInputController() {
        RuntimeInputConfiguration inputConfig = inputController.GetRuntimeInputConfig();
        InputBinding horizontalBinding = inputConfig.GetBinding(InputBindingName.GAMEPLAY_HORIZONTAL);
        InputBinding verticalBinding = inputConfig.GetBinding(InputBindingName.GAMEPLAY_VERTICAL);
        InputBinding interactBinding = inputConfig.GetBinding(InputBindingName.GAMEPLAY_INTERACT);
        InputBinding attackBinding = inputConfig.GetBinding(InputBindingName.GAMEPLAY_ATTACK);
        InputBinding primarySkillBinding = inputConfig.GetBinding(InputBindingName.GAMEPLAY_PRIMARY_SKILL);
        InputBinding secondarySkillBinding = inputConfig.GetBinding(InputBindingName.GAMEPLAY_SECONDARY_SKILL);
        InputBinding toggleTargetingBinding = inputConfig.GetBinding(InputBindingName.GAMEPLAY_TOGGLE_TARGETING);
        InputBinding nextTargetBinding = inputConfig.GetBinding(InputBindingName.GAMEPLAY_NEXT_TARGET);

        horizontalBinding.Action.Start = () => { blackboard.Set<float>(BlackboardKey.Horizontal, horizontalBinding.Value); };
        verticalBinding.Action.Start = () => { blackboard.Set<float>(BlackboardKey.Vertical, verticalBinding.Value); };

        interactBinding.Action.Start = () => interactor.Interact();

        attackBinding.Action.Start = () => {
            bool attackFlag = blackboard.Get<bool>(BlackboardKey.AttackingFlag);
            if (attackFlag)
                blackboard.Set<bool>(BlackboardKey.NextAttackFlag, true);
            else
                blackboard.Set<bool>(BlackboardKey.AttackingFlag, true);
        };

        primarySkillBinding.Action.Start = () => {
            bool skillFlag = blackboard.Get<bool>(BlackboardKey.SkillFlag);
            skillController.SetCurrentSkill(0);
            if (!skillFlag && skillController.CanUseCurrentSkill()) {
                EntitySkillInfo skill = skillController.CurrentSkill.SkillInfo;
                blackboard.Set<string>(BlackboardKey.AnimTrigger, skill.MetaData.AnimTrigger);
                blackboard.Set<bool>(BlackboardKey.SkillFlag, true);
            }
        };

        secondarySkillBinding.Action.Start = () => {
            bool skillFlag = blackboard.Get<bool>(BlackboardKey.SkillFlag);
            skillController.SetCurrentSkill(1);
            if (!skillFlag && skillController.CanUseCurrentSkill()) {
                EntitySkillInfo skill = skillController.CurrentSkill.SkillInfo;
                blackboard.Set<string>(BlackboardKey.AnimTrigger, skill.MetaData.AnimTrigger);
                blackboard.Set<bool>(BlackboardKey.SkillFlag, true);
            }
        };

        toggleTargetingBinding.Action.Start = () => { ToggleTargeting(); };
        nextTargetBinding.Action.Start = () => { GetNextTarget(); };
    }

    private void InitStateMachine() {
        blackboard = new StateBlackboard();
        blackboard.Add(BlackboardKey.AttackingFlag, false);
        blackboard.Add(BlackboardKey.NextAttackFlag, false);
        blackboard.Add(BlackboardKey.SkillFlag, false);
        blackboard.Add(BlackboardKey.DamageFlag, false);
        blackboard.Add(BlackboardKey.RepeatDamageFlag, false);
        blackboard.Add(BlackboardKey.AnimTrigger, string.Empty);
        blackboard.Add<Transform>(BlackboardKey.Target, null);
        blackboard.Add<Vector3>(BlackboardKey.MoveDirection, Vector3.zero);

        SetAnimTriggerAction setIdleTriggerAction = new SetAnimTriggerAction(blackboard, animator, "Idle");
        LookAtTargetAction lookAtAction = new LookAtTargetAction(blackboard, transform);
        PlayerIdleState idleState = new PlayerIdleState(blackboard, setIdleTriggerAction, lookAtAction);
        StateNode idleNode = new StateNode(idleState, new Condition(() => { return true; }));

        SetAnimTriggerAction setRunTriggerAction = new SetAnimTriggerAction(blackboard, animator, "Move");
        SetMoveDirectionAction setMoveDirAction = new SetMoveDirectionAction(blackboard, transform, Camera.main.transform);
        MoveControllerAction moveControllerAction = new MoveControllerAction(blackboard, GetComponent<CharacterController>(), runSpeed);

        PlayerMoveState moveState = new PlayerMoveState(blackboard, setRunTriggerAction, setMoveDirAction, moveControllerAction, 100);
        StateNode moveNode = new StateNode(moveState, new Condition(() => {
            float horizontal = inputController.GetRuntimeInputConfig().GetBinding(InputBindingName.GAMEPLAY_HORIZONTAL).Value;
            float vertical = inputController.GetRuntimeInputConfig().GetBinding(InputBindingName.GAMEPLAY_VERTICAL).Value;
            return Mathf.Abs(horizontal) > InputConstants.INPUT_DEAD_ZONE || Mathf.Abs(vertical) > InputConstants.INPUT_DEAD_ZONE;
        }));

        SetAnimTriggerAction setAttackTrigger = new SetAnimTriggerAction(blackboard, animator, "Attack", true);
        SetAnimTriggerAction setNextAttackTrigger = new SetAnimTriggerAction(blackboard, animator, "NextAttack", true);
        PlayerAttackState attackState = new PlayerAttackState(blackboard, setAttackTrigger, setNextAttackTrigger, lookAtAction, 500);
        StateNode attackNode = new StateNode(attackState, new Condition(() => { return blackboard.Get<bool>(BlackboardKey.AttackingFlag) == true; }));

        SetAnimTriggerAction setSkillTrigger = new SetAnimTriggerAction(blackboard, animator, BlackboardKey.AnimTrigger, true);
        PlayerSkillState skillState = new PlayerSkillState(blackboard, setSkillTrigger, 400);
        StateNode skillNode = new StateNode(skillState, new Condition(() => { return blackboard.Get<bool>(BlackboardKey.SkillFlag) == true; }));

        SetAnimTriggerAction damageAction = new SetAnimTriggerAction(blackboard, animator, "Damage", true);
        DamageState damageState = new DamageState(blackboard, damageAction, 600);
        Condition damageCondition = new Condition(() => { return blackboard.Get<bool>(BlackboardKey.DamageFlag); });
        StateNode damageNode = new StateNode(damageState, damageCondition);

        DeathState deathState = DeathState.Create(blackboard, animator, "Die", int.MaxValue);
        StateNode deathNode = new StateNode(deathState, new Condition(() => { return !self.IsAlive; }));

        List<StateNode> nodes = new List<StateNode>();
        nodes.Add(idleNode);
        nodes.Add(moveNode);
        nodes.Add(attackNode);
        nodes.Add(skillNode);
        nodes.Add(damageNode);
        nodes.Add(deathNode);

        stateMachine = new StateMachine(blackboard, nodes, idleState);
    }

    private void OnDestroy() {
        setPlayerInputControllerAction.RemoveListener(setPlayerInputControllerListener);
    }
}
