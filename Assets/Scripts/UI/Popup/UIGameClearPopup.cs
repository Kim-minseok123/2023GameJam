using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameClearPopup : UIBasePopup
{
    [SerializeField]
    private UIAmountText spendTimeText;

    public override void Init(UIData uiData)
    {
        //TODO :: ���߿� �ҿ�ð� ������ �ͼ� ������Ʈ �ϱ�
        spendTimeText.UpdateAmount(0);
    }

    public void OnEnterTitle()
    {
        SceneLoader.Instance.SwitchScene("TitleScene");
    }

}
