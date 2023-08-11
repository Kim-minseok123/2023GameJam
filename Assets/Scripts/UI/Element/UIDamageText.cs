using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDamageText : MonoBehaviour
{
    private Camera mainCamera;
    private Camera uiCamera;

    RectTransform rectTransform;

    private Vector3 hitPoint;
    [SerializeField]
    private Vector3 offset;

    private RectTransform worldRectGroup;
    private Vector2 canvasRectPoint;

    [SerializeField]
    private UIAmountText amountText;

    public void SetDamageAmount(Vector3 hitPoint, float damage)
    {
        mainCamera = Camera.main;
        uiCamera = UIController.Instance.uiCamera;
        worldRectGroup = UIController.Instance.worldGroup;
        rectTransform = GetComponent<RectTransform>();

        this.hitPoint = hitPoint + offset;

        var screenPoint = Camera.main.WorldToScreenPoint(hitPoint + offset);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(worldRectGroup, screenPoint, uiCamera, out canvasRectPoint);
        rectTransform.anchoredPosition = canvasRectPoint;

        amountText.UpdateAmount(damage);
    }

    public void AutoDestroy()
    {
        Destroy(gameObject);
    }

}
