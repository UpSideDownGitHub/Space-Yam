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
            if (other.CompareTag("Enemy"))
            {
                if (explodes)
                {
                    GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
                    Debug.Log("Ammount of Enemies: " + enemys.Length);
                    Debug.Log("Explosion Radius: " + explosionRadius); 
                    for (int i = 0; i < enemys.Length; i++)
                    {
                        Debug.Log("Distance: " + Vector3.Distance(transform.position, enemys[i].transform.position));
                        if (Vector3.Distance(transform.position, enemys[i].transform.position) < explosionRadius)
                        {
                            print("Attacked Another enemy");
                            enemys[i].GetComponent<EnemyHealth>().removeHealth(damage);
                        }
                    }
                    return;
                }
                other.gameObject.GetComponent<EnemyHealth>().removeHealth(damage);
                if (!laser)
                    Destroy(gameObject);
            }
        }
    }
}
