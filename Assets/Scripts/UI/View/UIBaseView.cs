using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIBaseView : MonoBehaviour
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

    public string viewName = "";

    public List<AnimatorGroup> openAnimatorElementList;
    public List<AnimatorGroup> closeAnimatorElementList;

    private List<Tween> playTweenList = new List<Tween>();
    private int playAnimationCount = 0;

    public UnityEvent beginOpenEvent;
    public UnityEvent endOpenEvent;
    public UnityEvent beginCloseEvent;
    public UnityEvent endCloseEvent;

    protected virtual void Start()
    {
        UIController.Instance.OpenView(this);
    }

    public abstract void Init(UIData uiData);

    [Button("Open")]
    public virtual void Open()
    {
        BeginOpen();
    }

    public virtual void BeginOpen()
    {
        gameObject.SetActive(true);
        beginOpenEvent?.Invoke();
        PlayAnimation(openAnimatorElementList, EndOpen);
    }

    public virtual void EndOpen()
    {
        endOpenEvent?.Invoke();
    }

    [Button("Close")]
    public virtual void Close()
    {
        BeginClose();
    }

    public virtual void BeginClose()
    {
        beginCloseEvent?.Invoke();
        PlayAnimation(closeAnimatorElementList, EndClose);
    }

    public virtual void EndClose()
    {
        endCloseEvent?.Invoke();
        gameObject.SetActive(false);
    }

    public virtual void PlayAnimation(List<AnimatorGroup> animators, UnityAction completeAction = null)
    {
        if (playTweenList != null || playTweenList.Count > 0)
        {
            Stop();
        }

        if (animators == null || animators.Count == 0)
        {
            completeAction?.Invoke();
            return;
        }

        for (var i = 0; i < animators.Count; ++i)
        {
            ++playAnimationCount;
            animators[i].PlayAnimation(() =>
            {
                CheckCompleteAnimation(completeAction);
            });
        }
    }

    public void CheckCompleteAnimation(UnityAction completeAction)
    {
        --playAnimationCount;
        if (playAnimationCount <= 0)
        {
            completeAction?.Invoke();
        }
    }

    public void Stop()
    {
        for (var i = 0; i < playTweenList.Count; ++i)
        {
            playTweenList[i].Kill();
        }

        playTweenList.Clear();
        playAnimationCount = 0;
    }

    [Button("OpenAnimator로 자동 등록")]
    private void AutoSetupOpenAnimator(string key)
    {
        openAnimatorElementList.Clear();

        var animators = GetComponentsInChildren<UITweenAnimator>(true);

        foreach (var animator in animators)
        {
            if (animator.ContainsKey(key))
            {
                openAnimatorElementList.Add(new AnimatorGroup()
                {
                    tweenKey = key,
                    tweenAnimator = animator
                });
            }
        }
    }

    [Button("CloseAnimator로 자동 등록")]
    private void AutoSetupCloseAnimator(string key)
    {
        closeAnimatorElementList.Clear();

        var animators = GetComponentsInChildren<UITweenAnimator>(true);

        foreach (var animator in animators)
        {
            if (animator.ContainsKey(key))
            {
                closeAnimatorElementList.Add(new AnimatorGroup()
                {
                    tweenKey = key,
                    tweenAnimator = animator
                });
            }
        }
    }

}
