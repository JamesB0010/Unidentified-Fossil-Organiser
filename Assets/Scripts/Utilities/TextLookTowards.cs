using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using UFO_PlayerStuff;
using UnityEngine.UI;

public class TextLookTowards : MonoBehaviour
{
    private GameObject playerCam;
    private GameObject parentBone = null;

    private CameraForwardsSampler cameraSampler;

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

        if (parentBone != null)
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

        parentBone = cameraSampler.ObjectInRange;
    }

    public void OnDropBone()
    {
        parentBone = null;
        boneText.text = " ";
    }
}
