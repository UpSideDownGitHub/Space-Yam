using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PowerUpHealth : MonoBehaviour
{
    /*
 *  POWER UPS
 *      0 - SHEILD (+1 HEALTH)
 *      1 - INCREASE FIRE RATE
 *      2 - INCREASE HEALTH BY 1
 *      3 - REDUCE LASER COOL DOWN
*/

    public GameObject player;
    private PowerupActions _actions;

    private MeshRenderer _meshRenderer;
    private Collider _collider;

    public bool used;

    public GameObject effect;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _actions = player.GetComponent<PowerupActions>();

        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    public void use(int ID)
    {
        if (used)
            return;
        used = true;
        if (ID == 0)
        {
            _actions.playerHealth.sheild = true;
        }
        else if (ID == 1)
        {
            StartCoroutine(_actions.powerup_1());
        }
        else if (ID == 2)
        {
            _actions.playerHealth.increaseHealth();
        }
        else if (ID == 3)
        {
            StartCoroutine(_actions.powerup_3());
        }

        // disable the object
        _meshRenderer.enabled = false;
        _collider.enabled = false;

        Instantiate(effect, transform.position, transform.rotation);

        Destroy(gameObject, 10);
    }
}
