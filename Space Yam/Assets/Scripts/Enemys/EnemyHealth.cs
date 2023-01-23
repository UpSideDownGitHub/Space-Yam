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

    [Header("Connections")]
    public Spawning spawning;

    public bool killed;

    public void Start()
    {
        killed = false;
        curHealth = maxHealth;
        spawning = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<Spawning>();
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
            }
            if (boss)
                spawning.bossDefeated = true;
            Destroy(gameObject);
        }
        curHealth -= ammount;
    }
}
