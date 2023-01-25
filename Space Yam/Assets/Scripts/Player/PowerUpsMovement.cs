using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsMovement : MonoBehaviour
{
    [Header("Smooth Movement")]
    public Vector3[] nodes;
    public GameObject powerup;
    public float moveSpeed;
    public float timer;
    public Vector3 currentNodePosition;
    public int currentNodeID;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = powerup.transform.position;
        timer = 0;
        currentNodePosition = nodes[currentNodeID];
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * moveSpeed;
        if (powerup.transform.position != currentNodePosition)
        {
            powerup.transform.position = Vector3.Lerp(startPosition, currentNodePosition, timer);
        }
        else if (currentNodeID < nodes.Length - 1)
        {
            startPosition = currentNodePosition;
            currentNodeID++;
            timer = 0;
            currentNodePosition = nodes[currentNodeID];
        }
    }
}
