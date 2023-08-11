using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutoFadeOut : MonoBehaviour
{
    public UnityEvent endFadeEvent;

    private void Start()
    {
        StartCoroutine(WaitForEndFrame());
    }

    IEnumerator WaitForEndFrame()
    {
        yield return new WaitForEndOfFrame();

        FadeController.Instance.FadeOut(() =>
        {
            endFadeEvent?.Invoke();
        });
    }
}
