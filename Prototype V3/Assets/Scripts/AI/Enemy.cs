using UnityEngine;

public class Enemy : MonoBehaviour {
    [SerializeField] private int exp;

    private Entity self;
    private Player player;

    private void Start() {
        self = GetComponent<Entity>();
        self.TakeDamage += OnTakeDamage;
        self.Die += OnDie;
    }

    private void OnTakeDamage(DamageInfo damageInfo) {
        if (this.player == null) {
            Player player = damageInfo.Owner.GetComponent<Player>();
            if (player)
                this.player = player;
        }
    }

    private void OnDie(Entity entity) {
        if (player != null) {
            player.GainExp(exp);
            player = null;
        }
    }
}
