using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ParticleSystemCallbackListener : MonoBehaviour
{
    private new ParticleSystem particleSystem;

    private float currentDelayTime = 0f;

    [SerializeField]
    private string eventName;
    public string EventName { get { return eventName; } }

    [SerializeField]
    private List<GameObject> startEventListenerList;

    [SerializeField]
    private List<GameObject> endEventListenerList;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();

        var psMain = particleSystem.main;
        psMain.stopAction = ParticleSystemStopAction.Callback;
    }

    private void Start()
    {
        if (string.IsNullOrEmpty(eventName))
            return;

        var psMain = particleSystem.main;
        StartCoroutine(UpdateDelayTime(psMain.startDelay.constant));
    }

    public void OnParticleSystemStopped()
    {
        if (string.IsNullOrEmpty(eventName))
            return;

        foreach (var element in endEventListenerList)
        {
            element.SendMessage(eventName);
        }
    }

    public void AddStartEventListener(GameObject listener)
    {
        if (startEventListenerList.Contains(listener))
            return;

        startEventListenerList.Add(listener);
    }

    public void AddEndEventListener(GameObject listener)
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
            element.SendMessage(eventName);
        }
    }

}
