using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    public int UpHp = 10;
    private float elapsedTime = 0f;
    private bool isTrigger = false;
    public float AliveTime;
    void Start()
    {
        if(AliveTime >= 0)
            Destroy(gameObject, AliveTime);
    }
    private void Update()
    {
        if (isTrigger)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 1f)
            {
                elapsedTime = 0f;
                var gauge = GameManager.Instance.ColdGauge;
                if ( gauge + UpHp > 300) {
                    GameManager.Instance.ColdGauge = 300;
                }
                else GameManager.Instance.ColdGauge += UpHp;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isTrigger = false;
        }
    }
}
