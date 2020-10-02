using UnityEngine;

[System.Serializable]
public class SkillRef {
    [SerializeField] private EntitySkillInfo skillInfo;
    private float cooldownTime;

    public EntitySkillInfo SkillInfo { get { return skillInfo; } }
    public float CooldownTime { get { return cooldownTime; } }

    public SkillRef() { }

    public SkillRef(EntitySkillInfo skillInfo) {
        this.skillInfo = skillInfo;
    }

    public void Start() {
        cooldownTime = Time.time;
    }

    public bool Done() {
        return skillInfo != null ? Time.time > cooldownTime + skillInfo.Cooldown : true;
    }
}
