using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boneFacts : MonoBehaviour
{
    public string boneFact;

    void Start()
    {
        if(boneFact == null || boneFact == "")
        {
            boneFact = "No Fact Available";
        }
    }
}
