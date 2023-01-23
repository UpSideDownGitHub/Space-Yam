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

    public List<Vector3[]> Paths = new List<Vector3[]>();

    public Vector3[] chosenPath;
    public int currentPoint;

    [Header("================================")]
    [Header("Attacking")]
    public GameObject player;
    public GameObject bullet;
    public float force;
    public float attackRate;
    private float _timeSinceLastAttack;


    // Start is called before the first frame update
    void Start()
    {
        Vector3[] path1 = { new Vector3(-18, 10, 0), 
                            new Vector3(-8 , 10, 0), 
                            new Vector3(-8 , 0 , 0), 
                            new Vector3(-18, 0 , 0)};
        Vector3[] path2 = { new Vector3(18, 10, 0),
                            new Vector3(8 , 10, 0),
                            new Vector3(8 , 0 , 0),
                            new Vector3(18, 0 , 0)};
        Vector3[] path3 = { new Vector3(-18, -10, 0),
                            new Vector3(-8 , -10, 0),
                            new Vector3(-8 , 0 , 0),
                            new Vector3(-18, 0 , 0)};
        Vector3[] path4 = { new Vector3(18, -10, 0),
                            new Vector3(8 , -10, 0),
                            new Vector3(8 , 0 , 0),
                            new Vector3(18, 0 , 0)};
        Vector3[] path5 = { new Vector3(0, 10 , 0),
                            new Vector3(0, -10, 0)};
        Vector3[] path6 = { new Vector3(20 , 0, 0),
                            new Vector3(-20, 0, 0)};
        Vector3[] path7 = { new Vector3(20, 10, 0),
                            new Vector3(-20, -10, 0)};
        Vector3[] path8 = { new Vector3(-20, 10, 0),
                            new Vector3(20, -10, 0)};


        Paths.Add(path1);
        Paths.Add(path2);
        Paths.Add(path3);
        Paths.Add(path4);
        Paths.Add(path5);
        Paths.Add(path6);
        Paths.Add(path7);
        Paths.Add(path8);


        // randomly set the path the enemy will take
        chosenPath = Paths[Random.Range(0, Paths.Count)];

        agent.SetDestination(new Vector3(chosenPath[currentPoint].x, chosenPath[currentPoint].y, transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        // attacking
        if (Time.time > attackRate + _timeSinceLastAttack)
        {
            _timeSinceLastAttack = Time.time;
            GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation);
            tempBullet.GetComponent<Rigidbody>().AddForce((transform.position - player.transform.position) * force);
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
