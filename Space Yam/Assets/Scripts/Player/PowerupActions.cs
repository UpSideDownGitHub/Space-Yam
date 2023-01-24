using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupActions : MonoBehaviour
{
    /*
     *  POWER UPS
     *      - SHEILD (+1 HEALTH)
     *      - INCREASE FIRE RATE
     *      - INCREASE HEALTH BY 1
     *      - REDUCE LASER COOL DOWN
    */

    [Header("Links")]
    public PlayerHealth playerHealth;
    public Shooting pistol1;
    public Shooting pistol2;
    public Shooting laser;
    public Shooting rocketLauncher;
}
