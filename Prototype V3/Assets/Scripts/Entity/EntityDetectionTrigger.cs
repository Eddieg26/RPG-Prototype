using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class EntityDetectionTrigger : MonoBehaviour {
    [SerializeField] private Entity self;
    private List<Entity> targetEntities = new List<Entity>();
    private List<Entity> entitiesToRemove = new List<Entity>();

    public List<Entity> Targets { get { return targetEntities; } }
    public UnityAction<Entity> AddCallback { get; set; }
    public UnityAction<Entity> RemoveCallback { get; set; }

    private void LateUpdate() {
        for (int index = 0; index < entitiesToRemove.Count; ++index)
            RemoveEntity(entitiesToRemove[index]);

        entitiesToRemove.Clear();
    }

    private void OnTriggerEnter(Collider other) {
        Entity entity = other.GetComponent<Entity>();
        if (entity != null && entity != self && entity.ID.Tag != self.ID.Tag && entity.IsAlive)
            AddEntity(entity);
    }

    private void OnTriggerExit(Collider other) {
        Entity entity = other.GetComponent<Entity>();
        if (entity != null && entity != self && entity.ID.Tag != self.ID.Tag && targetEntities.Contains(entity))
            AddToRemoveQueue(entity);
    }

    private void TakeDamage(DamageInfo damageInfo) {
        if(damageInfo.Owner != null)
            AddEntity(damageInfo.Owner);
    }

    public void AddEntity(Entity entity) {
        if (!targetEntities.Contains(entity)) {
            targetEntities.Add(entity);
            entity.Die += AddToRemoveQueue;
            if (AddCallback != null)
                AddCallback(entity);
        }
    }

    public void RemoveEntity(Entity entity) {
        targetEntities.Remove(entity);
        if (RemoveCallback != null)
            RemoveCallback(entity);
    }

    private void AddToRemoveQueue(Entity entity) {
        entitiesToRemove.Add(entity);
    }
}
