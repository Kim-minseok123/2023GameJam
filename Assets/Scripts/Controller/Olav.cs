using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Olav : BaseController
{
    private bool isGrab = false;
    private SnowBlock sn;
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
            if (isGrab) {
                sn.NonGrab(_isRight);
                isGrab = false;
                base.UseSkill();
            }
            else {
                Collider2D[] cd = Physics2D.OverlapBoxAll(transform.position + new Vector3(0.5f, 0, 0), new Vector2(0.5f, 0.1f), 0);

                if (cd.Length == 0) return;
                foreach (Collider2D collider in cd) {
                    if (collider.gameObject.CompareTag("SnowBlock"))
                    {
                        sn = collider.GetComponent<SnowBlock>();
                        sn.GrabBlock(gameObject);
                        isGrab = true;
                        base.UseSkill();
                        return;
                    }
                }
            }
            
        }
        else if(!_isRight){
            if (isGrab) {
                sn.NonGrab(_isRight);
                isGrab = false;
                base.UseSkill();
            }
            else
            {
                Collider2D[] cd = Physics2D.OverlapBoxAll(transform.position + new Vector3(-0.5f, 0, 0), new Vector2(0.5f, 0.1f), 0);
                if (cd.Length == 0) return;
                foreach (Collider2D collider in cd)
                {
                    if (collider.gameObject.CompareTag("SnowBlock"))
                    {
                        sn = collider.GetComponent<SnowBlock>();
                        sn.GrabBlock(gameObject);
                        isGrab = true;
                        base.UseSkill();
                        return;
                    }
                }
            }
        }
    }
}
