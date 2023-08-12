using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hansen : BaseController
{
    public override void ChangeCharacter()
    {
        StartCoroutine(Camera.main.GetComponent<CameraController>().ZoonIn(10f));
        GameManager.Instance.SetPlayer(this.gameObject, _coldGaugeReduced);
        Effect.SetTrigger("Effect");
    }
    
}
