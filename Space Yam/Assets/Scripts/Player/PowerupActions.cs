using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupActions : MonoBehaviour
{
    /*
     *  POWER UPS
     *      0 - SHEILD (+1 HEALTH)
     *      1 - INCREASE FIRE RATE
     *      2 - INCREASE HEALTH BY 1
     *      3 - REDUCE LASER COOL DOWN
    */

    [Header("Links")]
    public PlayerHealth playerHealth;
    public GameObject pistol1;
    public GameObject pistol2;
    public GameObject laser;
    public GameObject rocketLauncher;

    [Header("Power-up Effect")]
    [Header("Power-up 1")]
    public float increasedShootRate;
    public float P1_time;

    [Header("Power-up 4")]
    public float laserIncreaseRate;
    public float P4_time;

    public IEnumerator powerup_1()
    {
        float oldFireRate1 = pistol1.GetComponent<Shooting>().shotTimer;
        float oldFireRate2 = pistol2.GetComponent<Shooting>().shotTimer;
        float oldFireRate3 = rocketLauncher.GetComponent<Shooting>().shotTimer;

        pistol1.GetComponent<Shooting>().shotTimer = increasedShootRate;
        pistol2.GetComponent<Shooting>().shotTimer = increasedShootRate;
        rocketLauncher.GetComponent<Shooting>().shotTimer = increasedShootRate;

        yield return new WaitForSeconds(P1_time);

        pistol1.GetComponent<Shooting>().shotTimer = oldFireRate1;
        pistol2.GetComponent<Shooting>().shotTimer = oldFireRate2;
        rocketLauncher.GetComponent<Shooting>().shotTimer = oldFireRate3;
    }

    public IEnumerator powerup_3()
    {
        float laserIncreaseTime = laser.GetComponent<Shooting>().laserIncreaseTime;

        laser.GetComponent<Shooting>().laserIncreaseTime = laserIncreaseRate;

        yield return new WaitForSeconds(P4_time);

        laser.GetComponent<Shooting>().laserIncreaseTime = laserIncreaseTime;
    }
}
