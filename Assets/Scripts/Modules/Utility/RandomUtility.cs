using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUtility
{
    public static bool CheckSuccess(float percentRange)
    {
        return Random.Range(0f, 100f) <= percentRange;
    }

    public static List<T> Pickup<T>(List<T> datas, int pickupCount)
    {
        var selectList = new List<T>();
        selectList.AddRange(datas);

        var pickupList = new List<T>();

        for (var i = 0; i < pickupCount; ++i)
        {
            var randomIndex = Random.Range(0, selectList.Count);
            pickupList.Add(selectList[randomIndex]);

            selectList.RemoveAt(randomIndex);
        }

        return pickupList;
    }

    public static T Pickup<T>(List<float> percentRangeList, List<T> datas)
    {
        if (percentRangeList.Count != datas.Count)
        {
            Debug.LogError("[크리티컬 이슈] : 뽑기 간 퍼센트 범위와 데이터 범위가 일치하지 않습니다.");
            return default(T);
        }

        var maxWeight = 0f;

        var randomIndexList = new List<int>();
        for (var i = 0; i < percentRangeList.Count; ++i)
        {
            randomIndexList.Add(i);
            maxWeight += percentRangeList[i];
        }

        var randomDataList = new List<T>();

        var rangeList = new List<AmountRangeFloat>();
        var prevRangeMax = 0f;
        while (randomIndexList.Count > 0)
        {
            var randomIndex = Random.Range(0, randomIndexList.Count);
            var index = randomIndexList[randomIndex];
            var range = new AmountRangeFloat(prevRangeMax, prevRangeMax + percentRangeList[index]);
            prevRangeMax = prevRangeMax + percentRangeList[index];

            randomDataList.Add(datas[index]);
            rangeList.Add(range);

            randomIndexList.RemoveAt(randomIndex);
        }

        var currentRandomNumber = Random.Range(0f, maxWeight);
        var pickIndex = rangeList.FindIndex(item => item.Contain(currentRandomNumber));
        return randomDataList[pickIndex];
    }
}
