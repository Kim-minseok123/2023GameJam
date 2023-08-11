using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatusGauge : UIBaseGauge
{
    [SerializeField]
    private StatusType currentType;
    private StatusElement currentElement;
    [SerializeField]
    private StatusType maxType;
    private StatusElement maxElement;

    private StatusInfo statusInfo;

    public virtual void UpdateStatusInfo(EntityStatus entityStatus)
    {
        UpdateStatusInfo(entityStatus.currentStatus);
    }

    public virtual void UpdateStatusInfo(StatusInfo statusInfo)
    {
        this.statusInfo = statusInfo;

        currentElement = statusInfo.GetElement(currentType);
        currentElement.updateCalculateAction += UpdateStatusElement;
        var currentAmount = currentElement.CalculateTotalAmount();

        maxElement = statusInfo.GetElement(maxType);
        maxElement.updateCalculateAction += UpdateStatusElement;
        var maxAmount = maxElement.CalculateTotalAmount();

        UpdateGauge(currentAmount, maxAmount);
    }

    public virtual void UpdateStatusElement(float amount)
    {
        var currentAmount = currentElement.CalculateTotalAmount();
        var maxAmount = maxElement.CalculateTotalAmount();

        UpdateGauge(currentAmount, maxAmount);
    }

    protected virtual void OnDestroy()
    {
        currentElement.updateCalculateAction -= UpdateStatusElement;
        maxElement.updateCalculateAction -= UpdateStatusElement;
    }

}
