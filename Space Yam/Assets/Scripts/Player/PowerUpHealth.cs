using System.Collections;
using System.Collections.Generic;
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

    [Header("Power-up Effect")]
    [Header("Power-up 1")]
    public float increasedShootRate;
    public float P1_time;

    [Header("Power-up 4")]
    public float laserIncreaseRate;
    public float P4_time;

    public GameObject player;
    private PowerupActions _actions;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _actions = player.GetComponent<PowerupActions>();
    }

    public void use(int ID)
    {
        if (ID == 0)
        {
            _actions.playerHealth.sheild = true;
        }
        else if (ID == 1)
        {
            StartCoroutine(powerup_1());
        }
        else if (ID == 2)
        {
            _actions.playerHealth.increaseHealth();
        }
        else if (ID == 3)
        {

        }
    }

    public IEnumerator powerup_1()
    {
        float oldFireRate1 = _actions.pistol1.attackTime;
        float oldFireRate2 = _actions.pistol2.attackTime;
        float oldFireRate3 = _actions.rocketLauncher.attackTime;

        _actions.pistol1.attackTime = increasedShootRate;
        _actions.pistol2.attackTime = increasedShootRate;
        _actions.rocketLauncher.attackTime = increasedShootRate;

        yield return new WaitForSeconds(P1_time);

        _actions.pistol1.attackTime = oldFireRate1;
        _actions.pistol1.attackTime = oldFireRate2;
        _actions.pistol1.attackTime = oldFireRate3;
    }
    public IEnumerator powerup_3()
    {
        float laserIncreaseTime = _actions.laser.laserIncreaseTime;

        _actions.laser.laserIncreaseTime = laserIncreaseRate;

        yield return new WaitForSeconds(P4_time);

        _actions.laser.laserIncreaseTime = laserIncreaseTime;
    }
}
