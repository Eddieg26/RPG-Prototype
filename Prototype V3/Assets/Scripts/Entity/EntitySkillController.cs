using UnityEngine;
using System.Collections.Generic;

public class EntitySkillController : MonoBehaviour {
    [SerializeField] private Entity self;
    [SerializeField] private Transform originSpawnPos;
    [SerializeField] private Transform frontSpawnPos;
    [SerializeField] private List<SkillRef> skills = new List<SkillRef>();
    [SerializeField] private bool useSkillList = false;

    private SkillRef currentSkill;
    private SkillRef[] equippedSkills = new SkillRef[2];

    public SkillRef CurrentSkill { get { return currentSkill; } }
    public int SkillCount { get { return equippedSkills.Length; } }

    private void Start() {
        if (useSkillList)
            equippedSkills = skills.ToArray();
    }

    public void SetCurrentSkill(int index) {
        currentSkill = index < 0 || index >= equippedSkills.Length ? null : equippedSkills[index];
    }

    public void UseCurrentSkill() {
        Transform spawnTransform = currentSkill.SkillInfo.MetaData.SpawnPosition == SkillSpawnPosition.Origin ? originSpawnPos : frontSpawnPos;
        SkillObject skillObject = Instantiate(currentSkill.SkillInfo.SkillPrefab, frontSpawnPos.position, frontSpawnPos.rotation);
        skillObject.Init(self, currentSkill.SkillInfo);

        self.OnUseMana(currentSkill.SkillInfo.ManaCost);
        currentSkill.Start();
    }

    public bool CanUseCurrentSkill() {
        return currentSkill != null ? self.Stats.Mana >= currentSkill.SkillInfo.ManaCost && currentSkill.Done() : false;
    }

    public List<SkillRef> GetSkills() {
        return new List<SkillRef>(skills);
    }

    public SkillRef[] GetEquippedSkills() {
        return equippedSkills;
    }
}
