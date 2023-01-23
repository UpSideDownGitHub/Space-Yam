using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    [Header("Time")]
    public float shotTimer;
    private float _timeSinceLastShot;

    [Header("Pistol")]
    public float bulletForce;

    [Header("Laser")]
    public GameObject laserObject;
    public GameObject laserAimPos;
    public float shootTime;
    public Slider slider;

    private bool _shootingLaser;
    private float _sliderTime;
    private int _sliderMin = 0;
    private int _sliderMax = 100;


    [Header("INPUT")]
    public InputActionReference fire;
    public InputActionReference mousePosition;


    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;



        if (weaponType == weaponTypes.Laser)
        {
            slider.enabled = true;
            slider.minValue = _sliderMin;
            slider.maxValue = _sliderMax;
        }
        else
        {
            slider.enabled = false;
        }

        _timeSinceLastShot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // rotate the weapon to face the correct direction
        Vector3 mousePos1 = mousePosition.action.ReadValue<Vector2>();
        mousePos1 += _cam.transform.forward * 10f;
        var aim1 = _cam.ScreenToWorldPoint(mousePos1);
        currentGun.transform.LookAt(aim1);


        if (_shootingLaser)
        {
            if (fire.action.IsPressed())
            {
                if (slider.value >= _sliderMax)
                {
                    _shootingLaser = false;
                    laserObject.SetActive(false);
                    _timeSinceLastShot = Time.time;
                }
                else if (slider.value <= _sliderMax)
                {
                    _sliderTime += Time.deltaTime;
                    slider.value = Mathf.Lerp(_sliderMin, _sliderMax, _sliderTime / 2);
                }
            }

            if (fire.action.WasReleasedThisFrame())
            {
                laserObject.SetActive(false);
                _shootingLaser = false;
            }
        }
        else if (weaponType == weaponTypes.Laser)
        {
            if (slider.value > _sliderMin)
            {
                _sliderTime -= Time.deltaTime;
                slider.value = Mathf.Lerp(_sliderMin, _sliderMax, _sliderTime / 2);
            }
        }


        if ((weaponType == weaponTypes.Pistol || weaponType == weaponTypes.YamLauncher) && Time.time > shotTimer + _timeSinceLastShot)
        {
            if (fire.action.WasPressedThisFrame())
            {
                _timeSinceLastShot = Time.time;
                GameObject projectileClone = Instantiate(projectile, firepoint.transform.position, firepoint.transform.rotation);
                projectileClone.GetComponent<Rigidbody>().AddForce(projectileClone.transform.forward * bulletForce);
            }
        }
        else if (weaponType == weaponTypes.Laser && !_shootingLaser && Time.time > shotTimer + _timeSinceLastShot)
        {
            if (fire.action.WasPressedThisFrame())
            {
                laserObject.SetActive(true);
                _shootingLaser = true;
            }
        }
    }  
}
