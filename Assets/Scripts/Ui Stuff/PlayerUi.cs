using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UFO_UI
{
public class PlayerUi : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private TextMeshProUGUI pressEToPickup;

    private bool playerHoldingObject = false;
    
    #endregion


    #region Methods
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
        {
            pressEToPickup.text = "Press E To Pick up";
            pressEToPickup.enabled = true;
        }
    }

    public void ReactToOutOfRangePickup()
    {
        if(!this.playerHoldingObject)
            pressEToPickup.enabled = false;
    }

    public void ReactToInRangeOfInteraction()
    {
        pressEToPickup.text = "Press E to Interact";
        pressEToPickup.enabled = true;
    }

    public void ReactToOutOfRangeOfInteraction()
    {
        pressEToPickup.enabled = false;
    }
}

#endregion
    
}
