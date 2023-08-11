using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[System.Serializable]
public class StatusElement
{
    public StatusType type;
    public string name;
    [SerializeField]
    protected float amount;
    [SerializeField]
    protected float percent;

    public UnityAction<float> updateAmountAction;
    public UnityAction<float> updatePercentAction;
    public UnityAction<float> updateCalculateAction;

    public StatusElement()
    {

    }

    public StatusElement(StatusType type, string name, float amount, float percent)
    {
        this.type = type;
        this.name = name;
        this.amount = amount;
        this.percent = percent;
    }
    public StatusElement(StatusElement copyElement)
    {
        this.type = copyElement.type;
        this.name = copyElement.name;
        this.amount = copyElement.amount;
        this.percent = copyElement.percent;
    }

    public virtual float CalculateTotalAmount() { 
        return amount + (amount * percent  * 0.01f);
    }

    public virtual float CalculateTotalAmount(float origin)
    {
        return origin + amount + (origin * percent * 0.01f);
    }

    public virtual float CalculateAmount(float origin)
    {
        return origin + amount;
    }

    public virtual float GetPercentAmount(float origin)
    {
        return origin * percent;
    }

    public virtual float CalculatePercent(float origin)
    {
        return origin + (origin * percent * 0.01f);
    }

    public void SetAmount(float amount)
    {
        this.amount = amount;
        updateAmountAction?.Invoke(this.amount);
        updateCalculateAction?.Invoke(this.CalculateTotalAmount());
    }

    public void AddAmount(float amount) {
        this.amount += amount;
        this.amount = Mathf.Max(0, this.amount);
        updateAmountAction?.Invoke(this.amount);
        updateCalculateAction?.Invoke(this.CalculateTotalAmount());
    }

    public void SubAmount(float amount)
    {
        this.amount -= amount;
        this.amount = Mathf.Max(0, this.amount);
        updateAmountAction?.Invoke(this.amount);
        updateCalculateAction?.Invoke(this.CalculateTotalAmount());
    }

    public void MultiplyAmount(float amount)
    {
        this.amount *= amount;
        this.amount = Mathf.Max(0, this.amount);
        updateAmountAction?.Invoke(this.amount);
        updateCalculateAction?.Invoke(this.CalculateTotalAmount());
    }
    public void DivideAmount(float amount)
    {
        this.amount /= amount;
        this.amount = Mathf.Max(0, this.amount);
        updateAmountAction?.Invoke(this.amount);
        updateCalculateAction?.Invoke(this.CalculateTotalAmount());
    }

    public float GetAmount() { 
        return amount;
    }

    public void SetPercent(float percent)
    {
        this.percent = percent;
        updatePercentAction?.Invoke(this.percent);
        updateCalculateAction?.Invoke(this.CalculateTotalAmount());
    }

    public void AddPercent(float percent)
    {
        this.percent += percent;
        this.percent = Mathf.Max(0, this.percent);
        updatePercentAction?.Invoke(this.percent);
        updateCalculateAction?.Invoke(this.CalculateTotalAmount());
    }
    public void SubPercent(float percent)
    {
        this.percent -= percent;
        this.percent = Mathf.Max(0, this.percent);
        updatePercentAction?.Invoke(this.percent);
        updateCalculateAction?.Invoke(this.CalculateTotalAmount());
    }
    public void MultiplyPercent(float percent)
    {
        this.percent *= percent;
        this.percent = Mathf.Max(0, this.percent);
        updatePercentAction?.Invoke(this.percent);
        updateCalculateAction?.Invoke(this.CalculateTotalAmount());
    }
    public void DividePercent(float percent)
    {
        this.percent /= percent;
        this.percent = Mathf.Max(0, this.percent);
        updatePercentAction?.Invoke(this.percent);
        updateCalculateAction?.Invoke(this.CalculateTotalAmount());
    }

    public float GetPercent()
    {
        return percent;
    }
}