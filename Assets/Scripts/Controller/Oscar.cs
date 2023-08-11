using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscar : BaseController
{
    //����ī�� ���� ȸ�� ������� ü�� ȸ�� Ƚ�� ����
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
        // ȸ�� ��ġ Ƚ�� Ȯ��
        if (GameManager.Instance.StoveNumber > 0) { 
            GameManager.Instance.StoveNumber--;
            // �ɷ� ���
            var Stove = Resources.Load("Prefabs/Skill/Stove");
            if (Stove != null)
            {
                Vector3 ts = gameObject.transform.position;
                Instantiate(Stove, ts, Quaternion.identity);
            }
        }
    }
}