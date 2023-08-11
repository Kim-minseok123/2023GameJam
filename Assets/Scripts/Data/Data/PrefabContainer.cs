using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "PrefabContainer", menuName = "GameObject/PrefabContainer", order = 0)]
public class PrefabContainer : ScriptableObject
{
    [SerializeField]
    private SerializableDictionary<string, GameObject> prefabDic = new SerializableDictionary<string, GameObject>();

    public SerializableDictionary<string, GameObject> PrefabDic { get { return prefabDic; } }

    public GameObject GetVFXPrefab(string name)
    {
        if (prefabDic.ContainsKey(name))
            return prefabDic[name];

        return null;
    }

}
