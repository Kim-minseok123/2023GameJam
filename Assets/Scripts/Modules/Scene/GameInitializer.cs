using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Cysharp.Threading.Tasks;

public class GameInitializer : MonoBehaviour
{
    public string nextScene;

    private void Start()
    {
        Application.targetFrameRate = 60;        
        SceneLoader.Instance.SwitchScene(nextScene, false);
    }

}
