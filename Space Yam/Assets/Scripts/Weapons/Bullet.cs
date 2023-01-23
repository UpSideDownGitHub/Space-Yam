using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool explodes;

    public float explosionRadius;

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
                        // deal damage to the enemy
                    }
                }
            }
            // deal damage to the enemy
            Destroy(gameObject);
        }
    }
}
