using UnityEngine;

public class EntityKillData {
    private EntityID targetEntity;

    public EntityID TargetEntity { get { return targetEntity; } }

    public EntityKillData(EntityID targetEntity) {
        this.targetEntity = targetEntity;
    }
}
