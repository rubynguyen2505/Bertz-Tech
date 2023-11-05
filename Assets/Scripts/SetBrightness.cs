using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.PostProcessing;

public class SetBrightness : MonoBehaviour
{
    public Slider brightnessSlider;
    private UnityEngine.Rendering.VolumeProfile volumeProfile;
    private UnityEngine.Rendering.Universal.Vignette vignette;
    private UnityEngine.Rendering.Universal.ColorAdjustments colorAdjustments;

    // Start is called before the first frame update
    void Start()
    {
        volumeProfile = GetComponent<UnityEngine.Rendering.Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

        // You can leave this variable out of your function, so you can reuse it throughout your class.

        if (!volumeProfile.TryGet(out vignette)) throw new System.NullReferenceException(nameof(vignette));
        if (!volumeProfile.TryGet(out colorAdjustments)) throw new System.NullReferenceException(nameof(colorAdjustments));

        /*
        vignette.intensity.Override(0.1f);
        colorAdjustments.postExposure.Override(20.0f);
        colorAdjustments.contrast.Override(50);
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*
        colorAdjustments.postExposure.Override(20.0f);
        */
    }
}
