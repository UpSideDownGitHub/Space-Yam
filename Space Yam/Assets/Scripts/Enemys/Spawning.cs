using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    /*
     *  NEEDED FEATURES:
     *      - spawn a wave of enemies
     *      - make sure all enemies are dead before moving to next wave
     *      - at the end of the wave spawn the boss
     *      - make it so that the more levels you play the harder it gets
    */

    [Header("General")]
    public int waveNumber;      // the current wave number
    public int bossLevel;       // the ammount of levels until a boss spawns

    [Header("Boss")]
    public bool bossDefeated;
    [SerializeField]
    private bool _isBossLevel;

    [Header("Wave Stats")]
    public int waveEnemiesLeft; // the ammount left (how many killed)
    public int waveEnemiesSpawned; // the ammount allready in game and so not needing to be spawned

    public int enemyCount;
    public float enemyHealth;
    public float enemyFirerate;
    public float bossHealth;
    public float bossFirerate;

    [Header("Increase Ammount")]
    // Ammount
    public float increaseAfterLevel_EnemyCount;
    public float increaseAfterBoss_EnemyCount;
    // Health
    public float increaseAfterLevel_EnemyHealth;
    public float increaseAfterBoss_EnemyHealth;
    // Shoot Rate
    public float decreaseAfterLevel_EnemyFirerate;
    public float decreaseAfterBoss_EnemyFirerate;

    [Header("Enemy Control")]
    public int maxEnemiesOnScreen;
    public int currentEnemiesOnScreen;

    [Header("Enemey Spawning")]
    public GameObject enemy;
    public GameObject bossEnemy;
    public GameObject[] spawnPositions;
    public GameObject bossSpawn;

    [Header("Wave Transition")]
    public float warpTime;
    public bool startWave;
    [SerializeField]
    private bool _nothing;

    public void Start()
    {
        startWave = true;
        StartWave();
    }

    public void StartWave()
    {
        waveEnemiesLeft = enemyCount;
        waveEnemiesSpawned = enemyCount;
        bossDefeated = false;

        if (waveNumber % bossLevel == 0 && waveNumber != 0)
            _isBossLevel = true;

        if (_isBossLevel)
        {
            // spawn a boss
            GameObject tempEnemy = Instantiate(bossEnemy, bossSpawn.transform.position, bossSpawn.transform.rotation);
            var enemyComp = tempEnemy.GetComponent<Enemy>();
            var healthComp = tempEnemy.GetComponent<EnemyHealth>();
            enemyComp.attackRate = bossFirerate;
            healthComp.maxHealth = bossHealth;
            healthComp.boss = true;
        }
        else
        {
            for (int i = currentEnemiesOnScreen; i < maxEnemiesOnScreen; i++)
            {
                var spawnPoint = spawnPositions[Random.Range(0, spawnPositions.Length)];
                GameObject tempEnemy = Instantiate(enemy, spawnPoint.transform.position, spawnPoint.transform.rotation);
                var enemyComp = tempEnemy.GetComponent<Enemy>();
                var healthComp = tempEnemy.GetComponent<EnemyHealth>();
                enemyComp.attackRate = enemyFirerate;
                healthComp.maxHealth = enemyHealth;
                healthComp.enemy = true;
                currentEnemiesOnScreen++;
                waveEnemiesSpawned--;
            }
        }
        startWave = false;
    }

    public void EndWave(bool bossLevel)
    {
        waveNumber++;
        if (bossLevel)
        {
            enemyCount = (int)(enemyCount * increaseAfterBoss_EnemyCount);
            enemyHealth *= increaseAfterBoss_EnemyHealth;
            enemyFirerate *= decreaseAfterBoss_EnemyFirerate;
            bossHealth *= increaseAfterBoss_EnemyHealth;
            bossFirerate *= decreaseAfterBoss_EnemyFirerate;
        }
        else
        {
            enemyCount = (int)(enemyCount * increaseAfterLevel_EnemyCount);
            enemyHealth *= increaseAfterLevel_EnemyHealth;
            enemyFirerate *= decreaseAfterLevel_EnemyFirerate;
            bossHealth *= increaseAfterLevel_EnemyHealth;
            bossFirerate *= decreaseAfterLevel_EnemyFirerate;
        }

        StartCoroutine(warpSpeed());
    }
    public IEnumerator warpSpeed()
    {
        _nothing = true;
        // code here to make the particle effect warp
        yield return new WaitForSeconds(warpTime);
        // and then unwarp
        startWave = true;
        _nothing = false;
    }


    public void CheckForWaveEnd()
    {
        if (_isBossLevel && bossDefeated)
        {
            EndWave(true);
        }
        else if (waveEnemiesLeft == 0)
        {
            EndWave(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_nothing)
            return;

        if (startWave)
        {
            StartWave();
            return;
        }

        if (currentEnemiesOnScreen < maxEnemiesOnScreen && waveEnemiesSpawned > 0 && !_isBossLevel)
        {
            for (int i = currentEnemiesOnScreen; i < maxEnemiesOnScreen; i++)
            {
                var spawnPoint = spawnPositions[Random.Range(0, spawnPositions.Length)];
                GameObject tempEnemy = Instantiate(enemy, spawnPoint.transform.position, spawnPoint.transform.rotation);
                var enemyComp = tempEnemy.GetComponent<Enemy>();
                var healthComp = tempEnemy.GetComponent<EnemyHealth>();
                enemyComp.attackRate = enemyFirerate;
                healthComp.maxHealth = enemyHealth;
                healthComp.enemy = true;
                currentEnemiesOnScreen++;
                waveEnemiesSpawned--;
            }
            return;
        }


        CheckForWaveEnd();
    }
}
