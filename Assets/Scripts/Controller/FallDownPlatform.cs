using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FallDownPlatform : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private ObjectTweenAnimator tweenAnimator;
    private Vector3 StartPos;

    [SerializeField]
    private float lifeTime = 1f;
    [SerializeField]
    private float additionalLifeTime = 0f;

    Coroutine waitForLifeTime = null;

    [SerializeField]
    private GameObject sfxPrefab;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        tweenAnimator = GetComponent<ObjectTweenAnimator>();
        StartPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && waitForLifeTime == null)
        {
            var calTime = lifeTime;

            if(collision.gameObject.name == "Hasel")
                calTime += additionalLifeTime;

            waitForLifeTime = StartCoroutine(CoWaitForLifeTime(calTime));
        }
    }

    IEnumerator CoWaitForLifeTime(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        tweenAnimator.PlayAnimation("Hide");
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        rigidbody2D.freezeRotation = true;
        Instantiate(sfxPrefab, transform.position, Quaternion.identity);
    }

    public void SetUp()
    {
        tweenAnimator.PlayAnimation("Show");
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        transform.position = StartPos;
        waitForLifeTime = null;
    }
}
