using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth;
    public float curHealth;
    public bool boss;
    public bool enemy;

    [Header("Points")]
    public int bulletPoints;
    public int enemyPoints;

    [Header("Connections")]
    public Spawning spawning;
    public Score score;

    public bool killed;

    public void Start()
    {
        killed = false;
        curHealth = maxHealth;
        spawning = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<Spawning>();
        score = GameObject.FindGameObjectWithTag("Points").GetComponent<Score>();
    }

    public void removeHealth(float ammount)
    {
        if (killed)
            return;
        
        if (curHealth - ammount <= 0)
        {
            // kill the player
            killed = true;
            if (enemy)
            {
                spawning.currentEnemiesOnScreen--;
                spawning.waveEnemiesLeft--;
                score.increaseScore(enemyPoints);
            }
            else
                score.increaseScore(bulletPoints);
            if (boss)
                spawning.bossDefeated = true;
            Destroy(gameObject);
        }
        curHealth -= ammount;
    }
}
