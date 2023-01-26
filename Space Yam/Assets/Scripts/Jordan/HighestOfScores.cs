using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Jobs;
using System.Linq;

public class HighestOfScores1 : MonoBehaviour
{
    [Header("Highscore Enter Screen")]
    public TMP_Text[] nameText = new TMP_Text[10];
    public TMP_Text[] scoreText = new TMP_Text[10];

    public string[] names = new string[10];
    public int[] scores = new int[10];

    public bool showScore;
    public TMP_Text scoreTextAsset;


    // Start is called before the first frame update
    void Awake()
    {
        if (showScore)
            {
            scoreTextAsset.text = PlayerPrefs.GetInt("Score").ToString();
            //StartCoroutine(drawScores2());
            return;

        }
            
        StopAllCoroutines();
        print("AHHHHH");
        StartCoroutine(drawScores());
    }

    public IEnumerator drawScores()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 10; i++)
        {
            names[i] = PlayerPrefs.GetString("Name_" + i, "");
            scores[i] = PlayerPrefs.GetInt("Score_" + i, 0);
            
        }
        for (int i = 0; i < 10; i++)
        {
            nameText[i].text = names[i];
            scoreText[i].text = scores[i].ToString();
            yield return new WaitForSeconds(0.3f);
        }
    }
    public IEnumerator drawScores2()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 10; i++)
        {
            names[i] = PlayerPrefs.GetString("Name_" + i, "");
            scores[i] = PlayerPrefs.GetInt("Score_" + i, 0);
            
        }
        for (int i = 0; i < 10; i++)
        {
            nameText[i].text = names[i];
            scoreText[i].text = scores[i].ToString();
            yield return new WaitForSeconds(0.3f);
        }
    }
}
