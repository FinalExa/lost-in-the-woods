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
    [System.Serializable]
    public struct EnemiesToRespawn
    {
        public EnemyController enemy;
        public float maxTimer;
        public float deathTimer;
    }
    [SerializeField] private string zoneName;
    [SerializeField] private SpawnerEnemies[] enemiesToSpawnInThisZone;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private List<EnemiesToRespawn> activeEnemies;
    [SerializeField] private List<EnemiesToRespawn> deadEnemies;
    private bool spawnerIsSet;

    private void Start()
    {
        SpawnerStartup();
    }

    private void Update()
    {
        if (spawnerIsSet) DeadEnemyRespawn();
    }

    private void SpawnerStartup()
    {
        if (spawnPoints.Length > 0 && enemiesToSpawnInThisZone.Length > 0) SetupLists();
        else Debug.LogError("Error: No spawn points or enemies have been set for this zone");
    }

    private void SetupLists()
    {
        spawnerIsSet = true;
        activeEnemies = new List<EnemiesToRespawn>();
        deadEnemies = new List<EnemiesToRespawn>();
        foreach (SpawnerEnemies enemyToSpawn in enemiesToSpawnInThisZone)
        {
            EnemyController enemyRef = Instantiate(enemyToSpawn.enemy, this.transform);
            EnemiesToRespawn enemyToRespawn = CreateEnemyToRespawn(enemyRef, enemyToSpawn.deathCooldown);
            enemyRef.spawnerRef = this;
            enemyRef.spawnerEnemyInfo = enemyToRespawn;
            if (enemyToSpawn.startsSpawned)
            {
                if (enemyToSpawn.firstSpawnPosition != null) enemyToSpawn.enemy.transform.position = enemyToSpawn.firstSpawnPosition.transform.position;
                else RandomizeEnemyPosition(enemyToSpawn.enemy);
                SetEnemyActive(enemyToRespawn);
            }
            else SetEnemyDead(enemyToRespawn);
        }
    }
    private EnemiesToRespawn CreateEnemyToRespawn(EnemyController enemyRef, float timer)
    {
        EnemiesToRespawn enemyToRespawn = new EnemiesToRespawn();
        enemyToRespawn.enemy = enemyRef;
        enemyToRespawn.maxTimer = timer;
        enemyToRespawn.deathTimer = timer;
        return enemyToRespawn;
    }

    private void SetEnemyActive(EnemiesToRespawn enemyRef)
    {
        if (deadEnemies.Contains(enemyRef)) deadEnemies.Remove(enemyRef);
        if (!activeEnemies.Contains(enemyRef)) activeEnemies.Add(enemyRef);
        enemyRef.enemy.gameObject.SetActive(true);
    }


    public void SetEnemyDead(EnemiesToRespawn enemyRef)
    {
        if (!deadEnemies.Contains(enemyRef)) deadEnemies.Add(enemyRef);
        if (activeEnemies.Contains(enemyRef)) activeEnemies.Remove(enemyRef);
        enemyRef.enemy.gameObject.SetActive(false);
    }
    private void RandomizeEnemyPosition(EnemyController enemyRef)
    {
        enemyRef.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
    }
    private void DeadEnemyRespawn()
    {
        for (int i = 0; i < deadEnemies.Count; i++)
        {
            EnemiesToRespawn enemyToRespawn = deadEnemies[i];
            if (enemyToRespawn.deathTimer > 0)
            {
                enemyToRespawn.deathTimer -= Time.deltaTime;
                deadEnemies[i] = enemyToRespawn;
            }
            else
            {
                enemyToRespawn.deathTimer = enemyToRespawn.maxTimer;
                deadEnemies[i] = enemyToRespawn;
                RandomizeEnemyPosition(enemyToRespawn.enemy);
                SetEnemyActive(deadEnemies[i]);
            }
        }
    }

}
