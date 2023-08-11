using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class UICompareAmountText : UIAmountText
{
    [ShowIf("useDynamicColor")]
    public UIColorData colorData;

    [ShowIf("useDynamicColor")]
    public string normalColorKey = "Normal";

    public bool usePercentDisplay = true;
    public bool useDynamicColor = true;
    public bool useRequireAmountDisplay = false;

    [ShowIf("useDynamicColor")]
    public string lackColorKey = "Lack";
    [ShowIf("useDynamicColor")]
    public string equalColorKey = "Equal";
    [ShowIf("useDynamicColor")]
    public string overColorKey = "Over";

    [ShowInInspector]
    [ReadOnly]
    private float requireAmount = 0f;

    public void UpdateAmount(int current, int requireAmount)
    {
        var color = text.color;

        if (useDynamicColor)
        {
            if (current == requireAmount)
            {
                color = colorData.colorDic[equalColorKey];
            }
            else if (current < requireAmount)
            {
                color = colorData.colorDic[lackColorKey];
            }
            else
            {
                color = colorData.colorDic[overColorKey];
            }
        }

        var displayText = "";

        if (usePercentDisplay)
        {
            var percent = (current / (float)requireAmount) * 100f;
            displayText = string.IsNullOrEmpty(viewFormat) ? percent.ToString() : string.Format("{0:" + viewFormat + "}", percent);
        }
        else if (useRequireAmountDisplay)
        {
            displayText = string.IsNullOrEmpty(viewFormat) ? string.Format("{0}/{1}", current, requireAmount)
                : string.Format("{0:" + viewFormat + "}/{1:" + viewFormat + "}", current, requireAmount);
        }
        else
        {
            displayText = string.IsNullOrEmpty(viewFormat) ? current.ToString() : string.Format("{0:" + viewFormat + "}", current);
        }

        text.text = $"{frontAdditionalText}<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{displayText}</color>{backAdditionalText}";
    }

    public void UpdateAmount(float current, float requireAmount)
    {
        var color = text.color;

        if (useDynamicColor)
        {
            if (current == requireAmount)
            {
                color = colorData.colorDic[equalColorKey];
            }
            else if (current < requireAmount)
            {
                color = colorData.colorDic[lackColorKey];
            }
            else
            {
                color = colorData.colorDic[overColorKey];
            }
        }

        var displayText = "";

        if (usePercentDisplay)
        {
            var percent = (current / (float)requireAmount) * 100f;
            displayText = string.IsNullOrEmpty(viewFormat) ? percent.ToString() : string.Format("{0:" + viewFormat + "}", percent);
        }
        else if (useRequireAmountDisplay)
        {
            displayText = string.IsNullOrEmpty(viewFormat) ? string.Format("{0}/{1}", current, requireAmount)
                : string.Format("{0:" + viewFormat + "}/{1:" + viewFormat + "}", current, requireAmount);
        }
        else
        {
            displayText = string.IsNullOrEmpty(viewFormat) ? current.ToString() : string.Format("{0:" + viewFormat + "}", current);
        }

        text.text = $"{frontAdditionalText}<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{displayText}</color>{backAdditionalText}";
    }

}
