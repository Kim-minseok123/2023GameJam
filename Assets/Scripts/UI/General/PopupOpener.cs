using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PopupOpener : MonoBehaviour
{
    public string popupName;

    [Button("Open")]
    public void Open()
    {
        UIController.Instance.OpenPopup(popupName);
    }


}
