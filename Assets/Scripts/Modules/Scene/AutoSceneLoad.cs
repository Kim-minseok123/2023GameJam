using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSceneLoad : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    [SerializeField]
    private bool useLoading = true;
    [SerializeField]
    private bool useFade = true;

    // Start is called before the first frame update
    void Start()
    {
        if (useLoading)
        {
            SceneLoader.Instance.SwitchScene(sceneName);
        }
        else if (useFade)
        {
            SceneLoader.Instance.SwitchDirectScene(sceneName);
        }
        else
        {
            SceneLoader.Instance.LoadScene(sceneName);
        }
    }

}
