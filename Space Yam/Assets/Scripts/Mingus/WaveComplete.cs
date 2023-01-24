using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveComplete : MonoBehaviour
{
    //public TextMeshProUGUI waveText;
    public GameObject waveCompletedText;
    public GameObject waveCompletedBlob;

    void Start()
    {

    }
    void Update()
    {
        Animator blobAnimator = waveCompletedBlob.GetComponent<Animator>();
        blobAnimator.SetBool("waveCompleted", true);

        Animator textAnimator = waveCompletedText.GetComponent<Animator>();
        textAnimator.SetBool("waveCompleted", true);
    }
}
