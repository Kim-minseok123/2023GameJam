using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine.UI;

[System.Serializable]
public class TweenDynamicParmeter
{
    public Vector3 vectorAmount;
    public float floatAmount;
}

public class ObjectTweenAnimator : MonoBehaviour
{
    [System.Serializable]
    public class AnimationGroup
    {
        public List<ObjectTweenAnimationData> animationList;
    }

    public enum StopActionType
    {
        None,
        Disable,
        Destroy,
    }

    private Vector3 originPosition;
    private Quaternion originRotation;
    private Vector3 originScale;
    private Color originColor;
    private float originAlpha;

    public string animatorName = "";

    [SerializeField]
    private SerializableDictionary<string, AnimationGroup> animationTable;

    private new Renderer renderer;

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
    private bool resetOriginColorByPlay = false;

    [SerializeField]
    private bool resetOriginTransformByStop = false;
    [SerializeField]
    private bool resetOriginColorByStop = false;

    [SerializeField]
    private bool ignoreTimeScaled = false;

    private void Awake()
    {
        if (resetOriginTransformByPlay || resetOriginTransformByStop)
        {
            originPosition = transform.localPosition;
            originRotation = transform.localRotation;
            originScale = transform.localScale;
        }

        if (resetOriginColorByPlay || resetOriginColorByStop)
        {
            originColor = GetComponent<SpriteRenderer>().color;
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


    public virtual void PlayAnimation(List<ObjectTweenAnimationData> animations, List<TweenDynamicParmeter> tweenDynamicParmeters = null, UnityAction completeAction = null)
    {
        if (playTweenList.Count > 0)
        {
            Stop();
        }

        if (resetOriginTransformByStop)
        {
            originPosition = transform.localPosition;
            originRotation = transform.localRotation;
            originScale = transform.localScale;
        }

        if (autoActiveByPlay)
            gameObject.SetActive(true);

        if (resetOriginColorByPlay)
        {
            GetComponent<SpriteRenderer>().color = originColor;
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
                case ObjectTweenAnimationType.Move:
                    tween = transform.DOLocalMove(dynamicParmeter != null ? dynamicParmeter.vectorAmount : animationData.DestinationVector
                        , animationData.Duration);
                    break;
                case ObjectTweenAnimationType.Rotate:
                    tween = transform.DOLocalRotate(dynamicParmeter != null ? dynamicParmeter.vectorAmount : animationData.DestinationVector
                        , animationData.Duration);
                    break;
                case ObjectTweenAnimationType.Scale:
                    tween = transform.DOScale(dynamicParmeter != null ? dynamicParmeter.vectorAmount : animationData.DestinationVector
                        , animationData.Duration);
                    break;
                    case ObjectTweenAnimationType.Color:
                        tween = GetComponent<SpriteRenderer>()?.DOColor(animationData.DestinationColor, animationData.Duration);
                    break;
                case ObjectTweenAnimationType.ShakePosition:
                    tween = transform.DOShakePosition(animationData.Duration
                        , (dynamicParmeter != null ? dynamicParmeter.vectorAmount : animationData.DestinationVector) * animationData.Strength
                        , animationData.Vibrato, animationData.Randomness);
                    break;
                case ObjectTweenAnimationType.ShakeRotation:
                    tween = transform.DOShakeRotation(animationData.Duration
                        , (dynamicParmeter != null ? dynamicParmeter.vectorAmount : animationData.DestinationVector) * animationData.Strength
                        , animationData.Vibrato, animationData.Randomness);
                    break;
                case ObjectTweenAnimationType.CameraFov:
                    tween = Camera.main.DOFieldOfView(animationData.DestinationFloat, animationData.Duration);
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
            transform.localPosition = originPosition;
            transform.localRotation = originRotation;
            transform.localScale = originScale;
        }

        if (resetOriginColorByStop)
        {
            GetComponent<SpriteRenderer>().color = originColor;
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

}
