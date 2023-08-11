using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Olav : BaseController
{
    private bool isGrab = false;
    private SnowBlock sn;
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
            if (isGrab) {
                sn.NonGrab(_isRight);
                isGrab = false;
            }
            else {
                Collider2D cd = Physics2D.OverlapBox(transform.position + new Vector3(1.5f, 0, 0), new Vector2(0.5f, 0.1f), 0);
                if (cd == null) return;
                if (cd.gameObject.CompareTag("SnowBlock"))
                {
                    sn = cd.GetComponent<SnowBlock>();
                    sn.GrabBlock(gameObject);
                    isGrab = true;
                }
            }
            
        }
        else if(!_isRight){
            if (isGrab) {
                sn.NonGrab(_isRight);
                isGrab = false;
            }
            else
            {
                Collider2D cd = Physics2D.OverlapBox(transform.position + new Vector3(-1.5f, 0, 0), new Vector2(0.5f, 0.1f), 0);
                if (cd == null) return;
                if (cd.gameObject.CompareTag("SnowBlock"))
                {
                    sn = cd.GetComponent<SnowBlock>();
                    sn.GrabBlock(gameObject);
                    isGrab = true;
                }
            }
        }
    }
}
