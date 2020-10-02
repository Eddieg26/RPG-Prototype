using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Game/Skill")]
public class EntitySkillInfo : ScriptableObject {
    [SerializeField] private string skillName;
    [SerializeField, TextArea] private string skillInfo;
    [SerializeField] private int manaCost;
    [SerializeField] private float cooldown;
    [SerializeField] private Sprite icon;
    [SerializeField] private SkillObject skillPrefab;
    [SerializeField] private SkillMetaData metaData;

    public string SkillName { get { return skillName; } }
    public string SkillInfo { get { return skillInfo; } }
    public int ManaCost { get { return manaCost; } }
    public float Cooldown { get { return cooldown; } }
    public Sprite Icon { get { return icon; } }
    public SkillObject SkillPrefab { get { return skillPrefab; } }
    public SkillMetaData MetaData { get { return metaData; } }
}

[System.Serializable]
public class SkillMetaData {
    [SerializeField] private string animTrigger;
    [SerializeField] private SkillSpawnPosition spawnPosition;

    public string AnimTrigger { get { return animTrigger; } }
    public SkillSpawnPosition SpawnPosition { get { return spawnPosition; } }
}
