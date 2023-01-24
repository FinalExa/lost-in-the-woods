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
        public bool hasFixedSpawnPositionOnRespawn;
        public GameObject fixedSpawnPosition;
        public bool doesntRespawn;
        public float deathCooldown;
    }
    [System.Serializable]
    public struct EnemiesToRespawn
    {
        public EnemyController enemy;
        public bool doesntRespawn;
        public bool fixedSpawn;
        public GameObject fixedSpawnPosition;
        public float maxTimer;
        public float deathTimer;
    }
    [SerializeField] private SpawnerEnemies[] enemiesToSpawnInThisZone;
    [SerializeField] private GameObject[] spawnPoints;
    private List<EnemiesToRespawn> activeEnemies;
    private List<EnemiesToRespawn> deadEnemies;
    private bool spawnerIsSet;

    private void OnEnable()
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
            EnemiesToRespawn enemyToRespawn = CreateEnemyToRespawn(enemyRef, enemyToSpawn.deathCooldown, enemyToSpawn.doesntRespawn, enemyToSpawn.hasFixedSpawnPositionOnRespawn, enemyToSpawn.fixedSpawnPosition);
            enemyRef.spawnerRef = this;
            enemyRef.spawnerEnemyInfo = enemyToRespawn;
            enemyRef.isAlerted = false;
            if (enemyToSpawn.startsSpawned)
            {
                if (enemyToSpawn.firstSpawnPosition != null) enemyToSpawn.enemy.transform.position = enemyToSpawn.firstSpawnPosition.transform.position;
                else RandomizeEnemyPosition(enemyToSpawn.enemy);
                SetEnemyActive(enemyToRespawn);
            }
            else SetEnemyDead(enemyToRespawn, true);
        }
    }
    private EnemiesToRespawn CreateEnemyToRespawn(EnemyController enemyRef, float timer, bool _doesntRespawn, bool _fixedSpawn, GameObject fixedSpawnObj)
    {
        EnemiesToRespawn enemyToRespawn = new EnemiesToRespawn();
        enemyToRespawn.enemy = enemyRef;
        enemyToRespawn.doesntRespawn = _doesntRespawn;
        enemyToRespawn.fixedSpawn = _fixedSpawn;
        enemyToRespawn.fixedSpawnPosition = fixedSpawnObj;
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


    public void SetEnemyDead(EnemiesToRespawn enemyRef, bool firstTime)
    {
        if (!deadEnemies.Contains(enemyRef) && (!enemyRef.doesntRespawn || firstTime)) deadEnemies.Add(enemyRef);
        if (activeEnemies.Contains(enemyRef)) activeEnemies.Remove(enemyRef);
        if (enemyRef.enemy.interaction != null) enemyRef.enemy.interaction.despawned = true;
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
                if (!enemyToRespawn.fixedSpawn || (enemyToRespawn.fixedSpawn && enemyToRespawn.fixedSpawnPosition == null)) RandomizeEnemyPosition(enemyToRespawn.enemy);
                else enemyToRespawn.enemy.transform.position = enemyToRespawn.fixedSpawnPosition.transform.position;
                SetEnemyActive(deadEnemies[i]);
            }
        }
    }
}
