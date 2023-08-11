using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIAmountText : UIBaseText
{
    public bool useRangeSymbol = false;
    public bool hideZeroNumber = false;

    public UnityEvent updateAmountEvent;

    public virtual void UpdateAmount(int amount)
    {
        AutoCaching();

        if(hideZeroNumber && amount == 0)
        {
            text.text = "";
            updateAmountEvent?.Invoke();
            return;
        }

        string amountString = string.IsNullOrEmpty(viewFormat) ? amount.ToString() : string.Format("{0:" + viewFormat + "}", amount);

        if (useRangeSymbol && amount > 0) {
            amountString = string.Format($"+{amountString}");
        }

        text.text = $"{frontAdditionalText}{amountString}{backAdditionalText}";
        updateAmountEvent?.Invoke();
    }

    public virtual void UpdateAmount(float amount)
    {
        AutoCaching();

        if (hideZeroNumber && amount == 0)
        {
            text.text = "";
            updateAmountEvent?.Invoke();
            return;
        }

        string amountString = string.IsNullOrEmpty(viewFormat) ? amount.ToString() : string.Format("{0:" + viewFormat + "}", amount);

        if (useRangeSymbol && amount > 0)
        {
            amountString = string.Format($"+{amountString}");
        }

        text.text = $"{frontAdditionalText}{amountString}{backAdditionalText}";
        updateAmountEvent?.Invoke();
    }

}
