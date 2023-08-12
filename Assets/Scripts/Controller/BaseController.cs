using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public float _speed = 5f;
    protected float _moveInput;
    public bool _isRight = true;

    public float _jumpForce = 6f;
    public float _checkRadius = 0.2f;
    protected bool _isGrounded;
    public Transform _groundCheck;
    public LayerMask _groundLayer;
    protected bool _isJumping = false;
    public Animator animator;
    protected Rigidbody2D _rb;

    public float _coldGaugeReduced = 1;
    protected bool jumpRequested = false;
    protected bool prevGround;

    protected AudioSource _footAudio;
    public AudioClip AudioClipFoot;
    public AudioClip AudioClipJump;
    protected AudioSource _jumpAudio;
    public Animator Effect;
    public AudioClip EffectSound;
    public AudioSource AudioClipEffect;

    public float invincibleTime = 2.0f; // ���� �ð� ���� (�� ����)
    public float blinkDuration = 0.3f;  // �����̴� �ð� ����

    private bool isInvincible = false;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);
        prevGround = _isGrounded;
        _footAudio = GetComponentInChildren<AudioSource>();
        _jumpAudio = gameObject.AddComponent<AudioSource>();
    }

    public void FixedUpdate()
    {
        if (!GameManager.Instance.isPaused)
        {
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);
            Collider2D cd = Physics2D.OverlapCircle(_groundCheck.position, _checkRadius, _groundLayer);
            if (_isGrounded && cd.gameObject.CompareTag("Fail")) { jumpRequested = false; }
            _moveInput = Input.GetAxis("Horizontal");
            _rb.velocity = new Vector2(_moveInput * _speed, _rb.velocity.y);

            if (jumpRequested)
            {
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                if (_footAudio.isPlaying) _footAudio.Stop();
                _jumpAudio.clip = AudioClipJump;  // ����� ����� �ҽ�
                _jumpAudio.Play();
                animator.SetTrigger("Jump");
                _isJumping = true;
                jumpRequested = false;
            }

            if (!_isGrounded && !_isJumping)
            {
                animator.SetTrigger("Air");
                _footAudio.Stop();
            }
            else if (_isGrounded && _isGrounded != prevGround)
            {
                animator.SetTrigger("Ground");
                _isJumping = false;
            }
            prevGround = _isGrounded;


            if (_moveInput > 0 && !_isRight)
            {
                Flip();
            }
            else if (_moveInput < 0 && _isRight)
            {
                Flip();
            }
            if (_moveInput != 0 && _isGrounded && !_isJumping)
            {
                if (!_footAudio.isPlaying)
                {
                    _footAudio.clip = AudioClipFoot;
                    _footAudio.Play();
                }
                animator.SetBool("Walk", true);
            }
            else if (_moveInput == 0)
            {
                if (_footAudio.isPlaying)
                {
                    _footAudio.Stop();
                }
                animator.SetBool("Walk", false);
            }
        }
    }

    public virtual void Update()
    {
        if (GameManager.Instance.isPaused)
            return;

        if (Input.GetKeyDown(KeyCode.W) && _isGrounded)
        {
            jumpRequested = true;
        }
    }


    protected void Flip()
    {
        _isRight = !_isRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public virtual void UseSkill() {
        Effect.SetTrigger("EffectOn");
        AudioClipEffect.PlayOneShot(EffectSound);
    }
    public virtual void ChangeCharacter()
    {
        StartCoroutine(Camera.main.GetComponent<CameraController>().ZoonOut(5f));
        GameManager.Instance.SetPlayer(this.gameObject, _coldGaugeReduced);
        Effect.SetTrigger("EffectOn");
        AudioClipEffect.PlayOneShot(EffectSound);
    }
    public bool OnDamage() {
        if (isInvincible)
            return false;
        else {
            isInvincible = true;
            StartCoroutine(InvincibilityRoutine());
            return true;
        }
    }
    private IEnumerator InvincibilityRoutine()
    {
        float endTime = Time.time + invincibleTime;
        var sr = animator.gameObject.GetComponent<SpriteRenderer>();
        while (Time.time < endTime)
        {
            sr.enabled = !sr.enabled; // ��������Ʈ�� ǥ��/��ǥ�ø� ��ȯ
            yield return new WaitForSeconds(blinkDuration);
        }
        sr.enabled = true; // �����̴� ���� ���߰� ��������Ʈ�� �ٽ� ǥ��
        isInvincible = false;
    }
}
