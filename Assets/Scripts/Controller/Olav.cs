using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Olav : BaseController
{
    //�ö��� �����̸� ������ �� �ִ�.
    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space)) {
            UseSkill();
        }

    }
    public override void ChangeCharacter()
    {
        base.ChangeCharacter();
        
    }
    public override void UseSkill()
    {
        // ������ �� üũ
    }
}
