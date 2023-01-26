using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    // singleton
    public static Loading instance;

    // variables
    public bool loading = false;
    public GameObject loadingScreen;
    public Slider slider;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(loadingScreen);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void loadscene(string scene)
    {
        StartCoroutine(loadSceneAsync(scene));
    }

    IEnumerator loadSceneAsync(string ID)
    {
        loading = true;
        AsyncOperation operation = SceneManager.LoadSceneAsync(ID);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progression = Mathf.Clamp01(operation.progress);
            slider.value = progression;
            yield return null;
        }
        loadingScreen.SetActive(false);
    }

}