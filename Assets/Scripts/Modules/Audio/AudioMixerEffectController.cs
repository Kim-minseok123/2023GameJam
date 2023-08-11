using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerEffectController : MonoBehaviour
{
    private float currentTime;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private AudioMixerSnapshot originalSnapshot;
    [SerializeField]
    private float originalSnapshotTransitionTime;

    [SerializeField]
    private AudioMixerSnapshot effectSnapshot;
    [SerializeField]
    private float effectSnapshotTransitionTime;

    [SerializeField]
    private SerializableDictionary<string, AudioMixerEffectElement> effectParmeterTable;

    private float maxTime = float.MinValue;

    private void Start()
    {
        foreach (var key in effectParmeterTable.Keys)
        {
            var element = effectParmeterTable[key];

            if (maxTime < element.Time)
                maxTime = element.Time;
        }

        if (originalSnapshot != null && effectSnapshot != null)
        {
            effectSnapshot.TransitionTo(effectSnapshotTransitionTime);
        }
    }

    private void Update()
    {
        currentTime += Time.unscaledDeltaTime;
        foreach (var key in effectParmeterTable.Keys)
        {
            var element = effectParmeterTable[key];

            audioMixer.SetFloat(key, element.Curve.Evaluate(currentTime / element.Time));
        }

        if (maxTime <= currentTime)
        {
            if (originalSnapshot != null && effectSnapshot != null)
            {
                originalSnapshot.TransitionTo(originalSnapshotTransitionTime);
            }
            Destroy(this.gameObject);
        }
    }

}
