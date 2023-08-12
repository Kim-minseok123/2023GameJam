using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColdScreen : MonoBehaviour
{
    [System.Serializable]
    public class RangeData
    {
        public GameObject icedScreen;
        public AmountRangeFloat range;
    }

    [SerializeField]
    private RangeData[] ranges;

    private int rangeIndex = 0;
    private GameObject currentScreen;

    private void LateUpdate()
    {
        var current = GameManager.Instance.ColdGauge;
        var max = GameManager.Instance.MaxGauge;

        var progress = (float)current / max;

        for (var i = 0; i < ranges.Length; ++i)
        {
            var data = ranges[i];
            if (data.range.Contain(progress * 100f) && i != rangeIndex)
            {
                if (currentScreen != null)
                {
                    currentScreen.SetActive(false);
                }

                rangeIndex = i;

                if (data.icedScreen != null)
                {
                    data.icedScreen.SetActive(true);
                }

                currentScreen = data.icedScreen;

                break;
            }
        }
    }
}
