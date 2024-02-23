using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void AllBonesCollected()
    {
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
    
    }
}
