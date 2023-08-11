using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITargetFollower : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 targetOffset;

    [SerializeField]
    protected bool useLerp = true;
    [SerializeField]
    protected float lerpDamping = 0.5f;

    Camera uiCamera;

    RectTransform rectTransform;
    RectTransform canvasRectTransform;
    Vector2 resultTargetAnchor;

    private void Start()
    {
        Initialize();
    }

    public void SetTarget(Transform target)
    {
        if (rectTransform == null)
        {
            Initialize();
        }

        this.target = target;
        if (target != null)
        {
            CalculateTargetAnchorPosition();
            rectTransform.anchoredPosition = resultTargetAnchor;
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            CalculateTargetAnchorPosition();

            if (useLerp)
            {
                rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, resultTargetAnchor, Time.fixedDeltaTime * lerpDamping);
            }
            else
            {
                rectTransform.anchoredPosition = resultTargetAnchor;
            }
        }
    }

    private void CalculateTargetAnchorPosition()
    {
        var screenPoint = Camera.main.WorldToScreenPoint(target.position + targetOffset);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPoint, uiCamera, out resultTargetAnchor);
    }

    private void Initialize()
    {
        rectTransform = GetComponent<RectTransform>();
        uiCamera = UIController.Instance.uiCamera;
        canvasRectTransform = UIController.Instance.rootCanvas.GetComponent<RectTransform>();
    }

}
