using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private GameObject spawnVisualEffect;
    [SerializeField] private List<Enemy> enemyPrefabs = new List<Enemy>();
    [SerializeField] private List<Transform> spawnLocations = new List<Transform>();

    private List<Enemy> spawnedEnemies = new List<Enemy>();
    private List<Transform> usedSpawnLocations = new List<Transform>();

    public int SpanwedEnemyCount { get { return spawnedEnemies.Count; } }
    public UnityAction<Enemy> SpawnEnemyCallback { get; set; }

    public void SpawnEnemies() {
        while (enemyPrefabs.Count > 0) {
            SpawnEnemy(GetEnemy(), GetSpawnLocation());
        }
    }

    private void SpawnEnemy(Enemy enemyPrefab, Transform spawnLocation) {
        GameObject enemyObject = Instantiate<GameObject>(enemyPrefab.gameObject, spawnLocation.position, spawnLocation.rotation);

        if (spawnVisualEffect != null)
            Instantiate<GameObject>(spawnVisualEffect, spawnLocation.position, spawnLocation.rotation);

        Entity enemyEntity = enemyObject.GetComponent<Entity>();
        enemyEntity.OnEntityInit();

        Enemy enemy = enemyObject.GetComponent<Enemy>();
        if (SpawnEnemyCallback != null)
            SpawnEnemyCallback(enemy);
    }

    private Enemy GetEnemy() {
        if (enemyPrefabs.Count == 0) {
            enemyPrefabs.AddRange(spawnedEnemies);
            spawnedEnemies.Clear();
        }

        int index = Random.Range(0, enemyPrefabs.Count);
        Enemy enemy = enemyPrefabs[index];
        spawnedEnemies.Add(enemy);
        enemyPrefabs.RemoveAt(index);

        return enemy;
    }

    private Transform GetSpawnLocation() {
        if (spawnLocations.Count == 0) {
            spawnLocations.AddRange(usedSpawnLocations);
            usedSpawnLocations.Clear();
        }

        int index = Random.Range(0, spawnLocations.Count);
        Transform spawnLocation = spawnLocations[index];
        usedSpawnLocations.Add(spawnLocation);
        spawnLocations.RemoveAt(index);

        return spawnLocation;
    }
}