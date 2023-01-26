#define CLEARPLAYERPREFS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Jobs;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

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

    [Header("UI")]
    public GameObject[] playerUI;

    [Header("Swap Screen")]

    public GameObject endscreen;
    public GameObject highscorescreen;

    public GameObject gameOverText;
    public float gameOverTime;

    public void Start()
    {
#if CLEARPLAYERPREFS
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetString("Name_" + i, "");
            PlayerPrefs.SetInt("Score_" + i, 0);
        }
#endif
        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                PlayerPrefs.SetString("Name_" + i, "Yam Devs");
                PlayerPrefs.SetInt("Score_" + i, 6942069);
            }
            names[i] = PlayerPrefs.GetString("Name_" + i, "");
            scores[i] = PlayerPrefs.GetInt("Score_" + i, 0);
        }
        setText();
    }

    public IEnumerator gameOver(int score)
    {
        // disable all of the player UI
        foreach (var item in playerUI)
        {
            item.SetActive(false);
        }
        gameOverText.SetActive(true);
        yield return new WaitForSeconds(gameOverTime);
        gameOverText.SetActive(false);
        endscreen.SetActive(true);
        gameFinished(score);
    }
    public void gameFinished(int scoreAchieved)
    {
        Time.timeScale = 0f;


        PlayerPrefs.SetInt("Score", scoreAchieved);

        playerScoreText.text = scoreAchieved.ToString();
        if (scoreAchieved == 0)
        {
            SceneManager.LoadSceneAsync(2);
            return;
        }
        _currentScore = scoreAchieved;
        for (int i = 0; i < 10; i++)
        {
            if (scores[i] < scoreAchieved)
            {
                print("Place: " + i);
                setScore(i, scoreAchieved);
                return;
            }
        }

        // ENABLE THE END SCREEN
        SceneManager.LoadSceneAsync("End Screen");
    }

    public void setScore(int ID, int score)
    {
        ID++;
        // this does not work only workd fore bnumbers that are lower than the highscore
        int[] scoresArr = new int[scores.Length + 1];
        for (int i = 0; i < scores.Length + 1; i++)
        {
            if (i < ID - 1)
                scoresArr[i] = scores[i];
            else if (i == ID - 1)
            {
                
                scoresArr[i] = score;
            }
            else
                scoresArr[i] = scores[i - 1];
        }

        string[] namesArr = new string[names.Length + 1];
        for (int i = 0; i < names.Length + 1; i++)
        {
            if (i < ID - 1)
                namesArr[i] = names[i];
            else if (i == ID - 1)
                namesArr[i] = "";
            else
                namesArr[i] = names[i - 1];
        }

        for (int i = 0; i < 10; i++)
        {
            scores[i] = scoresArr[i];
            names[i] = namesArr[i];
        }

        //scores[ID] = score;
        //names[ID] = ""; // nothing as this is to be set by the player
        _currentName = "";
        _scoreID = --ID;
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
            SceneManager.LoadSceneAsync("End Screen");

            return;
        }
        if (_currentName.Length > 3)
            return;

        // if made it this far then we know a letter button has been pressed
        _currentName += button;
        nameText[_scoreID].text = _currentName;
    }
}
