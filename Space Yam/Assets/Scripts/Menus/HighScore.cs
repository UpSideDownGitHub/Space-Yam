using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Jobs;
using System.Linq;

public class HighScore : MonoBehaviour
{
    [Header("Highscore Enter Screen")]
    public TMP_Text[] nameText = new TMP_Text[10];
    public TMP_Text[] scoreText = new TMP_Text[10];

    public string[] names = new string[10];
    public int[] scores = new int[10];

    public TMP_Text playerScoreText;

    private int _currentScore;
    private string _currentName;
    private int _scoreID;

    public void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            names[i] = PlayerPrefs.GetString("Name_" + i, "");
            scores[i] = PlayerPrefs.GetInt("Score_" + i, 0);
        }

        setText();
    }

    public void gameFinished(int scoreAchieved)
    {
        Time.timeScale = 0f;

        playerScoreText.text = scoreAchieved.ToString();
        _currentScore = scoreAchieved;
        for (int i = 0; i < 10; i++)
        {
            if (scores[i] < scoreAchieved)
            {
                setScore(i, scoreAchieved);
                break;
            }
        }
    }

    public void setScore(int ID, int score)
    {
        for (int i = 9; i >= ID; i--)
        {
            scores[i-1] = scores[i];
            names[i-1] = names[i];
        }
        scores[ID] = score;
        names[ID] = ""; // nothing as this is to be set by the player
        _currentName = "";
        _scoreID = ID;
        setText();
    }

    public void setText()
    {
        for (int i = 0; i < 10; i++)
        {
            if (!names[i].Equals(""))
            {
                nameText[i].text = names[i];
                scoreText[i].text = scores[i].ToString();
            }
        }
    }

    public void ButtonPressed(string button)
    {
        if (button.Equals("DEL"))
        {
            // remove the last item of the string
            _currentName.Remove(_currentName.Length);
        }
        if (button.Equals("OK"))
        {
            // save the score and move to the next section
            if (_currentName.Equals(""))
            {
                return;
            }

            // set the new scores
            scores[_scoreID] = _currentScore;
            names[_scoreID] = _currentName;

            // save the scores
            for (int i = 0; i < 10; i++)
            {
                PlayerPrefs.SetString("Name_" + i, names[i]);
                PlayerPrefs.SetInt("Score_" + i, scores[i]);
            }
        }

        // if made it this far then we know a letter button has been pressed
        _currentName += button;
    }
}
