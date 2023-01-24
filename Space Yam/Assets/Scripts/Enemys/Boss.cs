using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Boss : MonoBehaviour
{
    [Header("Navigation")]
    public NavMeshAgent agent;

    [Header("Path")]
    public float minDistance;
    public Vector3[] chosenPath;
    public int currentPoint;

    [Header("================================")]
    [Header("Attacking")]
    public GameObject player;
    public GameObject bullet;
    public float attackRate;

    private float _timeSinceLastAttack;
    private bool _attacking;

    [Header("Attack 1")]
    public GameObject[] A1_FirePoints;
    public float A1_gapBetweenShot;
    public float A1_BulletForce;

    [Header("Attack 2")]
    public GameObject[] A2_FirePoints;
    public float A2_gapBetweenShot;
    public float A2_BulletForce;

    [Header("Attack 3")]
    public GameObject[] A3_FirePoints;
    public float A3_gapBetweenShot;
    public float A3_BulletForce;

    [Header("Attack 4")]
    public GameObject[] A4_FirePoints;
    public float A4_BulletForce;


    [Header("Improvements")]
    public float speed;
    public float preNodeSpeed;
    public float minAttackDelay;
    public float maxAttackDelay;
    private bool _firstTime;
    private bool _reachedFirstNode;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent.SetDestination(new Vector3(chosenPath[currentPoint].x, chosenPath[currentPoint].y, transform.position.z));
        _firstTime = true;
        agent.speed = preNodeSpeed;
    }

    public void reduceFirerate(float ammount)
    {
        A1_gapBetweenShot *= ammount;
        A2_gapBetweenShot *= ammount;
        A3_gapBetweenShot *= ammount;
    }

    public void shoot(GameObject position, float fireForce)
    {
        position.transform.LookAt(player.transform);
        GameObject tempBullet = Instantiate(bullet, position.transform.position, position.transform.rotation);
        tempBullet.GetComponent<Rigidbody>().AddForce(tempBullet.transform.forward * fireForce);
    }

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(0);
        int attack = Random.Range(1, 5);
        if (attack == 1)
        {
            // shoot 5 bullets at the player in quick sucession
            for (int i = 0; i < A1_FirePoints.Length; i++)
            {
                shoot(A1_FirePoints[i], A1_BulletForce);
                yield return new WaitForSeconds(A1_gapBetweenShot);
            }
        }
        else if (attack == 2)
        {
            // shoot bullets in a spiral at the player
            for (int i = 0; i < A2_FirePoints.Length; i++)
            {
                shoot(A2_FirePoints[i], A2_BulletForce);
                yield return new WaitForSeconds(A2_gapBetweenShot);
            }
        }
        else if (attack == 3)
        {
            // shoot 3 double sets of bullets at the player
            for (int i = 0; i < A3_FirePoints.Length; i++)
            {
                
                shoot(A3_FirePoints[i], A3_BulletForce);
                i++;
                shoot(A3_FirePoints[i], A3_BulletForce);
                yield return new WaitForSeconds(A3_gapBetweenShot);

            }
        }
        else if (attack == 4)
        {
            // shoot 4 bullets at the player at once
            shoot(A4_FirePoints[0], A4_BulletForce);
            shoot(A4_FirePoints[1], A4_BulletForce);
            shoot(A4_FirePoints[2], A4_BulletForce);
            shoot(A4_FirePoints[3], A4_BulletForce);            
        }

        _timeSinceLastAttack = Time.time;
        _attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ATTACKING
        if (Time.time > attackRate + _timeSinceLastAttack && _reachedFirstNode && !_attacking)
        {
            _attacking = true;
            StartCoroutine(Attack());
        }

        // MOVEMENT
        agent.SetDestination(new Vector3(chosenPath[currentPoint].x, chosenPath[currentPoint].y, transform.position.z));
        if (Vector2.Distance(transform.position, chosenPath[currentPoint]) < minDistance)
        {
            if (currentPoint == 0 && _firstTime)
            {
                _firstTime = false;
                _reachedFirstNode = true;
                agent.speed = speed;

                _timeSinceLastAttack = Time.time + Random.Range(minAttackDelay, maxAttackDelay);
            }

            // set the destination to the new point
            currentPoint++;
            if (currentPoint == chosenPath.Length)
                currentPoint = 0;

            agent.SetDestination(new Vector3(chosenPath[currentPoint].x, chosenPath[currentPoint].y, transform.position.z));
        }
    }
}
