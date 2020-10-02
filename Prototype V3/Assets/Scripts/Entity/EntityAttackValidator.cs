using UnityEngine;
using System.Collections.Generic;

public class EntityAttackValidator : MonoBehaviour {
    [SerializeField] private Entity self;
    [SerializeField] private EntityDetectionTrigger detectionTrigger;
    [SerializeField] private MeleeAttackData[] meleeAttacks;

    public MeleeAttackData[] MeleeAttacks { get { return meleeAttacks; } }

    private void Attack(int index) {
        MeleeAttackData meleeAttack = meleeAttacks[index];
        float attackDistance = meleeAttack.AttackRange * meleeAttack.AttackRange;
        List<Entity> targets = new List<Entity>(detectionTrigger.Targets);

        foreach (Entity entity in targets) {
            if (entity.IsAlive) {
                float entityDir = GetFacingDirection(entity.transform);
                float entityDist = GetDistance(entity.transform);
                if (entityDir > meleeAttack.DirectionThreshold && entityDist < attackDistance)
                    DoDamage(entity, meleeAttack.HitClip, meleeAttack.HitEffect);
            }
        }
    }

    private void DoDamage(Entity target, AudioClip hitClip, GameObject hitEffect) {
        int damage = self.Stats.GetAttack();
        target.OnTakeDamage(new DamageInfo(self, damage, hitEffect, hitClip));
    }

    private float GetFacingDirection(Transform entityTransform) {
        Vector3 dir = (entityTransform.position - transform.root.position).normalized;
        return Vector3.Dot(transform.root.forward, dir);
    }

    private float GetDistance(Transform entityTransform) {
        return (entityTransform.position - transform.root.position).sqrMagnitude;
    }
}
