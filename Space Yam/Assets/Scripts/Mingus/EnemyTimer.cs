using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTimer : MonoBehaviour
{
    public EnemySpawn enemyScript;
    public GameObject enemyBasic;

    private float spawnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        spawnCooldown = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        spawnCooldown -= Time.deltaTime;

        if (spawnCooldown <= 0)
        {
            //enemyScript.Awake();

            Instantiate(enemyBasic, enemyScript.enemySpawnPosition, Quaternion.identity);

            spawnCooldown = 2.5f;
        }
    }
}
