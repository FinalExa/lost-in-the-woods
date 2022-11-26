using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnerEnemies
    {
        public EnemyController enemy;
        public bool startsSpawned;
        public GameObject firstSpawnPosition;
        public float deathCooldown;
    }
    [SerializeField] private string zoneName;
    [SerializeField] private SpawnerEnemies[] enemiesToSpawnInThisZone;
    [SerializeField] private GameObject[] spawnPoints;
    private List<EnemyController> activeEnemies;
    private List<EnemyController> deadEnemies;

    private void Start()
    {
        activeEnemies = new List<EnemyController>();
        deadEnemies = new List<EnemyController>();
    }
}
