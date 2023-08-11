using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParticleSystemActionCallbackListener : MonoBehaviour
{
    private new ParticleSystem particleSystem;

    private float currentDelayTime = 0f;

    [SerializeField]
    private List<UnityAction> startEventListenerList = new();
    public UnityEvent startEvent;

    [SerializeField]
    private List<UnityAction> endEventListenerList = new();
    public UnityEvent endEvent;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();

        var psMain = particleSystem.main;
        psMain.stopAction = ParticleSystemStopAction.Callback;
    }

    private void Start()
    {
        var psMain = particleSystem.main;
        StartCoroutine(UpdateDelayTime(psMain.startDelay.constant));
    }

    public void OnParticleSystemStopped()
    {
        foreach (var element in endEventListenerList)
        {
            element?.Invoke();
        }
        endEvent?.Invoke();

        endEventListenerList.Clear();
    }

    public void AddStartEventListener(UnityAction listener)
    {
        if (startEventListenerList.Contains(listener))
            return;

        startEventListenerList.Add(listener);
    }

    public void AddEndEventListener(UnityAction listener)
    {
        if (endEventListenerList.Contains(listener))
            return;

        endEventListenerList.Add(listener);
    }

    IEnumerator UpdateDelayTime(float time)
    {
        var waitForSec = new WaitForSeconds(time);
        yield return waitForSec;

        foreach (var element in startEventListenerList)
        {
            element?.Invoke();
        }
        startEvent?.Invoke();

        startEventListenerList.Clear();
    }
}
