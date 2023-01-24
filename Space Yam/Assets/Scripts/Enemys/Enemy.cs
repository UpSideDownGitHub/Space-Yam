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
    public Vector3[] Path1;
    public Vector3[] Path2;
    public Vector3[] Path3;
    public Vector3[] Path4;
    public Vector3[] Path5;
    public Vector3[] Path6;
    public Vector3[] Path7;
    public Vector3[] Path8;
    public List<Vector3[]> paths = new List<Vector3[]>();
    public int currentPoint;

    [Header("================================")]
    [Header("Attacking")]
    public GameObject player;
    public GameObject bullet;
    public GameObject firePoint;
    public float force;
    public float attackRate;
    private float _timeSinceLastAttack;

    [Header("Better Shooting")]
    public float speed;
    public float preNodeSpeed;
    public float minAttackDelay;
    public float maxAttackDelay;
    private bool _firstTime;
    private bool _reachedFirstNode;


    // Start is called before the first frame update
    void Start()
    {
        paths.Add(Path1);
        paths.Add(Path2);
        paths.Add(Path3);
        paths.Add(Path4);
        paths.Add(Path5);

        paths.Add(Path6);
        paths.Add(Path7);
        paths.Add(Path8);

        chosenPath = paths[Random.Range(0, paths.Count)];

        player = GameObject.FindGameObjectWithTag("Player");
        agent.SetDestination(new Vector3(chosenPath[currentPoint].x, chosenPath[currentPoint].y, transform.position.z));
        _firstTime = true;
        agent.speed = preNodeSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // attacking
        if (Time.time > attackRate + _timeSinceLastAttack && _reachedFirstNode)
        {
            _timeSinceLastAttack = Time.time + Random.Range(minAttackDelay, maxAttackDelay);
            firePoint.transform.LookAt(player.transform);
            GameObject tempBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
            tempBullet.GetComponent<Rigidbody>().AddForce(tempBullet.transform.forward * force);
        }

        agent.SetDestination(new Vector3(chosenPath[currentPoint].x, chosenPath[currentPoint].y, transform.position.z));
        // movement
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
