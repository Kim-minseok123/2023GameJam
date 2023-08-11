using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amundsen : BaseController
{
    public override void ChangeCharacter()
    {
        base.ChangeCharacter();
        _coldGaugeReduced = 2;
    }
}
