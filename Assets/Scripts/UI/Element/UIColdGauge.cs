using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColdGauge : UIBaseGauge
{
    [System.Serializable]
    public class RangeData
    {
        public Sprite thermometer;
        public AmountRangeFloat range;
    }


    [SerializeField]
    private RangeData[] ranges;

    [SerializeField]
    private Image thermometerImage;

    private void LateUpdate()
    {
        var current = GameManager.Instance.ColdGauge;
        var max = GameManager.Instance.MaxGauge;

        var progress = (float)current / max;

        UpdateGauge(progress);

        for (var i = 0; i < ranges.Length; ++i)
        {
            var data = ranges[i];
            Debug.Log(progress * 100f);
            if (data.range.Contain(progress * 100f))
            {
                thermometerImage.sprite = data.thermometer;
                break;
            }
        }
    }

}
