using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOpener : MonoBehaviour
{
    public string popupName;

    public void Open()
    {
        UIController.Instance.OpenPopup(popupName);
    }


}
