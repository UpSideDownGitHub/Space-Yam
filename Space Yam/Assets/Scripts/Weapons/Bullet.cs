using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("General")]
    public bool enemy;
    public float damage;

    [Header("Explosion")]
    public bool explodes;
    public float explosionRadius;

    [Header("Laser")]
    public bool laser;

    public void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (enemy)
        {
            //   MAKE THIS UES THE PLAYER HEATH INSTED OF THE ENEMY HEALTH AS THIS IS STILL NOT FIXED
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerHealth>().removeHealth();
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.CompareTag("PowerUp"))
            {
                if (explodes)
                {
                    GameObject[] powerups = GameObject.FindGameObjectsWithTag("PowerUp");
                    for (int i = 0; i < powerups.Length; i++)
                    {
                        if (Vector3.Distance(transform.position, powerups[i].transform.position) < explosionRadius)
                        {
                            powerups[i].GetComponent<PowerUpHealth>().use(powerups[i].GetComponent<PowerupID>().ID);
                        }
                    }
                    Destroy(gameObject);
                    return;
                }
                other.gameObject.GetComponent<PowerUpHealth>().use(other.gameObject.GetComponent<PowerupID>().ID);
                if (!laser)
                    Destroy(gameObject);
            }
            if (other.CompareTag("Enemy"))
            {
                if (explodes)
                {
                    GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
                    for (int i = 0; i < enemys.Length; i++)
                    {
                        if (Vector3.Distance(transform.position, enemys[i].transform.position) < explosionRadius)
                        {
                            enemys[i].GetComponent<EnemyHealth>().removeHealth(damage);
                        }
                    }
                    Destroy(gameObject);
                    return;
                }
                other.gameObject.GetComponent<EnemyHealth>().removeHealth(damage);
                if (!laser)
                    Destroy(gameObject);
            }
        }
    }
}
