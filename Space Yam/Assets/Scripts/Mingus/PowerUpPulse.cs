using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PowerUpPulse : MonoBehaviour
{
    Vignette vignetteEffect;
    public PostProcessVolume globalVolume;
    private float speed;
    private bool tuneUp;

    public GameObject healthBar;
    public GameObject scoreCounter;
    public GameObject laserBar;

    private float colourShift;

    void Start()
    {
        globalVolume.GetComponent<PostProcessVolume>();
        globalVolume.profile.TryGetSettings(out vignetteEffect);
    }

    void Update()
    {
        speed += Time.deltaTime;

        if (tuneUp)
        {
            vignetteEffect.intensity.value = Mathf.Lerp(0.05f, 0.3f, 2 * speed);

            colourShift = Mathf.InverseLerp(0.05f, 0.3f, speed);
        }

        if (!tuneUp)
        {
            vignetteEffect.intensity.value = Mathf.Lerp(0.3f, 0.05f, 2 * speed);

            colourShift = Mathf.InverseLerp(0.3f, 0.05f, speed);
        }

        if (vignetteEffect.intensity.value <= 0.05f && !tuneUp)
        {
            tuneUp = true;

            speed = 0;
        }

        else if (vignetteEffect.intensity.value >= 0.3f && tuneUp)
        {
            tuneUp = false;

            speed = 0;
        }

        healthBar.GetComponent<Image>().color = new Color(colourShift, 1, colourShift);
        //healthBar.GetComponentInChildren<Image>().color = new Color(colourShift, 1, colourShift);
        scoreCounter.GetComponent<Image>().color = new Color(colourShift, 1, colourShift);
        //scoreCounter.GetComponentInChildren<Image>().color = new Color(colourShift, 1, colourShift);
        laserBar.GetComponent<Image>().color = new Color(colourShift, 1, colourShift);
    }
}
