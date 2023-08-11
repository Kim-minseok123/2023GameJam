using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICheatModePopup : UIBasePopup
{
    [SerializeField]
    private TMP_InputField inputField;

    public override void Init(UIData uiData)
    {

    }

    public void ChangeScene() { 
        if(string.IsNullOrEmpty(inputField.text))
            return;

        SceneLoader.Instance.LoadScene(inputField.text);
    }

}
