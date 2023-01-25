using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public GameObject[] images;
    public TextMeshProUGUI scoreText;

    private float colourShift;

    void Start()
    {
        globalVolume.GetComponent<PostProcessVolume>();
        globalVolume.profile.TryGetSettings(out vignetteEffect);
    }

    private void OnDisable()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].GetComponent<Image>().color = new Color(0, 1, 1);
        }

        scoreText.GetComponent<TMP_Text>().color = new Color(0, 1, 1);

        vignetteEffect.intensity.value = 0;
    }

    void Update()
    {
        speed += Time.deltaTime;

        if (tuneUp)
        {
            vignetteEffect.intensity.value = Mathf.Lerp(0.05f, 0.3f, 2 * speed);

            colourShift = Mathf.Lerp(1, 0, 2 * speed);
        }

        if (!tuneUp)
        {
            vignetteEffect.intensity.value = Mathf.Lerp(0.3f, 0.05f, 2 * speed);

            colourShift = Mathf.Lerp(0, 1, 2 * speed);
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

        for (int i = 0; i < images.Length; i++)
        {
            images[i].GetComponent<Image>().color = new Color(colourShift, 1, colourShift);
        }

        scoreText.GetComponent<TMP_Text>().color = new Color(colourShift, 1, colourShift);
    }
}
