using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawning : MonoBehaviour
{
    [Header("General Variables")]
    [Range(0,1)]
    public float spawnChance;


    public GameObject[] powerups;
    public GameObject spawnPoint;

    public float spawnRate;
    private float _lastSpawnAttempt;

    public void Start()
    {
        _lastSpawnAttempt = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnRate + _lastSpawnAttempt)
        {
            _lastSpawnAttempt = Time.time;
            if (Random.value < spawnChance)
            {
                // spawn an enemy
                Instantiate(powerups[Random.Range(0, powerups.Length)], spawnPoint.transform.position, spawnPoint.transform.rotation);
            }
        }
    }
}
