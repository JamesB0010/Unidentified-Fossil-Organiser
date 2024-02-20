using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class canvasManager : MonoBehaviour
{
    public TMP_Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = PlayerPrefs.GetFloat("playerTime").ToString("F") + " seconds";
    }
}
