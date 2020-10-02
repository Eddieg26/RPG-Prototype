using UnityEngine;

public class SpawnerTrigger : MonoBehaviour {
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private GameObject[] barriers;
    [SerializeField] private bool spawnEnemies = true;

    private bool enemiesSpawned;
    private int defeatedEnemyCount;

    private void Start() {
        enemiesSpawned = false;
        spawner.SpawnEnemyCallback += EnemySpawned;
        ToggleBarriers(false);
    }

    private void EnemySpawned(Enemy enemy) {
        Entity enemyEntitiy = enemy.GetComponent<Entity>();
        enemyEntitiy.Die += OnDefeatEnemy;
    }

    private void OnDefeatEnemy(Entity entity) {
        defeatedEnemyCount++;

        if (defeatedEnemyCount == spawner.SpanwedEnemyCount)
            ToggleBarriers(false);
    }

    private void OnTriggerEnter(Collider other) {
        Player player = other.GetComponent<Player>();
        if (player != null && !enemiesSpawned) {
            ToggleBarriers(true);
            if (spawnEnemies)
                spawner.SpawnEnemies();
        }
    }

    private void ToggleBarriers(bool activate) {
        foreach (var barrier in barriers)
            barrier.SetActive(activate);
    }
}
