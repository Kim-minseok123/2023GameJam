using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.OdinInspector;

[System.Serializable]
public class AutoPropBatchElementData
{
    public GameObject prefab;

    [SerializeField]
    private bool useRandomPosition = false;
    public bool UseRandomPosition => useRandomPosition;

    public Vector3 offsetPosition;

    [ShowIf("useRandomPosition")]
    public AmountRangeVector3 offsetRandomPosition;

    [SerializeField]
    private bool useRandomRotation = false;
    public bool UseRandomRotation => useRandomRotation;

    public float offsetRotation;

    [ShowIf("useRandomRotation")]
    public AmountRangeFloat offsetRandomRotation;

    [SerializeField]
    private bool useRandomScale = false;
    public bool UseRandomScale => useRandomScale;

    public float offsetScale;

    [ShowIf("useRandomScale")]
    public AmountRangeFloat offsetRandomScale;

    public float percent = 100f;

    public Vector3 GetPosition() { 
        return useRandomPosition? offsetPosition + offsetRandomPosition.GetRandomAmount() : offsetPosition;
    }

    public Quaternion GetRotation() { 
        return Quaternion.Euler(0,0,useRandomRotation? offsetRotation + offsetRandomRotation.GetRandomAmount() : offsetRotation);
    }

    public Vector3 GetScale() { 
        var scale = Vector3.zero;

        var calculateScale = useRandomScale ? offsetScale + offsetRandomScale.GetRandomAmount() : offsetScale;

        scale.x += calculateScale;
        scale.y += calculateScale;

        return scale;
    }
}

[CreateAssetMenu(fileName = "AutoPropBatchElementData", menuName = "TileMap/AutoPropBatchElementData", order = 0)]
public class AutoPropBatchFolderData : ScriptableObject
{
    [SerializeField]
    private bool useRuleTile = false;

    [HideIf("useRuleTile")]
    [SerializeField]
    private Tile terrainNormalTile;

    [ShowIf("useRuleTile")]
    [SerializeField]
    private RuleTile terrainRuleTile;

    public TileBase TerrainTile => useRuleTile? terrainRuleTile : terrainNormalTile;

    [SerializeField]
    private List<AutoPropBatchElementData> elementDataList;
    public List<AutoPropBatchElementData> ElementDataList => elementDataList;

    public AutoPropBatchElementData GetRandomElement()
    {
        var percentList = new List<float>();

        for (var i = 0; i < elementDataList.Count; ++i)
        {
            percentList.Add(elementDataList[i].percent);
        }

        return RandomUtility.Pickup<AutoPropBatchElementData>(percentList, elementDataList);
    }
}