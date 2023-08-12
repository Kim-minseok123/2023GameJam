using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmPlayerEvent : MonoBehaviour
{
    public string NextScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { 
            // �ʸ� ���� ���.
            // 3�ʰ� �����ְ� �ڵ����� ���� ����������
            
            SceneLoader.Instance.SwitchScene(NextScene);
        }
    }
}
