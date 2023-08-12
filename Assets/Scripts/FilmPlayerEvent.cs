using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FilmPlayerEvent : MonoBehaviour
{
    public string NextScene;
    [SerializeField]
    private GameObject sfxPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            // �ʸ� ���� ���.
            // 3�ʰ� �����ְ� �ڵ����� ���� ����������
            sfxPlayer.SetActive(true);
            SceneLoader.Instance.SwitchScene(NextScene);
        }
    }
}
