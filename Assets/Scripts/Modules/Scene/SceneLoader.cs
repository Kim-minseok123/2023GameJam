using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField]
    private string loadingSceneName;
    [SerializeField]
    private string nextSceneName;

    public void SwitchScene(string nextScene, bool useFade = true)
    {
        nextSceneName = nextScene;
        if (useFade)
        {
            FadeController.Instance.FadeIn(() =>
            {
                LoadScene(loadingSceneName);
            });
        }
        else
        {
            LoadScene(loadingSceneName);
        }

    }

    public void SwitchDirectScene(string nextScene, bool useFade = true)
    {
        if (useFade)
        {
            FadeController.Instance.FadeIn(() =>
            {
                LoadScene(nextScene);
            });
        }
        else
        {
            LoadScene(nextScene);
        }
    }

    public void LoadNextScene(UnityAction<float> updateLoadingAction = null, UnityAction endAction = null)
    {
        LoadSceneAsync(nextSceneName, LoadSceneMode.Single, updateLoadingAction: updateLoadingAction, endAction: endAction);
        nextSceneName = "";
    }

    public void LoadScene(string sceneName)
    {
        LoadSceneAsync(sceneName);
    }

    public void LoadScene(string sceneName, LoadSceneMode loadMode = LoadSceneMode.Single, UnityAction endAction = null)
    {
        SceneManager.LoadScene(sceneName, loadMode);
        endAction?.Invoke();
    }

    public async Task LoadSceneAsync(string sceneName, LoadSceneMode loadMode = LoadSceneMode.Single, bool allowActive = true, UnityAction<float> updateLoadingAction = null, UnityAction endAction = null)
    {
        var asyncOper = SceneManager.LoadSceneAsync(sceneName, loadMode);
        updateLoadingAction?.Invoke(0f);
        asyncOper.allowSceneActivation = allowActive;
        do
        {
            updateLoadingAction?.Invoke(asyncOper.progress);
            await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);
        } while (!asyncOper.isDone);

        updateLoadingAction?.Invoke(1f);
        endAction?.Invoke();
    }


}
