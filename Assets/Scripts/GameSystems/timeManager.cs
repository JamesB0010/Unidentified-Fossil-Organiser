using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeManager : MonoBehaviour
{
    private float currentPlayTime;
    private bool isGameOver;

    public float CurrentPlayTime
    {
        get
        {
            return currentPlayTime;
        }

        set
        {
            if (value != currentPlayTime)
            {
                currentPlayTime = value;
            }
        }
    }

    public bool IsGameOver
    {
        get
        {
            return isGameOver;
        }

        set
        {
            isGameOver = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver)
        {
            PlayerPrefs.SetFloat("playerTime", currentPlayTime);
        }
        else if (PlayerPrefs.GetFloat("playerTime") == 0 || PlayerPrefs.GetFloat("playerTime") == null)
        {
            currentPlayTime += Time.deltaTime;
        }
        else
        {
            PlayerPrefs.DeleteKey("playerTime");
        }
    }
}
