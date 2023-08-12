using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPausePopup : UIBasePopup
{
    public override void Init(UIData uiData)
    {

    }

    public override void EndClose()
    {
        base.EndClose();
        GameManager.Instance.TogglePause();
    }

    public void OnEnterTitle()
    {
        SceneLoader.Instance.SwitchScene("TitleScene");
    }
}
