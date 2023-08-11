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
        if (_isRight) {
            Collider2D cd = Physics2D.OverlapBox(transform.position + new Vector3(2f,0,0),new Vector2(1,0.1f),0);
            if (cd == null) return;
            if (cd.gameObject.CompareTag("SnowBlock")) {

            }
        }
        else if(!_isRight){
            Collider2D cd = Physics2D.OverlapBox(transform.position + new Vector3(-2f, 0, 0), new Vector2(1, 0.1f), 0);
            if (cd == null) return;
            if (cd.gameObject.CompareTag("SnowBlock"))
            {

            }
        }
    }
}
