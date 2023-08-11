using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "AudioClipContainer", menuName = "Audio/AudioClipContainer", order = 0)]
public class AudioClipContainer : ScriptableObject
{
    [SerializeField]
    private SerializableDictionary<string, AudioClip> clipDic = new SerializableDictionary<string, AudioClip>();

    public SerializableDictionary<string, AudioClip> ClipDic { get { return clipDic; } }

    public AudioClip GetAudioClip(string name)
    {
        if (clipDic.ContainsKey(name))
            return clipDic[name];

        return null;
    }
}
