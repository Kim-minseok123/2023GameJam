using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupLoadingView : MonoBehaviour
{

    private void Start()
    {
        FadeController.Instance.ShowLoadingView();
    }

}
