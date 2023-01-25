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
    PowerUpActive powerUpActive;

    public GameObject player;
    private PowerupActions _actions;

    private MeshRenderer _meshRenderer;
    private Collider _collider;

    public bool used;

    public GameObject effect;

    public AudioSource PowerUpSFX;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _actions = player.GetComponent<PowerupActions>();

        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();

        powerUpActive = GameObject.FindGameObjectWithTag("PowerUpUI").GetComponent<PowerUpActive>();
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
            powerUpActive.setImage(0);

            StartCoroutine(_actions.powerup_1());
            
        }
        else if (ID == 2)
        {
            _actions.playerHealth.increaseHealth();
            
        }
        else if (ID == 3)
        {
            powerUpActive.setImage(1);
            StartCoroutine(_actions.powerup_3());
        }
        
        // disable the object
        _meshRenderer.enabled = false;
        _collider.enabled = false;

        Instantiate(effect, transform.position, transform.rotation);
        PowerUpSFX.Play();
        Destroy(gameObject, 10);
    }
}
