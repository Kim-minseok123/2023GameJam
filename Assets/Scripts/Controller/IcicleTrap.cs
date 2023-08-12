using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleTrap : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private Animator animator;
    private BoxCollider2D boxCollider2D;

    [SerializeField]
    private float fallDelayTime = 0f;

    [SerializeField]
    private float fallSpeed = 5f;

    [SerializeField]
    private int damage = 20;

    private bool isFall = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();  
    }

    private void FixedUpdate()
    {
        if (isFall)
        {
            transform.position += Vector3.down * fallSpeed * Time.fixedDeltaTime;
        }
        else
        {
            var hit = Physics2D.BoxCast(transform.position + new Vector3(0f, -boxCollider2D.size.y * 2f), Vector2.one * 0.5f, 0f, Vector2.down);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    //TODO :: Drop
                    StartCoroutine(CoWaitForDelay());
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFall)
            return;

        Debug.Log(collision.gameObject.name);

        animator.SetTrigger("Break");
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        isFall = false;

        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.Damage(damage);
        }
    }

    public void OnBreak()
    {
        Destroy(gameObject);
    }

    IEnumerator CoWaitForDelay()
    {
        yield return new WaitForSeconds(fallDelayTime);
        isFall = true;
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        rigidbody2D.freezeRotation = true;

    }


}
