using System.Collections;
using System.Collections.Generic;
using LerpData;
using TMPro;
using UnityEngine;

public class LerpingUi : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

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
            ObjectLerpPackage<TextMeshProUGUI> pkg = new FloatLerpPackage<TextMeshProUGUI>(
                text.fontSize, scaleTarget,
                ((value, component) =>
                {
                    component.fontSize = value;
                }),
                pkg =>
                {
                    scaleTarget = scaleTarget == 70.0f ? 125.0f : 70.0f;
                    this.centerTextLerping = false;
                },
                text.gameObject
                );
            GlobalProcessorHandler.reference.AddPackage(pkg);
        }
    }
}
