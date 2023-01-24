using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Jobs;
using System.Linq;
using static UnityEditor.PlayerSettings;

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
        if (scoreAchieved == 0)
        {
            // dont do anyhting as there is not high enough score so at this point will have to to the end screen which will then be activated
        }
        _currentScore = scoreAchieved;
        for (int i = 0; i < 10; i++)
        {
            if (scores[i] < scoreAchieved)
            {
                setScore(i, scoreAchieved);
                return;
            }
        }

        // ENABLE THE END SCREEN
    }

    public void setScore(int ID, int score)
    {
        // this does not work only workd fore bnumbers that are lower than the highscore
        int[] scoresArr = new int[scores.Length + 1];
        for (int i = 0; i < scores.Length + 1; i++)
        {
            if (i < ID - 1)
                scoresArr[i] = scores[i];
            else if (i == ID - 1)
                scoresArr[i] = score;
            else
                scoresArr[i] = scores[i - 1];
        }

        string[] namesArr = new string[names.Length + 1];
        for (int i = 0; i < scores.Length + 1; i++)
        {
            if (i < ID - 1)
                namesArr[i] = names[i];
            else if (i == ID - 1)
                namesArr[i] = "";
            else
                namesArr[i] = names[i - 1];
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
            nameText[i].text = names[i];
            scoreText[i].text = scores[i].ToString();
        }

    }

    public void ButtonPressed(string button)
    {
        if (button.Equals("DEL"))
        {
            if (_currentName.Equals(""))
            {
                return;
            }
            // remove the last item of the string
            _currentName = _currentName.Remove(_currentName.Length-1);
            nameText[_scoreID].text = _currentName;
            return;
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


            // ENABLE THE END SCREEN

            return;
        }
        if (_currentName.Length > 3)
            return;

        // if made it this far then we know a letter button has been pressed
        _currentName += button;
        nameText[_scoreID].text = _currentName;
    }
}
