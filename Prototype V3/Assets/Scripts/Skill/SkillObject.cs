using UnityEngine;

public class SkillObject : MonoBehaviour {
    protected Entity owner;
    protected EntitySkillInfo skillInfo;

    public Entity Owner { get { return owner; } }
    public EntitySkillInfo SkillInfo { get { return skillInfo; } }

    public void Init(Entity owner, EntitySkillInfo skillInfo) {
        this.owner = owner;
        this.skillInfo = skillInfo;
    }
}
