using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioMixerEffectElement
{
    [SerializeField]
    private AnimationCurve curve;
    public AnimationCurve Curve { get { return curve; }}

    [SerializeField]
    private float time;
    public float Time { get { return time; }}
}
