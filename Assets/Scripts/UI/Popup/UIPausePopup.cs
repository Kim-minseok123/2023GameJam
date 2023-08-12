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
        GameManager.Instance.isEsc = false;
        Time.timeScale = 1f;
    }

    public void OnEnterTitle()
    {
        Time.timeScale = 1f;
        Destroy(GameManager.Instance.gameObject);
        SceneLoader.Instance.SwitchScene("TitleScene");
    }
}
