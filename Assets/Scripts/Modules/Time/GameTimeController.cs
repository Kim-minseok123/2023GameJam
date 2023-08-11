using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameTimeController : Singleton<GameTimeController>
{
    public UnityEvent<float> updateTimeScaleEvent;

    private Coroutine timeScaleCoroutine;

    private const float originTimeScale = 1f;

    public void Pause()
    {
        if (Time.timeScale != originTimeScale)
            return;

        updateTimeScaleEvent?.Invoke(0f);
        Time.timeScale = 0f;
        Time.fixedDeltaTime = Time.fixedUnscaledDeltaTime * 0f;
    }

    public void UnPause()
    {
        if (Time.timeScale != 0f)
            return;

        updateTimeScaleEvent?.Invoke(originTimeScale);
        Time.timeScale = originTimeScale;
        Time.fixedDeltaTime = Time.fixedUnscaledDeltaTime * originTimeScale;
    }

    public void ChangeTimeScale(float timeScale)
    {
        updateTimeScaleEvent?.Invoke(timeScale);
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = Time.fixedUnscaledDeltaTime * timeScale;
    }

    public void ChangeTimeScale(float timeScale, float lifeTime)
    {
        updateTimeScaleEvent?.Invoke(timeScale);
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = Time.fixedUnscaledDeltaTime * timeScale;

        if (timeScaleCoroutine != null)
            StopCoroutine(timeScaleCoroutine);

        timeScaleCoroutine = StartCoroutine(CoTimeScaleAnimation(lifeTime));
    }

    IEnumerator CoTimeScaleAnimation(float lifeTime)
    {
        yield return new WaitForSecondsRealtime(lifeTime);
        ChangeTimeScale(1f);
    }

}
