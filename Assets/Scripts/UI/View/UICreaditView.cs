using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UICreaditView : MonoBehaviour
{
    [SerializeField]
    private string nextScene;

    private float currentShowTime = 0f;
    public float showTime = 5f;

    [SerializeField]
    private float fastTimeAmount = 5f;

    [SerializeField]
    private RectTransform creaditBound;

    public UnityEvent startCreaditEvent;
    public UnityEvent endCreaditEvent;

    private Vector3 startPoint;
    private Vector3 endPoint;

    private bool isUpdate = false;

    private void Start()
    {
        FadeController.Instance.SetActive(false);
        Canvas.ForceUpdateCanvases();

        startPoint = creaditBound.anchoredPosition;
        endPoint = startPoint + Vector3.up * (creaditBound.rect.height + Screen.height);
        startCreaditEvent?.Invoke();
        currentShowTime = 0f;
        isUpdate = true;
    }


    private void Update()
    {
        if (!isUpdate)
            return;

        // 아무 키나 마우스 버튼이 눌렸을 때 크레딧을 빠르게 내리도록 조절합니다.
        if (Input.anyKey)
        {
            currentShowTime += Time.deltaTime * fastTimeAmount; // 이 값을 높일수록 크레딧이 더 빨리 내려갑니다.
        }
        else
        {
            currentShowTime += Time.deltaTime;
        }

        creaditBound.anchoredPosition = Vector3.Lerp(startPoint, endPoint, currentShowTime / showTime);

        if (currentShowTime >= showTime)
        {
            isUpdate = false;
            endCreaditEvent?.Invoke();
            FadeController.Instance.SetActive(true);
            SceneLoader.Instance.SwitchDirectScene(nextScene);
        }
    }


}
