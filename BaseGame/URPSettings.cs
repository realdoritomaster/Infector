using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class URPSettings : MonoBehaviour
{
    public Slider RenderScaleSlider;
    public Toggle toggle;
    public UniversalRenderPipelineAsset URP;

    public float DefaultRenderScale;
    public bool DefaultHDR;

    void Start()
    {
        RenderScaleSlider.value = URP.renderScale;
        toggle.isOn = URP.supportsHDR;
    }

    public void URPSetting(string s)
    {
        
        switch (s)
        {
            case "RenderScale":
                URP.renderScale = RenderScaleSlider.value;
                break;
            case "HDRenable":
                URP.supportsHDR = toggle.isOn;
                URP.colorGradingMode = toggle.isOn ? ColorGradingMode.HighDynamicRange : ColorGradingMode.LowDynamicRange;
                break;
            case "SetDefault":
                URP.renderScale = DefaultRenderScale;
                RenderScaleSlider.value = DefaultRenderScale;
                toggle.isOn = DefaultHDR;
                URP.supportsHDR = toggle.isOn;
                URP.colorGradingMode = toggle.isOn ? ColorGradingMode.HighDynamicRange : ColorGradingMode.LowDynamicRange;
                break;
        }

        
    }
}
