using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    public TMP_Text scoreText;

    public void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void increaseScore(int ammount)
    {
        score += ammount;
        scoreText.text = score.ToString();
    }
}
