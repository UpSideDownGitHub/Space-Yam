using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth;
    public float curHealth;
    public bool boss;

    [Header("Connections")]
    public Spawning spawning;

    public void Start()
    {
        curHealth = maxHealth;
        spawning = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<Spawning>();
    }

    public void removeHealth(float ammount)
    {
        if (curHealth - ammount <= 0)
        {
            // kill the player
            spawning.currentEnemiesOnScreen--;
            spawning.waveEnemiesLeft--;
            if (boss)
                spawning.bossDefeated = true;
            Destroy(gameObject);
        }
        curHealth -= ammount;
    }
}
