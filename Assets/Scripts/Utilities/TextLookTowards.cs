using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using UFO_PlayerStuff;
using UnityEngine.UI;
using System;

public class TextLookTowards : MonoBehaviour
{
    private GameObject playerCam;
    private GameObject parentBone = null;

    private CameraForwardsSampler cameraSampler;

    private bool isPlayerHoldingBone = false;

    public TMP_Text boneText; 

    // Start is called before the first frame update
    void Start()
    {
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");

        cameraSampler = GameObject.FindAnyObjectByType<CameraForwardsSampler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parentBone != null && isPlayerHoldingBone)
        {
            TextLookAt();
            followParent();
        }
    }
    private void followParent()
    {
        string temp = parentBone.GetComponent<boneFacts>().boneFact;
        Debug.Log(temp);
        boneText.text = "" + temp;
        this.transform.position = new Vector3(parentBone.transform.position.x, parentBone.transform.position.y + 0.3f, parentBone.transform.position.z);
    }
    private void TextLookAt()
    {
        this.transform.LookAt(playerCam.transform.position + playerCam.transform.rotation * Vector3.forward, playerCam.transform.rotation * Vector3.up);
    
        Quaternion q = this.transform.rotation;
        this.transform.rotation = Quaternion.Euler(0, q.eulerAngles.y + 180, 0);
    }

    public void OnPickupBone()
    {
        AudioSource aiSources = null;

        tryS
        {
            parentBone = cameraSampler.ObjectInRange;
            Debug.Log(parentBone);
            aiSources = parentBone.GetComponent<boneFacts>().Source;
            Debug.Log(aiSources);
            isPlayerHoldingBone = true;
            Debug.Log("" + parentBone.GetComponent<boneFacts>().isPlayed + " " + isPlayerHoldingBone);
            if (!parentBone.GetComponent<boneFacts>().isPlayed && isPlayerHoldingBone)
            {
                Debug.Log("throught the if, " + parentBone.GetComponent<boneFacts>().isPlayed + " " + isPlayerHoldingBone);
                aiSources.clip = parentBone.GetComponent<boneFacts>().aiVoice;
                aiSources.Play();
                parentBone.GetComponent<boneFacts>().isPlayed = true;
                Debug.Log("Player pickup bone success");
            }
        } catch (NullReferenceException error) { isPlayerHoldingBone = false; }

    }

    public void OnDropBone()
    {
        parentBone = null;
        boneText.text = " ";
    }
}
