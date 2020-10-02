using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float moveSpeed;
    [SerializeField, Range(0, 200)] private int attackPower = 100;
    [SerializeField] private GameObject hitEffect;

    private void Update() {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.isTrigger)
            return;

        SkillObject skillObject = GetComponent<SkillObject>();
        Entity targetEntity = other.GetComponent<Entity>();
        if (targetEntity != null && targetEntity != skillObject.Owner && targetEntity.ID.Tag != skillObject.Owner.ID.Tag) {
            int damage = Mathf.RoundToInt(skillObject.Owner.Stats.GetAttack() * (attackPower / StatConstants.MOD));
            DamageInfo damageInfo = new DamageInfo(skillObject.Owner, damage);
            targetEntity.OnTakeDamage(damageInfo);
        }

        if (hitEffect != null)
            Instantiate(hitEffect, transform.position, transform.rotation);

        Destroy(this.gameObject);
    }

}
