using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("General")]
    public float damage;

    [Header("Explosion")]
    public bool explodes;
    public float explosionRadius;

    [Header("Laser")]
    public bool laser;
    public float laserHitTime;
    private float _laserLastHitTime;

    public void Start()
    {
        _laserLastHitTime = 0;
    }

    public void OnTriggerStay(Collider other)
    {
        if (!laser)
            return;
        if (other.CompareTag("Enemy"))
        {
            if (Time.time > _laserLastHitTime + laserHitTime)
            {
                _laserLastHitTime = Time.time;
                other.gameObject.GetComponent<EnemyHealth>().removeHealth(damage);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (explodes)
            {
                GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < enemys.Length; i++)
                {
                    if (Vector3.Distance(transform.position, enemys[i].transform.position) > explosionRadius)
                    {
                        enemys[i].GetComponent<EnemyHealth>().removeHealth(damage);
                    }
                }
            }
            // deal damage to the enemy
            Destroy(gameObject);
        }
    }
}
