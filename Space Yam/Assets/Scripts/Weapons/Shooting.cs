using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum weaponTypes
{
    Pistol,
    Laser,
    YamLauncher
}

public class Shooting : MonoBehaviour
{
    public weaponTypes weaponType;

    private Camera _cam;
    public GameObject currentGun;

    [Header("Shooting General")]
    public GameObject firepoint;
    public GameObject projectile;

    [Header("Pistol")]
    public float bulletForce;

    [Header("Laser")]
    public GameObject laserObject;
    public GameObject laserAimPos;
    private bool _shootingLaser;


    [Header("INPUT")]
    public InputActionReference fire;
    public InputActionReference mousePosition;


    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (_shootingLaser)
        {
            if (fire.action.IsPressed())
            {
                Vector3 mousePos = mousePosition.action.ReadValue<Vector2>();
                mousePos += _cam.transform.forward * 10f;
                var aim = _cam.ScreenToWorldPoint(mousePos);

                laserAimPos.transform.LookAt(aim);
            }

            if (fire.action.WasReleasedThisFrame())
            {
                laserObject.SetActive(false);
                _shootingLaser = false;
            }
        }


        if (weaponType == weaponTypes.Pistol)
        {
            if (fire.action.WasPressedThisFrame())
            {
                Vector3 mousePos = mousePosition.action.ReadValue<Vector2>();
                mousePos += _cam.transform.forward * 10f;
                var aim = _cam.ScreenToWorldPoint(mousePos);

                firepoint.transform.LookAt(aim);

                GameObject projectileClone = Instantiate(projectile, firepoint.transform.position, firepoint.transform.rotation);
                projectileClone.GetComponent<Rigidbody>().AddForce(projectileClone.transform.forward * bulletForce);
            }
        }
        else if (weaponType == weaponTypes.Laser && !_shootingLaser)
        {
            if (fire.action.WasPressedThisFrame())
            {
                laserObject.SetActive(true);
                _shootingLaser = true;
            }
        }
        else if (weaponType == weaponTypes.YamLauncher)
        {

        }

    }

    
}
