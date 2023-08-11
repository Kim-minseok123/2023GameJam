using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;


public class FadeController : Singleton<FadeController>
{
    [System.Serializable]
    public class AnimatorGroup
    {
        public string tweenKey;
        public UITweenAnimator tweenAnimator;

        public void PlayAnimation(UnityAction completeAction)
        {
            if (string.IsNullOrEmpty(tweenKey))
            {
                completeAction?.Invoke();
                return;
            }

            tweenAnimator.PlayAnimation(tweenKey, completeAction);
        }
    }

    [SerializeField]
    private UILoadingView loadingView;

    private CanvasGroup canvasGroup;

    public List<AnimatorGroup> fadeInAnimatorElementList;
    public List<AnimatorGroup> fadeOutAnimatorElementList;

    [SerializeField]
    private int currentAnimationPlayCount = 0;
    private Coroutine animationCoroutine;

    [SerializeField]
    bool isFadeRunning = false;

    protected override void Awake()
    {
        base.Awake();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetActive(bool isActive) { 
        gameObject.SetActive(isActive);
    }

    public void ShowLoadingView()
    {
        loadingView.Open();
    }

    [Button("Fade In")]
    public void FadeIn(UnityAction fadeEndAction = null)
    {
        if(isFadeRunning)
            return;

        canvasGroup.blocksRaycasts = true;
        isFadeRunning = true;

        currentAnimationPlayCount = fadeInAnimatorElementList.Count;

        for (var i = 0; i < fadeInAnimatorElementList.Count; ++i)
        {
            fadeInAnimatorElementList[i].PlayAnimation(() =>
            {
                --currentAnimationPlayCount;
            });
        }

        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(CoWaitCompleteAnimation(fadeEndAction));
    }

    [Button("Fade Out")]
    public void FadeOut(UnityAction fadeEndAction = null)
    {
        if (isFadeRunning)
            return;

        loadingView.Close();
        canvasGroup.blocksRaycasts = true;
        isFadeRunning = true;
        currentAnimationPlayCount = fadeOutAnimatorElementList.Count;

        for (var i = 0; i < fadeOutAnimatorElementList.Count; ++i)
        {
            fadeOutAnimatorElementList[i].PlayAnimation(() =>
            {
                --currentAnimationPlayCount;
            });
        }

        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }

        animationCoroutine = StartCoroutine(CoWaitCompleteAnimation(fadeEndAction));
    }

    private IEnumerator CoWaitCompleteAnimation(UnityAction completeEvent)
    {
        while (currentAnimationPlayCount > 0)
        {
            yield return null;
        }

        canvasGroup.blocksRaycasts = false;
        isFadeRunning = false;
        completeEvent?.Invoke();
        animationCoroutine = null;
    }

    private void OnDisable()
    {
        isFadeRunning = false;
    }
}
