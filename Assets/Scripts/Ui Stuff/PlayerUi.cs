using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UFO
{
public class PlayerUi : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pressEToPickup;
    
    public void ReactToInRangeOfPickup()
    {
        pressEToPickup.enabled = true;
    }

    public void ReactToOutOfRangePickup()
    {
        pressEToPickup.enabled = false;
    }
}
    
}
