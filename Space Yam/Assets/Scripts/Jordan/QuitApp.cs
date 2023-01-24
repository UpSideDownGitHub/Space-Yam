using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class QuitApp : MonoBehaviour
{
    public void QuitDaGame()
    {
        print("Quitting");
        Time.timeScale = 1f;
        Application.Quit();
    }
}
