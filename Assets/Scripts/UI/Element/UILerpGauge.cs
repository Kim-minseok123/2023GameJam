using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILerpGauge : UIBaseGauge
{
    [SerializeField]
    protected float prevProgress;
    protected float targetProgress;

    [SerializeField]
    protected float lerpTime;

    private Coroutine updateProgressRoutine = null;

    public override void UpdateGauge(float progress)
    {
        if (Mathf.Abs(progress - targetProgress) < 0.05f)
            return;

        targetProgress = progress;

        if (updateProgressRoutine != null)
        {
            StopCoroutine(updateProgressRoutine);
        }

        updateProgressRoutine = StartCoroutine(UpdateLerpProgress());
    }

    public override void UpdateGauge(float current, float max)
    {
        var progress = current / max;
        if (Mathf.Abs(progress - targetProgress) < 0.05f)
            return;

        targetProgress = progress;

        if (updateProgressRoutine != null)
        {
            StopCoroutine(updateProgressRoutine);
        }

        updateProgressRoutine = StartCoroutine(UpdateLerpProgress());
    }

    IEnumerator UpdateLerpProgress()
    {
        var time = 0f;
        var progress = prevProgress;

        while (time <= lerpTime)
        {
            prevProgress = Mathf.Lerp(progress, targetProgress, time / lerpTime);
            base.UpdateGauge(prevProgress);
            time += Time.deltaTime;
            yield return null;
        }

        prevProgress = targetProgress;
        base.UpdateGauge(prevProgress);
    }
}
