using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth;
    public float curHealth;

    public void Start()
    {
        curHealth = maxHealth;
    }

    public void removeHealth(float ammount)
    {
        if (curHealth - ammount <= 0)
        {
            // kill the player
            Destroy(gameObject);
        }
        curHealth -= ammount;
    }
}
