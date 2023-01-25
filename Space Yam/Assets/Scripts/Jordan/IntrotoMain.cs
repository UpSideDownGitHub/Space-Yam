using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;



public class IntrotoMain : MonoBehaviour
{
    public float TimeToSwitch;
    public InputActionReference fire;

    void Start()
    {
        StartCoroutine(nameof(SwitchToMenu));
    }
    IEnumerator SwitchToMenu()
    {
        yield return new WaitForSecondsRealtime(TimeToSwitch);
        SceneManager.LoadScene("MainMenu");
        print("ChangeScene");
    }
    private void Update()
    {
        if (fire.action.WasPerformedThisFrame())
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
