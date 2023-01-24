using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public GameObject[] healthObjects;

    // Start is called before the first frame update
    void Start()
    {
        health = healthObjects.Length;
    }

    public void removeHealth()
    {
        health--;
        if (health <= 0)
        {
            // reload the current scene
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
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