using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCheckController : MonoBehaviour
{
    public Vector3 Startpos = Vector3.zero;
    public AudioClip FlyDeath;
    public AudioSource FlySource;
    public void Start()
    {
        GameManager.Instance.SpawnPlayer(Startpos);
    }
    public void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("s");
            GameManager.Instance.SpawnPlayer(Startpos);
            GameManager.Instance.Damage(15);
            FlySource.PlayOneShot(FlyDeath);
        }
        else if (collision.CompareTag("Fail")) {
            collision.gameObject.GetComponent<FallDownPlatform>().SetUp();
        }
    }
}
