using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallDownPlatform : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    [SerializeField]
    private float lifeTime = 1f;

    Coroutine waitForLifeTime = null;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && waitForLifeTime == null)
        {
            waitForLifeTime = StartCoroutine(CoWaitForLifeTime());
        }
    }

    IEnumerator CoWaitForLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }


}
