using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

[System.Serializable]
public class PlayRecordData
{
    public float masterVolume = 1f;
    public float bgmVolume = 1f;
    public float sfxVolume = 1f;

    public Vector2Int screenResolution = new Vector2Int(1920, 1080);
    public FullScreenMode screenMode = FullScreenMode.FullScreenWindow;

}
