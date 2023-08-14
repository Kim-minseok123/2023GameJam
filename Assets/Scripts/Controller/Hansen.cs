using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hansen : BaseController
{
    private bool isSkill = false;
    public override void FixedUpdate()
    {
        if (isSkill) return;
        base.FixedUpdate();
    }
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            UseSkill();
        }

        if (isSkill) return;
        base.Update();
    }
    public override void UseSkill()
    {
        base.UseSkill();
        _rb.velocity = Vector2.zero;
        if (!isSkill)
        {
            Camera.main.gameObject.GetComponent<CameraController>().HansenSkillOn();
            isSkill = true;
        }
        else if (isSkill)
        {
            Camera.main.gameObject.GetComponent<CameraController>().HansenSkillOff();
            isSkill = false;
        }
    }

}
