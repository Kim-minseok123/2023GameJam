using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverPopup : UIBasePopup
{
    public override void Init(UIData uiData)
    {

    }

    public void OnEnterTitle()
    {
        SceneLoader.Instance.SwitchScene("TitleScene");
    }
}
