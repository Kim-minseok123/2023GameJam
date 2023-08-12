using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameClearPopup : UIBasePopup
{
    [SerializeField]
    private UIAmountText spendTimeText;

    public override void Init(UIData uiData)
    {
        //TODO :: 나중에 소요시간 가지고 와서 업데이트 하기
        spendTimeText.UpdateAmount(GameManager.Instance.GetTime());
    }

    public void OnEnterTitle()
    {
        SceneLoader.Instance.SwitchScene("TitleScene");
    }

}
