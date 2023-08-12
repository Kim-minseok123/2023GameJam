using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UILoadingView : UIBaseView
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private LoadingTipData loadingTipData;

    [SerializeField]
    private UIBaseText loadingText;

    public AmountRangeFloat viewTimeRange;

    private Coroutine viewWaitCoroutine;

    public override void Init(UIData uiData)
    {

    }

    protected override void Start()
    {

    }

    public override void Open()
    {
        gameObject.SetActive(true);
        loadingText.SetText(loadingTipData.GetRandomData());
        StartCoroutine(CoWaitForViewTime());
        animator.SetTrigger("Character_" + Random.Range(0, 5));
    }

    private IEnumerator CoWaitForViewTime()
    {
        var time = viewTimeRange.GetRandomAmount();

        while (time >= 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        SceneLoader.Instance.LoadNextScene();
    }

}
