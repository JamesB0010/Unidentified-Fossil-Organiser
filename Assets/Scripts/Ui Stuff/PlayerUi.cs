using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace UFO_UI
{
public class PlayerUi : MonoBehaviour
{
    #region Attributes
    [SerializeField]
    private TextMeshProUGUI pressEToPickup;

    private bool playerHoldingObject = false;

    [SerializeField] private GameObject InactivityScreen;
    
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


    private bool playerInactive;

    private bool PlayerInactive
    {
        get
        {
            return playerInactive;
        }

        set
        {
            if (value == playerInactive)
                return;

            playerInactive = value;
            
            LerpMenuInOrOut(value);
        }
    }

    [SerializeField] private AnimationCurve inactivityNotificationLerpCurve;
    void LerpMenuInOrOut(bool lerpIn)
    {
        if (lerpIn)
        {
            foreach (var image in this.InactivityScreen.transform.GetComponentsInChildren<UnityEngine.UI.Image>())
            {
                0.0f.LerpTo(1.0f,2,
                    value =>
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, value);
                    },
                    null,
                    this.inactivityNotificationLerpCurve
                    );
                
            }

            foreach (var text in this.InactivityScreen.transform.GetComponentsInChildren<TextMeshProUGUI>())
            {
                0.0f.LerpTo(1.0f,2,
                    value =>
                    {
                        text.color = new Color(text.color.r, text.color.g, text.color.b, value);
                    },
                    null,
                    this.inactivityNotificationLerpCurve
                    );
            }
        }

        else
        {
            foreach (var image in this.InactivityScreen.transform.GetComponentsInChildren<UnityEngine.UI.Image>())
            {
                1.0f.LerpTo(0.0f,1,
                    value =>
                    {
                        image.color = new Color(image.color.r, image.color.g, image.color.b, value);
                    },
                    null,
                    this.inactivityNotificationLerpCurve
                );
            }

            foreach (var text in this.InactivityScreen.transform.GetComponentsInChildren<TextMeshProUGUI>())
            {
                1.0f.LerpTo(0.0f,1,
                    value =>
                    {
                        text.color = new Color(text.color.r, text.color.g, text.color.b, value);
                    },
                    null,
                    this.inactivityNotificationLerpCurve
                );
            }
        }
    }

    [SerializeField] private TextMeshProUGUI timerText;
    private IEnumerator CountdownUntilExit()
    {
        for (int i = 10; i > 0; i--)
        {
            if (this.PlayerInactive == false)
                break;

            this.timerText.text = i.ToString();
            
            yield return new WaitForSeconds(1);
        }

        if (this.playerInactive) 
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
    public void ReactToInactivityDetected()
    {
        this.PlayerInactive = true;
        StartCoroutine(CountdownUntilExit());
    }

    public void ReactToActivityDetected()
    {
        this.PlayerInactive = false;
    }
}

#endregion
    
}
