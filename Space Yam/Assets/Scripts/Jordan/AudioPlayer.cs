using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource clip1;
    public AudioSource clip2;
    public AudioSource clip3;

    // Start is called before the first frame update
    public void FirstClip()
    {
        clip1.Play();
    }

    public void SecondClip()
    {
        clip2.Play();
    }
    public void ThirdClip()
    {
        clip3.Play();
    }
}
