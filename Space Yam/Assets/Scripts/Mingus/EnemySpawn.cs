using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //public Animator enemySpawnAnimation;
    private int animationID;

    public Vector3 enemySpawnPosition;

    private float positionX;
    private float positionY;

    void Start()
    {
        animationID = UnityEngine.Random.Range(1, 4);

        print(animationID);

        Animator enemyAnimator = gameObject.GetComponent<Animator>();
        enemyAnimator.SetInteger("stateID", animationID);

        if (animationID == 2)
        {
            positionX = UnityEngine.Random.Range(-50, 50);
            enemySpawnPosition = new Vector3(positionX, 50, -30);
        }
        else
        {
            positionY = UnityEngine.Random.Range(-50, 50);
            enemySpawnPosition = new Vector3(0, positionY, -30);
        }
    }
}
