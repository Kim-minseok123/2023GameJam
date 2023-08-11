using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System.Linq;

public class UITweenAnimator : MonoBehaviour
{
    [System.Serializable]
    public class AnimationGroup
    {
        public List<UIAnimationData> animationList;
    }

    public enum StopActionType
    {
        None,
        Disable,
        Destroy,
    }

    private RectTransform rectTransform;

    private Vector3 originPosition;
    private Quaternion originRotation;
    private Vector3 originScale;
    private Color originColor;
    private float originAlpha;
    private Vector3 originSize;

    public string animatorName = "";

    [SerializeField]
    private SerializableDictionary<string, AnimationGroup> animationTable;

    [SerializeField]
    private UnityEvent completeEvent;
    [SerializeField]
    private UnityEvent stopEvent;

    private List<Tween> playTweenList = new List<Tween>();

    public StopActionType stopActionType;

    [SerializeField]
    private bool autoActiveByPlay = false;
    [SerializeField]
    private bool autoStartPlay = false;

    [SerializeField]
    private bool resetOriginTransformByPlay = false;
    [SerializeField]
    private bool resetOriginAlphaGroupByPlay = false;
    [SerializeField]
    private bool resetOriginColorByPlay = false;
    [SerializeField]
    private bool resetOriginSizeByPlay = false;

    [SerializeField]
    private bool resetOriginTransformByStop = false;
    [SerializeField]
    private bool resetOriginAlphaGroupByStop = false;
    [SerializeField]
    private bool resetOriginColorByStop = false;
    [SerializeField]
    private bool resetOriginSizeByStop = false;

    [SerializeField]
    private bool ignoreTimeScaled = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        if (resetOriginTransformByPlay || resetOriginTransformByStop)
        {
            originPosition = rectTransform.anchoredPosition;
            originRotation = rectTransform.localRotation;
            originScale = rectTransform.localScale;
        }

        if (resetOriginAlphaGroupByPlay || resetOriginAlphaGroupByStop)
        {
            originAlpha = GetComponent<CanvasGroup>().alpha;
        }

        if (resetOriginColorByPlay || resetOriginColorByStop)
        {
            originColor = GetComponent<Image>().color;
        }

        if (resetOriginSizeByPlay || resetOriginSizeByStop)
        {
            originSize = rectTransform.sizeDelta;
        }

    }

    private void Start()
    {
        if (autoStartPlay)
            PlayAnimation();
    }

    [Button("Play")]
    public virtual void PlayAnimation()
    {
        if (animationTable == null || animationTable.Values.Count == 0)
            return;

        var animationList = animationTable.First().Value.animationList;

        PlayAnimation(animationList);
    }

    public virtual void PlayAnimation(List<TweenDynamicParmeter> dynamicParmeters)
    {
        if (animationTable == null || animationTable.Values.Count == 0)
            return;

        var animationList = animationTable.First().Value.animationList;
        PlayAnimation(animationList, tweenDynamicParmeters: dynamicParmeters);
    }

    public virtual void PlayAnimation(UnityAction completeAction)
    {
        if (animationTable == null || animationTable.Values.Count == 0)
            return;

        var animationList = animationTable.First().Value.animationList;
        PlayAnimation(animationList, completeAction: completeAction);
    }

    public virtual void PlayAnimation(List<TweenDynamicParmeter> dynamicParmeters, UnityAction completeAction)
    {
        if (animationTable == null || animationTable.Values.Count == 0)
            return;

        var animationList = animationTable.First().Value.animationList;
        PlayAnimation(animationList, dynamicParmeters, completeAction);
    }

    public virtual void PlayAnimation(string key)
    {
        if (!animationTable.ContainsKey(key))
        {
            return;
        }

        var animationList = animationTable[key].animationList;
        PlayAnimation(animationList);
    }

    public virtual void PlayAnimation(string key, List<TweenDynamicParmeter> dynamicParmeters)
    {
        if (!animationTable.ContainsKey(key))
        {
            return;
        }

        var animationList = animationTable[key].animationList;
        PlayAnimation(animationList, tweenDynamicParmeters: dynamicParmeters);
    }

    public virtual void PlayAnimation(string key, UnityAction completeAction)
    {
        if (!animationTable.ContainsKey(key))
        {
            return;
        }

        var animationList = animationTable[key].animationList;
        PlayAnimation(animationList, completeAction: completeAction);
    }

    public virtual void PlayAnimation(string key, List<TweenDynamicParmeter> dynamicParmeters, UnityAction completeAction)
    {
        if (!animationTable.ContainsKey(key))
        {
            return;
        }

        var animationList = animationTable[key].animationList;
        PlayAnimation(animationList, dynamicParmeters, completeAction);
    }

    public virtual void PlayAnimation(List<UIAnimationData> animations, List<TweenDynamicParmeter> tweenDynamicParmeters = null, UnityAction completeAction = null)
    {
        if (playTweenList.Count > 0)
        {
            Stop();
        }

        if (resetOriginTransformByPlay)
        {
            rectTransform.anchoredPosition = originPosition;
            transform.localRotation = originRotation;
            transform.localScale = originScale;
        }

        if (autoActiveByPlay)
            gameObject.SetActive(true);

        if (resetOriginColorByPlay)
        {
            GetComponent<Graphic>().color = originColor;
        }

        if (resetOriginAlphaGroupByPlay)
        {
            GetComponent<CanvasGroup>().alpha = originAlpha;
        }

        if (resetOriginSizeByPlay) { 
            rectTransform.sizeDelta =  originSize;
        }

        if (animations.Count == 0)
        {
            completeAction?.Invoke();
            completeEvent?.Invoke();
            return;
        }

        for (var i = 0; i < animations.Count; ++i)
        {
            var animationData = animations[i];
            TweenDynamicParmeter dynamicParmeter = null;
            Tween tween = null;

            if (tweenDynamicParmeters != null && i < tweenDynamicParmeters.Count)
            {
                dynamicParmeter = tweenDynamicParmeters[i];
            }

            switch (animationData.AnimationType)
            {
                case UIAnimationType.Move:
                    tween = rectTransform.DOAnchorPos(dynamicParmeter != null ? dynamicParmeter.vectorAmount : animationData.DestinationVector
                        , animationData.Duration);
                    break;
                case UIAnimationType.Rotate:
                    tween = rectTransform.DOLocalRotate(dynamicParmeter != null ? dynamicParmeter.vectorAmount : animationData.DestinationVector
                        , animationData.Duration);
                    break;
                case UIAnimationType.Scale:
                    tween = rectTransform.DOScale(dynamicParmeter != null ? dynamicParmeter.vectorAmount : animationData.DestinationVector
                        , animationData.Duration);
                    break;
                case UIAnimationType.Color:
                    tween = GetComponent<Graphic>()?.DOColor(animationData.DestinationColor, animationData.Duration);
                    break;
                case UIAnimationType.Alpha:
                    tween = GetComponent<CanvasGroup>()?.DOFade(animationData.DestinationFloat, animationData.Duration);
                    break;
                case UIAnimationType.ShakePosition:
                    tween = rectTransform.DOShakeAnchorPos(animationData.Duration
                        , (dynamicParmeter != null ? dynamicParmeter.vectorAmount : animationData.DestinationVector) * animationData.Strength
                        , animationData.Vibrato, animationData.Randomness);
                    break;
                case UIAnimationType.ShakeRotation:
                    tween = rectTransform.DOShakeRotation(animationData.Duration
                        , (dynamicParmeter != null ? dynamicParmeter.vectorAmount : animationData.DestinationVector) * animationData.Strength
                        , animationData.Vibrato, animationData.Randomness);
                    break;
                case UIAnimationType.Size:
                    tween = rectTransform.DOSizeDelta(dynamicParmeter != null ? dynamicParmeter.vectorAmount : animationData.DestinationVector
                        , animationData.Duration);
                    break;
            }

            tween.SetLoops(animationData.LoopCount, animationData.LoopType);

            playTweenList.Add(tween);
            tween.SetDelay(animationData.Delay);
            tween.SetEase(animationData.EaseType);
            tween.OnComplete(() =>
            {
                playTweenList.Remove(tween);
                if (playTweenList.Count <= 0)
                {
                    completeAction?.Invoke();
                    completeEvent?.Invoke();
                    AutoHide();
                }
            });
            tween.SetRelative(animationData.IsRelative);
            tween.SetUpdate(ignoreTimeScaled);
            tween.Play();
        }
    }

    public void Stop()
    {
        for (var i = 0; i < playTweenList.Count; ++i)
        {
            playTweenList[i].Kill();
        }

        playTweenList.Clear();

        if (resetOriginTransformByStop)
        {
            rectTransform.anchoredPosition = originPosition;
            transform.localRotation = originRotation;
            transform.localScale = originScale;
        }

        if (resetOriginColorByStop)
        {
            GetComponent<Graphic>().color = originColor;
        }

        if (resetOriginAlphaGroupByStop)
        {
            GetComponent<CanvasGroup>().alpha = originAlpha;
        }

        if (resetOriginSizeByStop)
        {
            rectTransform.sizeDelta = originSize;
        }
        stopEvent?.Invoke();
    }

    public void AutoHide()
    {
        switch (stopActionType)
        {
            case StopActionType.Disable:
                gameObject.SetActive(false);
                break;
            case StopActionType.Destroy:
                Destroy(gameObject);
                break;
            default: break;
        }
    }

    public bool ContainsKey(string key)
    {
        return animationTable.ContainsKey(key);
    }

}
