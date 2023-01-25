using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth;
    public float curHealth;
    public bool boss;
    public bool enemy;

    public Slider healthBar;

    public GameObject deathParticle;

    [Header("Points")]
    public int bulletPoints;
    public int enemyPoints;

    [Header("Connections")]
    public Spawning spawning;
    public Score score;

    public bool killed;
    private bool _first;

    public void Start()
    {
        _first = true;
        killed = false;
        curHealth = maxHealth;
        spawning = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<Spawning>();
        score = GameObject.FindGameObjectWithTag("Points").GetComponent<Score>();
        if (enemy || boss)
        {
            healthBar.minValue = 0;
            healthBar.maxValue = maxHealth;
            healthBar.value = curHealth;
        }
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
            // spawn a particle system at death point
            Instantiate(deathParticle, transform.position, transform.rotation);

            Destroy(gameObject);
        }
        curHealth -= ammount;
        if (enemy || boss)
        {
            if (_first)
            {
                _first = false;

                healthBar.minValue = 0;
                healthBar.maxValue = maxHealth;
                healthBar.value = curHealth;
            }
            healthBar.value = curHealth;
        }
    }
}
