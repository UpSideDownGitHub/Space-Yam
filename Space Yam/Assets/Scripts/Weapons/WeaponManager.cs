using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon")]
    public GameObject[] weapons;
    [SerializeField]
    private int _curWeapon;

    [Header("INPUT")]
    public InputActionReference weaponSwitch;


    void Update()
    {
        if (weaponSwitch.action.WasPressedThisFrame())
        {
            if (_curWeapon + 1 < weapons.Length)
            {
                weapons[_curWeapon].SetActive(false);
                _curWeapon++;
                weapons[_curWeapon].SetActive(true);
            }
            else
            {
                weapons[_curWeapon].SetActive(false);
                _curWeapon = 0;
                weapons[_curWeapon].SetActive(true);
            }
        }
    }
}
