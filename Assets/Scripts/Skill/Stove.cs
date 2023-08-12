using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : MonoBehaviour
{
    public int UpHp = 10;
    private float elapsedTime = 0f;
    private bool isTrigger = false;
    public float AliveTime;

    public GameObject vfxFire;
    public Sprite StoveOn;
    public Sprite StoveOff;
    private SpriteRenderer sr;
    private AudioSource As;
    public AudioClip Heal;
    void Start()
    {
        if(AliveTime >= 0)
            Destroy(gameObject, AliveTime);
        sr = GetComponent<SpriteRenderer>();
        As = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isTrigger)
        {
            sr.sprite = StoveOn;
            vfxFire.SetActive(true);
            if (!As.isPlaying)
            { 
                As.Stop();
                As.PlayOneShot(Heal);
            }
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 1f)
            {

                elapsedTime = 0f;
                var gauge = GameManager.Instance.ColdGauge;
                if (gauge + UpHp > 300)
                {
                    GameManager.Instance.ColdGauge = 300;
                }
                else GameManager.Instance.ColdGauge += UpHp;
            }
        }
        else {
            sr.sprite = StoveOff;
            vfxFire.SetActive(false);
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
