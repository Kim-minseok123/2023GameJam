using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmPlayerEvent : MonoBehaviour
{
    public string NextScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { 
            // 필름 사진 찍기.
            // 3초간 보여주고 자동으로 다음 스테이지로
            
            SceneLoader.Instance.SwitchScene(NextScene);
        }
    }
}
