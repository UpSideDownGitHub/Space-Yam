using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public bool sheild;
    public GameObject[] healthObjects;

    public HighScore highscore;
    public GameObject endscreenone;
    public Score score;

    // Start is called before the first frame update
    void Start()
    {
        sheild = false;
        health = healthObjects.Length;
    }

    public void increaseHealth()
    {
        health++;
        if (health > 5)
        {
            health = 5;
        }
        var health1 = health - 1;
        for (int i = 0; i < healthObjects.Length; i++)
        {
            if (health1 >= i)
                healthObjects[i].SetActive(true);
            else
                healthObjects[i].SetActive(false);
        }
    }

    public void removeHealth()
    {
        if (sheild)
        {
            sheild = false;
            return;
        }

        health--;
        if (health <= 0)
        {
            endscreenone.SetActive(true);
            // reload the current scene
            highscore.gameFinished(score.score);
            return;
        }
        var health1 = health - 1;
        for (int i = 0; i < healthObjects.Length; i++)
        {
            if (health1 >= i)
                healthObjects[i].SetActive(true);
            else
                healthObjects[i].SetActive(false);
        }
    }
}