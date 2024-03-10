using System.Collections;
using System.Collections.Generic;
using LerpData;
using TMPro;
using UnityEngine;

public class LerpingUi : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField] private AnimationCurve animCurve;

    private float scaleTarget = 125.0f;
    private bool centerTextLerping = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (this.centerTextLerping)
                return;
            
            
            this.centerTextLerping = true;
            text.fontSize.LerpTo(scaleTarget, 1, (value) =>
            {
                text.fontSize = value;
            }, 
                pkg => 
                {
                    scaleTarget = scaleTarget == 70.0f? 125.0f : 70.0f;
                    this.centerTextLerping = false;
                },
                this.animCurve);
        }
    }
}
