using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScroller : MonoBehaviour
{
    [SerializeField] private Transform parentTransform;
    
    void Start()
    {
        parentTransform.position.y.LerpTo(2000, 13, value =>
        {
            parentTransform.position = new Vector3(parentTransform.position.x, value, parentTransform.position.z);
        },
            pkg =>
            {
                SceneManager.LoadScene(0, LoadSceneMode.Single);
            });
    }
    
}
