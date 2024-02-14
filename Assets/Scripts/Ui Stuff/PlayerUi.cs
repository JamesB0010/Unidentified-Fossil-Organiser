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

    private bool playerHoldingObject = false;

    public void ReactToPlayerPickUpObject()
    {
        pressEToPickup.enabled = false;
        this.playerHoldingObject = true;
    }

    public void ReactToPlayerDroppedObject()
    {
        this.playerHoldingObject = false;
    }
    
    public void ReactToInRangeOfPickup()
    {
        if (!this.playerHoldingObject)
            pressEToPickup.enabled = true;
    }

    public void ReactToOutOfRangePickup()
    {
        if(!this.playerHoldingObject)
            pressEToPickup.enabled = false;
    }
}
    
}
