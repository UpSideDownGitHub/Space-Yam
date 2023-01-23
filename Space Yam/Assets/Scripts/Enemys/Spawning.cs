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
    private bool _isBossLevel;

    [Header("Wave Stats")]
    public int waveEnemiesLeft; // the ammount left (how many killed)

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
    public float increaseAfterLevel_EnemyFirerate;
    public float increaseAfterBoss_EnemyFirerate;

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

    public void StartWave()
    {
        waveEnemiesLeft = enemyCount;
        bossDefeated = false;

        if (waveNumber % bossLevel == 0)
            _isBossLevel = true;

        if (_isBossLevel)
        {
            // spawn a boss
            GameObject tempEnemy = Instantiate(bossEnemy, bossSpawn.transform.position, bossSpawn.transform.rotation);
            var enemyComp = tempEnemy.GetComponent<Enemy>();
            var healthComp = tempEnemy.GetComponent<EnemyHealth>();
            enemyComp.attackRate = bossFirerate;
            healthComp.maxHealth = bossHealth;
        }
        else
        {
            for (int i = 0; i < maxEnemiesOnScreen; i++)
            {
                var spawnPoint = spawnPositions[Random.Range(0, spawnPositions.Length)];
                GameObject tempEnemy = Instantiate(enemy, spawnPoint.transform.position, spawnPoint.transform.rotation);
                var enemyComp = tempEnemy.GetComponent<Enemy>();
                var healthComp = tempEnemy.GetComponent<EnemyHealth>();
                enemyComp.attackRate = enemyFirerate;
                healthComp.maxHealth = enemyHealth;
                currentEnemiesOnScreen++;
            }
        }
        startWave = false;
    }

    public void EndWave(bool bossLevel)
    {
        if (bossLevel)
        {
            enemyCount = (int)(enemyCount * increaseAfterBoss_EnemyCount);
            enemyHealth *= increaseAfterBoss_EnemyHealth;
            enemyFirerate *= increaseAfterBoss_EnemyFirerate;
            bossHealth *= increaseAfterBoss_EnemyHealth;
            bossFirerate *= increaseAfterBoss_EnemyFirerate;
        }
        else
        {
            enemyCount = (int)(enemyCount * increaseAfterLevel_EnemyCount);
            enemyHealth *= increaseAfterLevel_EnemyHealth;
            enemyFirerate *= increaseAfterLevel_EnemyFirerate;
            bossHealth *= increaseAfterLevel_EnemyHealth;
            bossFirerate *= increaseAfterLevel_EnemyFirerate;
        }

        StartCoroutine(warpSpeed());
    }
    public IEnumerator warpSpeed()
    {
        // code here to make the particle effect warp
        yield return new WaitForSeconds(warpTime);
        // and then unwarp
        startWave = true;
    }


    public void CheckForWaveEnd()
    {
        if (_isBossLevel && bossDefeated)
        {
            EndWave(true);
        }
        if (waveEnemiesLeft == 0)
        {
            EndWave(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startWave)
            StartWave();

        if (currentEnemiesOnScreen < maxEnemiesOnScreen && waveEnemiesLeft > 0)
        {
            for (int i = 0; i < maxEnemiesOnScreen; i++)
            {
                var spawnPoint = spawnPositions[Random.Range(0, spawnPositions.Length)];
                GameObject tempEnemy = Instantiate(enemy, spawnPoint.transform.position, spawnPoint.transform.rotation);
                var enemyComp = tempEnemy.GetComponent<Enemy>();
                var healthComp = tempEnemy.GetComponent<EnemyHealth>();
                enemyComp.attackRate = enemyFirerate;
                healthComp.maxHealth = enemyHealth;
                currentEnemiesOnScreen++;
            }
        }


        CheckForWaveEnd();
    }
}
