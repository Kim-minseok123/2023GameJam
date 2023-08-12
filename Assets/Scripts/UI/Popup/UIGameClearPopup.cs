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
        spendTimeText.UpdateAmount(GameManager.Instance.GetTime());
    }

    public void OnEnterTitle()
    {
        SceneLoader.Instance.SwitchScene("TitleScene");
    }

}
