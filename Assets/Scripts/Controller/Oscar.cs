using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscar : BaseController
{
    //오스카는 간이 회로 사용으로 체력 회복 횟수 제한
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseSkill();
        }
    }
    public override void ChangeCharacter()
    {
        base.ChangeCharacter();
    }
    public override void UseSkill()
    {
        base.UseSkill();
        // 회로 설치 횟수 확인
        if (GameManager.Instance.StoveNumber > 0) { 
            GameManager.Instance.StoveNumber--;
            // 능력 사용
            var Stove = Resources.Load("Prefabs/Skill/Stove");
            if (Stove != null)
            {
                Vector3 ts = gameObject.transform.position;
                Instantiate(Stove, ts + new Vector3(0,0f,0), Quaternion.identity);
            }
        }
    }
}
