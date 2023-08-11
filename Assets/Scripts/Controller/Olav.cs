using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Olav : BaseController
{
    //올라브는 눈덩이를 움직일 수 있다.
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
       
        // 눈덩이 블럭 체크
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
