using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public GameObject UI;
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        UI.SetActive(false);
        Loading.instance.loadscene(sceneName);
        //SceneManager.LoadSceneAsync(sceneName);
    }
}
