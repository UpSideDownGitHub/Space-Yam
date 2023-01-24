using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Jobs;
using System.Linq;

public class HighestOfScores : MonoBehaviour
{
    [Header("Highscore Enter Screen")]
    public TMP_Text[] nameText = new TMP_Text[10];
    public TMP_Text[] scoreText = new TMP_Text[10];

    public string[] names = new string[10];
    public int[] scores = new int[10];


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            names[i] = PlayerPrefs.GetString("Name_" + i, "");
            scores[i] = PlayerPrefs.GetInt("Score_" + i, 0);
        }

        setText();
    }

    public void setText()
    {
        for (int i = 0; i < 10; i++)
        {
            nameText[i].text = names[i];
            scoreText[i].text = scores[i].ToString();
        }

    }
}
