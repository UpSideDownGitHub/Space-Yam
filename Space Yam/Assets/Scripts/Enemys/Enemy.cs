using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
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
    public GameObject firePoint;
    public float force;
    public float attackRate;
    private float _timeSinceLastAttack;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent.SetDestination(new Vector3(chosenPath[currentPoint].x, chosenPath[currentPoint].y, transform.position.z));
        _timeSinceLastAttack = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // attacking
        if (Time.time > attackRate + _timeSinceLastAttack)
        {
            _timeSinceLastAttack = Time.time;
            firePoint.transform.LookAt(player.transform);
            GameObject tempBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            tempBullet.GetComponent<Rigidbody>().AddForce(tempBullet.transform.forward * force);
        }

        // movement
        if (Vector2.Distance(transform.position, chosenPath[currentPoint]) < minDistance)
        {
            // set the destination to the new point
            currentPoint++;
            if (currentPoint == chosenPath.Length)
                currentPoint = 0;

            agent.SetDestination(new Vector3(chosenPath[currentPoint].x, chosenPath[currentPoint].y, transform.position.z));
        }
    }
}
