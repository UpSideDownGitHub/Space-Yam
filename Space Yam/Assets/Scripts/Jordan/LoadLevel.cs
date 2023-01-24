using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(sceneName);
    }
}
