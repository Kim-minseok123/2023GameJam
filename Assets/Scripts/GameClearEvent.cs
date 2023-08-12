using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearEvent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //≥≤±ÿ¡° µµ¥ﬁ
            GameManager.Instance.GameClear();
        }
    }
}
