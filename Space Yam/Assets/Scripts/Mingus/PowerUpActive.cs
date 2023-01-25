using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpActive : MonoBehaviour
{
    public GameObject powerUpRing;
    public GameObject powerUpCover;

    public GameObject fireRateIcon;
    public GameObject coolDownIcon;

    float currentVal;

    public GameObject jazz;


    public void setImage(int ID)
    {
        if (ID == 0)
        {
            // fire rate image
            fireRateIcon.SetActive(true);
        }
        else if (ID == 1)
        {
            // laser cool down
            coolDownIcon.SetActive(true);
        }

        StartCoroutine(ringCountdown());
    }

    public IEnumerator ringCountdown()
    {
        jazz.SetActive(true);

        float fromVal = 1;
        float toVal = 0;
        float duration = 5;

        float counter = 0f;

        // decrease outer ring
        while (counter < duration)
        {
            if (Time.timeScale == 0)
                counter += Time.unscaledDeltaTime;
            else
                counter += Time.deltaTime;

            float val = Mathf.Lerp(fromVal, toVal, counter / duration);
            Debug.Log("Val: " + val);
            currentVal = val;
            powerUpRing.GetComponent<Image>().fillAmount = currentVal;
            yield return null;
        }

        // ring is minimum

        // increase outer ring and enble inner ring
        counter = 0f;
        fromVal = 0;
        toVal = 1;
        duration = 0.5f;

        powerUpCover.SetActive(true);

        while (counter < duration)
        {
            if (Time.timeScale == 0)
                counter += Time.unscaledDeltaTime;
            else
                counter += Time.deltaTime;

            float val = Mathf.Lerp(fromVal, toVal, counter / duration);
            Debug.Log("Val: " + val);
            currentVal = val;
            powerUpRing.GetComponent<Image>().fillAmount = currentVal;
            yield return null;
        }

        // outer == maximum
        // inner finshed circle
        fireRateIcon.SetActive(false);
        coolDownIcon.SetActive(false);


        yield return new WaitForSeconds(1.5f);
        powerUpCover.gameObject.SetActive(false);
        jazz.SetActive(false);
    }

    /*
    public IEnumerator deactiveDelay()
    {
        yield return new WaitForSeconds(1f);
        powerUpCover.gameObject.SetActive(false);
    }

    IEnumerator changeValueOverTime(float fromVal, float toVal, float duration)
    {
        float counter = 0f;

        while (counter < duration)
        {
            if (Time.timeScale == 0)
                counter += Time.unscaledDeltaTime;
            else
                counter += Time.deltaTime;

            float val = Mathf.Lerp(fromVal, toVal, counter / duration);
            Debug.Log("Val: " + val);
            currentVal = val;
            yield return null;
        }
        doOnce = false;
    }
    void Update()
    {        
        if (active)
        {
            if (!doOnce)
            {
                doOnce = true;
                StartCoroutine(changeValueOverTime(1, 0, 5));
                
            }

            powerUpRing.GetComponent<Image>().fillAmount = currentVal;
            
        }

        if (!active && !finished)
        {
            powerUpCover.gameObject.SetActive(true);
            StartCoroutine(deactiveDelay());

            if (!doOnce)
            {
                doOnce = true;
                StartCoroutine(changeValueOverTime(0, 1, 0.5f));

            }

            powerUpRing.GetComponent<Image>().fillAmount = currentVal;


            if (currentVal == 1)
            {
                for (int i = 0; i < symbols.Length; i++)
                {
                    symbols[i].gameObject.SetActive(false);
                }

                //active = true;
            }
        }

        if (powerUpRing.GetComponent<Image>().fillAmount <= 0 && active)
        {
            active = false;
            finished = false;
        }
        */
}
