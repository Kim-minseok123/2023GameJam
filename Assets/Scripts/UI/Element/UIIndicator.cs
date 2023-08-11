using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIndicator : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 targetOffset;

    [SerializeField]
    protected bool useLerp = true;
    [SerializeField]
    protected float lerpDamping = 0.5f;

    private Camera uiCamera;

    private RectTransform rectTransform;
    private RectTransform canvasRectTransform;

    private Vector2 resultTargetAnchor;
    private Vector2 clampTargetAnchor;
    private Vector2 indicatorHalfArea;

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
                clampTargetAnchor = Vector3.Lerp(rectTransform.anchoredPosition, resultTargetAnchor, Time.fixedDeltaTime * lerpDamping);
            }
            else
            {
                clampTargetAnchor = resultTargetAnchor;
            }


            var isClamp = false;
            if (Mathf.Abs(clampTargetAnchor.x) > indicatorHalfArea.x)
            {
                isClamp = true;
                clampTargetAnchor.x = Mathf.Clamp(clampTargetAnchor.x, -indicatorHalfArea.x, indicatorHalfArea.x);
            }

            if (Mathf.Abs(clampTargetAnchor.y) > indicatorHalfArea.y)
            {
                isClamp = true;
                clampTargetAnchor.y = Mathf.Clamp(clampTargetAnchor.y, -indicatorHalfArea.y, indicatorHalfArea.y);
            }

            rectTransform.anchoredPosition = clampTargetAnchor;

            if (isClamp)
            {
                var direction = (target.position - Camera.main.transform.position).normalized;
                direction.z = 0f;

                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                var axis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
                rectTransform.localRotation = axis;
                canvasGroup.alpha = 1f;
            }
            else
            {
                rectTransform.localRotation = Quaternion.Euler(0, 0, 0);
                canvasGroup.alpha = 0f;
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
        canvasGroup = GetComponent<CanvasGroup>();
        uiCamera = UIController.Instance.uiCamera;
        canvasRectTransform = UIController.Instance.rootCanvas.GetComponent<RectTransform>();
        indicatorHalfArea = canvasRectTransform.sizeDelta * 0.5f - rectTransform.sizeDelta * 0.5f;
    }

}
